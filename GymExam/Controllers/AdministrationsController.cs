﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GymExam.Data;
using GymExam.Models;
using Microsoft.AspNetCore.Authorization;

namespace GymExam.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdministrationsController : Controller
    {
        private readonly GymContext _context;

        public AdministrationsController(GymContext context)
        {
            _context = context;
        }

        // GET: Administrations
        public async Task<IActionResult> Index()
        {
              return _context.Administration != null ? 
                          View(await _context.Administration.ToListAsync()) :
                          Problem("Entity set 'GymContext.Administration'  is null.");
        }

        // GET: Administrations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Administration == null)
            {
                return NotFound();
            }

            var administration = await _context.Administration
                .FirstOrDefaultAsync(m => m.Id == id);
            if (administration == null)
            {
                return NotFound();
            }

            return View(administration);
        }

        // GET: Administrations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Birthday,Phone,Email,Gender,Status")] Administration administration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(administration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(administration);
        }

        // GET: Administrations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Administration == null)
            {
                return NotFound();
            }

            var administration = await _context.Administration.FindAsync(id);
            if (administration == null)
            {
                return NotFound();
            }
            return View(administration);
        }

        // POST: Administrations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Birthday,Phone,Email,Gender,Status")] Administration administration)
        {
            if (id != administration.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(administration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdministrationExists(administration.Id))
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
            return View(administration);
        }

        // GET: Administrations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Administration == null)
            {
                return NotFound();
            }

            var administration = await _context.Administration
                .FirstOrDefaultAsync(m => m.Id == id);
            if (administration == null)
            {
                return NotFound();
            }

            return View(administration);
        }

        // POST: Administrations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Administration == null)
            {
                return Problem("Entity set 'GymContext.Administration'  is null.");
            }
            var administration = await _context.Administration.FindAsync(id);
            if (administration != null)
            {
                _context.Administration.Remove(administration);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdministrationExists(int id)
        {
          return (_context.Administration?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
