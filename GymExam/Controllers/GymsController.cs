using System;
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

    public class GymsController : Controller
    {
        private readonly GymContext _context;

        public GymsController(GymContext context)
        {
            _context = context;
        }

        // GET: Gyms
        public async Task<IActionResult> Index()
        {
              return _context.Gyms != null ? 
                          View(await _context.Gyms.ToListAsync()) :
                          Problem("Entity set 'GymContext.Gyms'  is null.");
        }

        // GET: Gyms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Gyms == null)
            {
                return NotFound();
            }

            var gym = await _context.Gyms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gym == null)
            {
                return NotFound();
            }

            return View(gym);
        }

        [Authorize(Roles = "admin")]
        // GET: Gyms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gyms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Gym gym)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gym);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gym);
        }

        [Authorize(Roles = "admin")]
        // GET: Gyms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Gyms == null)
            {
                return NotFound();
            }

            var gym = await _context.Gyms.FindAsync(id);
            if (gym == null)
            {
                return NotFound();
            }
            return View(gym);
        }

        // POST: Gyms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Gym gym)
        {
            if (id != gym.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gym);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GymExists(gym.Id))
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
            return View(gym);
        }
        [Authorize(Roles = "admin")]
        // GET: Gyms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Gyms == null)
            {
                return NotFound();
            }

            var gym = await _context.Gyms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gym == null)
            {
                return NotFound();
            }

            return View(gym);
        }
        [Authorize(Roles = "admin")]
        // POST: Gyms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Gyms == null)
            {
                return Problem("Entity set 'GymContext.Gyms'  is null.");
            }
            var gym = await _context.Gyms.FindAsync(id);
            if (gym != null)
            {
                _context.Gyms.Remove(gym);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GymExists(int id)
        {
          return (_context.Gyms?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
