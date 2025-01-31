using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskManager.Data;

//[Authorize]
public class TasksController : Controller
{
    private readonly AppDbContext _context;

    public TasksController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var tasks = await _context.Tasks.Where(t => t.UserId == userId).ToListAsync();
        return View(tasks);
    }

    public IActionResult Create() => View();

    [HttpPost]
    public async Task<IActionResult> Create(TaskItem task)
    {
        task.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        _context.Add(task);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null || task.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            return NotFound();

        return View(task);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(TaskItem task)
    {
        _context.Update(task);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task != null)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Index");
    }
}
