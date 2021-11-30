using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MOTI.Data;
using MOTI.Models;

namespace MOTI.Controllers
{
    public class PresetController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PresetController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Preset
        public async Task<IActionResult> Index()
        {
            return View(await _context.Presets.ToListAsync());
        }

        // GET: Preset/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preset = await _context.Presets
                .FirstOrDefaultAsync(m => m.PresetId == id);
            if (preset == null)
            {
                return NotFound();
            }

            return View(preset);
        }

        // GET: Preset/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Preset/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PresetId,Title")] Preset preset)
        {
            if (ModelState.IsValid)
            {
                _context.Add(preset);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(preset);
        }

        // GET: Preset/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preset = await _context.Presets.FindAsync(id);
            if (preset == null)
            {
                return NotFound();
            }
            return View(preset);
        }

        // POST: Preset/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PresetId,Title")] Preset preset)
        {
            if (id != preset.PresetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(preset);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PresetExists(preset.PresetId))
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
            return View(preset);
        }

        // GET: Preset/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preset = await _context.Presets
                .FirstOrDefaultAsync(m => m.PresetId == id);
            if (preset == null)
            {
                return NotFound();
            }

            return View(preset);
        }

        // POST: Preset/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var preset = await _context.Presets.FindAsync(id);
            _context.Presets.Remove(preset);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PresetExists(int id)
        {
            return _context.Presets.Any(e => e.PresetId == id);
        }
    }
}
