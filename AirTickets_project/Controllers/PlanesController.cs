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
    public class PlanesController : Controller
    {
        private readonly Air_TicketsContext _context;

        public PlanesController(Air_TicketsContext context)
        {
            _context = context;
        }

        // GET: Planes
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null||id==0) return RedirectToAction("Index", "Models");
            ViewBag.ModelId = id;
            ViewBag.ModelName = name;
            var planesByModel = _context.Planes.Where(b=>b.ModelsId==id).Include(b=>b.Models);
            return View(await planesByModel.ToListAsync());
        }

        // GET: Planes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plane = await _context.Planes
                .Include(p => p.Models)
                .FirstOrDefaultAsync(m => m.PlanesId == id);
            if (plane == null)
            {
                return NotFound();
            }

            return View(plane);
        }

        // GET: Planes/Create
        public IActionResult Create(int modelsId)
        {
            //ViewData["ModelsId"] = new SelectList(_context.Models, "ModelsId", "Firm");
            ViewBag.ModelId = modelsId;
            ViewBag.ModelName = _context.Models.Where(c => c.ModelsId == modelsId).FirstOrDefault().ModelName;
            return View();
        }

        // POST: Planes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int modelsId,[Bind("PlanesId,ModelsId")] Plane plane)
        {
           // plane.ModelsId = modelsId;
            if (ModelState.IsValid)
            {
                _context.Add(plane);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Planes", new { id = modelsId, name = _context.Models.Where(c => c.ModelsId == modelsId).FirstOrDefault().ModelName });
            }
            // ViewData["ModelsId"] = new SelectList(_context.Models, "ModelsId", "Firm", plane.ModelsId);
            //return View(plane);
            return RedirectToAction("Index", "Planes", new { id = modelsId, name = _context.Models.Where(c => c.ModelsId == modelsId).FirstOrDefault().ModelName });
        }

        // GET: Planes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plane = await _context.Planes.FindAsync(id);
            if (plane == null)
            {
                return NotFound();
            }
            ViewData["ModelsId"] = new SelectList(_context.Models, "ModelsId", "Firm", plane.ModelsId);
            return View(plane);
        }

        // POST: Planes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlanesId,ModelsId")] Plane plane)
        {
            if (id != plane.PlanesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plane);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlaneExists(plane.PlanesId))
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
            ViewData["ModelsId"] = new SelectList(_context.Models, "ModelsId", "Firm", plane.ModelsId);
            return View(plane);
        }

        // GET: Planes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plane = await _context.Planes
                .Include(p => p.Models)
                .FirstOrDefaultAsync(m => m.PlanesId == id);
            if (plane == null)
            {
                return NotFound();
            }

            return View(plane);
        }

        // POST: Planes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plane = await _context.Planes.FindAsync(id);
            _context.Planes.Remove(plane);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlaneExists(int id)
        {
            return _context.Planes.Any(e => e.PlanesId == id);
        }
    }
}
