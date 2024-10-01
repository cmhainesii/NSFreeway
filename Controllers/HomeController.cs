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
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddHighway(HighwayClass roadClass, ushort roadNumber,
        string beginCity, string beginState, string endCity, string endState,
        uint length, string tollRoad)
    {
        bool IsTollRoad = tollRoad.CompareTo("true") == 0 ? true : false;
        var highway = new HighwayModel
        {
            RoadClass = roadClass,
            RoadNumber = roadNumber,
            BeginCity = beginCity,
            BeginState = beginState,
            EndCity = endCity,
            EndState = endState,
            Length = length,
            IsTollRoad = IsTollRoad
        };

        await _context.Highways.AddAsync(highway);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
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
    public async Task<IActionResult> EditHighway(int id, HighwayClass roadClass,
     ushort roadNumber, string beginCity, string beginState,
     string endCity, string endState, uint length, string tollRoad)
    {

        var highway = await _context.Highways.FindAsync(id);
        if (highway == null)
        {
            Console.WriteLine("NO!!!!!");
            return RedirectToAction("Index");

        }

        highway.RoadClass = roadClass;
        highway.RoadNumber = roadNumber;
        highway.BeginCity = beginCity;
        highway.BeginState = beginState;
        highway.EndCity = endCity;
        highway.EndState = endState;
        highway.Length = length;
        highway.IsTollRoad = tollRoad.CompareTo("true") == 0;

        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
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
