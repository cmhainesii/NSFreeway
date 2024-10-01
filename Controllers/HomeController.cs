using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NSFreeway.Contexts;
using NSFreeway.Models;

namespace NSFreeway.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly HighwayContext _context;

    public HomeController(ILogger<HomeController> logger, HighwayContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        var highways = _context.Highways.ToList();
        return View(highways);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpGet]
    public IActionResult AddHighwayForm()
    {
        var model = new HighwayModel();
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AddHighway(HighwayModel model)
    {
        if (ModelState.IsValid)
        {
            await _context.Highways.AddAsync(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        return View(model);
    }

    public IActionResult EditHighwayForm(int id)
    {
        var highway = _context.Highways.Find(id);
        if (highway == null)
        {
            return RedirectToAction("Index");
        }

        Console.WriteLine($"hw: {HighwayModel.ToReadableString(highway.RoadClass)}");

        return View("EditHighwayForm", highway);
    }

    [HttpPost]
    public async Task<IActionResult> EditHighway(HighwayModel highway)
    {

        
        if (ModelState.IsValid)
        {
            var existingHighway = await _context.Highways.FindAsync(highway.Id);
            if (existingHighway == null)
            {
                return NotFound();
            }

            _context.Entry(existingHighway).CurrentValues.SetValues(highway);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");   
        }

        return View(highway);
    }

    public async Task<IActionResult> DeleteHighway(int id)
    {
        var highway = await _context.Highways.FindAsync(id);

        if(highway == null)
        {
            return NotFound();
        }

        _context.Highways.Remove(highway);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

}
