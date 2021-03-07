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
    public class TicketsController : Controller
    {
        private readonly Air_TicketsContext _context;

        public TicketsController(Air_TicketsContext context)
        {
            _context = context;
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            //if (id == null) return RedirectToAction("Types", "Index");
            var air_TicketsContext = _context.Tickets.Where(x=>x.ClientsId!=null).Include(t => t.Clients).Include(t => t.Flights).Include(t => t.Types);
            return View(await air_TicketsContext.ToListAsync());
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.Clients)
                .Include(t => t.Flights)
                .Include(t => t.Types)
                .FirstOrDefaultAsync(m => m.TicketsId == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            ViewData["ClientsId"] = new SelectList(_context.Clients, "ClientsId", "Name");
            ViewData["FlightsId"] = new SelectList(_context.Flights, "FlightsId", "FlightsId");
            //ViewData["TypesId"] = new SelectList(_context.Types, "TypesId", "TypeName");
            ViewBag.Types = new SelectList(_context.Types, "TypesId", "TypeName");
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TicketsId,FlightsId,Cost,ClientsId, TypesId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                var tick = _context.Tickets.Where(x => x.FlightsId == ticket.FlightsId).Where(x => x.Cost == ticket.Cost).FirstOrDefault();
                if (tick != null)
                {
                    tick.ClientsId = ticket.ClientsId;
                    _context.Update(tick);
                    //_context.Add(ticket);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                    // return RedirectToAction("Index", "Tickets", new { id = typeId, name = _context.Types.Where(t => t.TypesId==typeId).FirstOrDefault().TypeName });
                }
            }
            ViewData["ClientsId"] = new SelectList(_context.Clients, "ClientsId", "ClientsId", ticket.ClientsId);
            ViewData["FlightsId"] = new SelectList(_context.Flights, "FlightsId", "FlightsId", ticket.FlightsId);
            ViewData["TypesId"] = new SelectList(_context.Types, "TypesId", "TypeName", ticket.TypesId);
            //  return RedirectToAction("Index","Tickets", new { id = typeId, name = _context.Types.Where(t => t.TypesId == typeId).FirstOrDefault().TypeName });
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            ViewData["ClientsId"] = new SelectList(_context.Clients, "ClientsId", "ClientsId", ticket.ClientsId);
            ViewData["FlightsId"] = new SelectList(_context.Flights, "FlightsId", "FlightsId", ticket.FlightsId);
            ViewData["TypesId"] = new SelectList(_context.Types, "TypesId", "TypeName", ticket.TypesId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TicketsId,FlightsId,TypesId,Cost,ClientsId")] Ticket ticket)
        {
            if (id != ticket.TicketsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.TicketsId))
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
            ViewData["ClientsId"] = new SelectList(_context.Clients, "ClientsId", "ClientsId", ticket.ClientsId);
            ViewData["FlightsId"] = new SelectList(_context.Flights, "FlightsId", "FlightsId", ticket.FlightsId);
            ViewData["TypesId"] = new SelectList(_context.Types, "TypesId", "TypeName", ticket.TypesId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.Clients)
                .Include(t => t.Flights)
                .Include(t => t.Types)
                .FirstOrDefaultAsync(m => m.TicketsId == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.TicketsId == id);
        }
    }
}
