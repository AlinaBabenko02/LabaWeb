﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AirTickets_project;

namespace AirTickets_project.Controllers
{
    public class ModelsController : Controller
    {
        private readonly Air_TicketsContext _context;

        public ModelsController(Air_TicketsContext context)
        {
            _context = context;
        }

        // GET: Models
        public async Task<IActionResult> Index()
        {
            return View(await _context.Models.ToListAsync());
        }

        // GET: Models/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.Models
                .FirstOrDefaultAsync(m => m.ModelsId == id);
            if (model == null)
            {
                return NotFound();
            }

            //return View(model);
            return RedirectToAction("Index", "Planes", new { id = model.ModelsId, name = model.ModelName });
        }

        // GET: Models/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Models/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ModelsId,Firm,NumberOfSeats,ModelName")] Model model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Models/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.Models.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        // POST: Models/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ModelsId,Firm,NumberOfSeats,ModelName")] Model model)
        {
            if (id != model.ModelsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModelExists(model.ModelsId))
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
            return View(model);
        }

        // GET: Models/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.Models
                .FirstOrDefaultAsync(m => m.ModelsId == id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: Models/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var model = await _context.Models.FindAsync(id);
            _context.Models.Remove(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModelExists(int id)
        {
            return _context.Models.Any(e => e.ModelsId == id);
        }
    }
}
