using System.Diagnostics;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NSFreeway.Contexts;
using NSFreeway.Models;

namespace NSFreeway.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly HighwayDataContext _context;

    public HomeController(ILogger<HomeController> logger, HighwayDataContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        var highways = _context.Highways.ToList();
        return View(highways);
    }

    public IActionResult Construction()
    {
        var projects = _context.ConstructionProjects
                    .Include(p => p.Highway)
                    .ToList();        

        return View(projects);
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

    [HttpGet]
    public IActionResult AddConstructionRoadSelect()
    {
        var highways = _context.Highways.OrderBy(m => m.RoadClass).ThenBy(m => m.RoadNumber).ToList();
        return View(highways);
    }

    [HttpGet]
    public IActionResult AddConstructionForm(int roadId)
    {
        var project = new RoadConstruction();
        project.RoadId = roadId;
        project.StartDate = DateTime.Now;
        project.EndDate = DateTime.Now;

        return View(project);
    }

    [HttpPost]
    public async Task<IActionResult> AddConstructionForm(RoadConstruction project)
    {
        if(!ModelState.IsValid)
        {
            return RedirectToAction("Construction");
        }

        await _context.ConstructionProjects.AddAsync(project);
        await _context.SaveChangesAsync();

        return RedirectToAction("Construction");
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

    public async Task<IActionResult> EditHighwayForm(int id)
    {
        var highway = await _context.Highways.FindAsync(id);
        if (highway == null)
        {
            return RedirectToAction("Index");
        }

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


    [HttpGet]
    public async Task<IActionResult> EditConstruction(int projectId)
    {
        Console.WriteLine($"Project ID: {projectId}");
        var project = await _context.ConstructionProjects.FindAsync(projectId);
        
        if(project == null)
        {
            return NotFound();
        }

        return View(project);
    }

    [HttpPost]
    public async Task<IActionResult> EditConstruction(RoadConstruction project)
    {
        if(!ModelState.IsValid)
        {
            RedirectToAction("Construction");
        }

        var originalProject = await _context.ConstructionProjects.FindAsync(project.Id);
        Console.WriteLine($"Project ID 2: {project.Id}");
        if(originalProject == null)
        {
            return NotFound();
        }

        _context.Entry(originalProject).CurrentValues.SetValues(project);
        await _context.SaveChangesAsync();
        return RedirectToAction("Construction");
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

    public async Task<IActionResult> DeleteConstruction(int id)
    {
        if( id <= 0)
        {
            return RedirectToAction("Construction");
        }
        
        var project = await _context.ConstructionProjects.FindAsync(id);
        if(project == null)
        {
            // Not found
            return RedirectToAction("Construction");
        }

        _context.ConstructionProjects.Remove(project);
        await _context.SaveChangesAsync();

        return RedirectToAction("Construction");
    }

}
