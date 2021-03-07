using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AirTickets_project;

namespace AirTickets_project.Controllers
{
    public class FlightsController : Controller
    {
        private readonly Air_TicketsContext _context;

        public FlightsController(Air_TicketsContext context)
        {
            _context = context;
        }

        // GET: Flights
        public async Task<IActionResult> Index(int? idDeparture, int? idArrival, DateTime? departureTime )
        {
            var air_TicketsContext = _context.Flights.Where(x => true);
            if(idDeparture!=null&&idDeparture!=0)
            {
                air_TicketsContext = air_TicketsContext.Where(x => x.PlaceOfDeparture == idDeparture);
            }
            if (idArrival != null&&idArrival!=0)
            {
                air_TicketsContext = air_TicketsContext.Where(x => x.PlaceOfArrival == idArrival);
            }
            if (departureTime != null)
            {
                air_TicketsContext = air_TicketsContext
                    .Where(x => (x.DepartureTime.Value.Date == departureTime.GetValueOrDefault().Date));
            }
            ViewBag.airports = new SelectList(_context.Airports, "AirportsId", "NameOfAirport");
           var answer=air_TicketsContext.Include(f => f.PlaceOfArrivalNavigation).Include(f => f.PlaceOfDepartureNavigation).Include(f => f.Planes);
            return View(await answer.ToListAsync());
        }

        // GET: Flights/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights
                .Include(f => f.PlaceOfArrivalNavigation)
                .Include(f => f.PlaceOfDepartureNavigation)
                .Include(f => f.Planes)
                .FirstOrDefaultAsync(m => m.FlightsId == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // GET: Flights/Create
        public IActionResult Create()
        {
            ViewData["PlaceOfArrival"] = new SelectList(_context.Airports, "AirportsId", "NameOfAirport");
            ViewData["PlaceOfDeparture"] = new SelectList(_context.Airports, "AirportsId", "NameOfAirport");
            ViewData["PlanesId"] = new SelectList(_context.Planes, "PlanesId", "PlanesId");
            return View();
        }

        // POST: Flights/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlanesId,PlaceOfDeparture,DepartureTime,PlaceOfArrival,ArrivalTime,FlightsId,FlightTime")] Flight flight)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flight);
                await _context.SaveChangesAsync();
                Init.Initialize();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlaceOfArrival"] = new SelectList(_context.Airports, "AirportsId", "NameOfAirport", flight.PlaceOfArrival);
            ViewData["PlaceOfDeparture"] = new SelectList(_context.Airports, "AirportsId", "NameOfAirport", flight.PlaceOfDeparture);
            ViewData["PlanesId"] = new SelectList(_context.Planes, "PlanesId", "PlanesId", flight.PlanesId);
            return View(flight);
        }

        // GET: Flights/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights.FindAsync(id);
            if (flight == null)
            {
                return NotFound();
            }
            ViewData["PlaceOfArrival"] = new SelectList(_context.Airports, "AirportsId", "NameOfAirport", flight.PlaceOfArrival);
            ViewData["PlaceOfDeparture"] = new SelectList(_context.Airports, "AirportsId", "NameOfAirport", flight.PlaceOfDeparture);
            ViewData["PlanesId"] = new SelectList(_context.Planes, "PlanesId", "PlanesId", flight.PlanesId);
            return View(flight);
        }

        // POST: Flights/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlanesId,PlaceOfDeparture,DepartureTime,PlaceOfArrival,ArrivalTime,FlightsId,FlightTime")] Flight flight)
        {
            if (id != flight.FlightsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flight);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightExists(flight.FlightsId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlaceOfArrival"] = new SelectList(_context.Airports, "AirportsId", "NameOfAirport", flight.PlaceOfArrival);
            ViewData["PlaceOfDeparture"] = new SelectList(_context.Airports, "AirportsId", "NameOfAirport", flight.PlaceOfDeparture);
            ViewData["PlanesId"] = new SelectList(_context.Planes, "PlanesId", "PlanesId", flight.PlanesId);
            return View(flight);
        }

        // GET: Flights/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flight = await _context.Flights
                .Include(f => f.PlaceOfArrivalNavigation)
                .Include(f => f.PlaceOfDepartureNavigation)
                .Include(f => f.Planes)
                .FirstOrDefaultAsync(m => m.FlightsId == id);
            if (flight == null)
            {
                return NotFound();
            }

            return View(flight);
        }

        // POST: Flights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flight = await _context.Flights.FindAsync(id);
            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlightExists(int id)
        {
            return _context.Flights.Any(e => e.FlightsId == id);
        }
    }
}
