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
    public class WorkoutsController : Controller
    {
        private readonly GymContext _context;

        public WorkoutsController(GymContext context)
        {
            _context = context;
        }

        // GET: Workouts
        public async Task<IActionResult> Index()
        {
              return _context.Workouts != null ? 
                          View(await _context.Workouts.ToListAsync()) :
                          Problem("Entity set 'GymContext.Workouts'  is null.");
        }

        // GET: Workouts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Workouts == null)
            {
                return NotFound();
            }

            var workout = await _context.Workouts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workout == null)
            {
                return NotFound();
            }

            return View(workout);
        }

        // GET: Workouts/Create
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "admin, coach")]
        // POST: Workouts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Time,startTrainingTime,TypeWorkout")] Workout workout)
        {
            if (ModelState.IsValid)
            {
                _context.Add(workout);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(workout);
        }
        [Authorize(Roles = "admin, coach")]
        // GET: Workouts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Workouts == null)
            {
                return NotFound();
            }

            var workout = await _context.Workouts.FindAsync(id);
            if (workout == null)
            {
                return NotFound();
            }
            return View(workout);
        }

        // POST: Workouts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Time,startTrainingTime,TypeWorkout")] Workout workout)
        {
            if (id != workout.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workout);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkoutExists(workout.Id))
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
            return View(workout);
        }
        [Authorize(Roles = "admin, coach")]
        // GET: Workouts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Workouts == null)
            {
                return NotFound();
            }

            var workout = await _context.Workouts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workout == null)
            {
                return NotFound();
            }

            return View(workout);
        }
        [Authorize(Roles = "admin, coach")]
        // POST: Workouts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Workouts == null)
            {
                return Problem("Entity set 'GymContext.Workouts'  is null.");
            }
            var workout = await _context.Workouts.FindAsync(id);
            if (workout != null)
            {
                _context.Workouts.Remove(workout);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkoutExists(int id)
        {
          return (_context.Workouts?.Any(e => e.Id == id)).GetValueOrDefault();
        }


       private Random gen = new Random();

        public DateTime RandomDay()
        {
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }

        public IActionResult RecordToWorkout()
        {
            return View();
        }

    }
}
