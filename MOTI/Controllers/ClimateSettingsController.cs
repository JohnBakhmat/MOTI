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
    public class ClimateSettingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClimateSettingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ClimateSettings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ClimateSettings.Include(c => c.Request);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ClimateSettings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var climateSetting = await _context.ClimateSettings
                .Include(c => c.Request)
                .FirstOrDefaultAsync(m => m.ClimateSettingId == id);
            if (climateSetting == null)
            {
                return NotFound();
            }

            return View(climateSetting);
        }

        // GET: ClimateSettings/Create
        public IActionResult Create()
        {
            ViewData["RequestId"] = new SelectList(_context.Requests, "RequestId", "RequestId");
            return View();
        }

        // POST: ClimateSettings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClimateSettingId,ClimateType,Value,Units,RequestId")] ClimateSetting climateSetting)
        {
            if (ModelState.IsValid)
            {
                _context.Add(climateSetting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RequestId"] = new SelectList(_context.Requests, "RequestId", "RequestId", climateSetting.RequestId);
            return View(climateSetting);
        }

        // GET: ClimateSettings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var climateSetting = await _context.ClimateSettings.FindAsync(id);
            if (climateSetting == null)
            {
                return NotFound();
            }
            ViewData["RequestId"] = new SelectList(_context.Requests, "RequestId", "RequestId", climateSetting.RequestId);
            return View(climateSetting);
        }

        // POST: ClimateSettings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClimateSettingId,ClimateType,Value,Units,RequestId")] ClimateSetting climateSetting)
        {
            if (id != climateSetting.ClimateSettingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(climateSetting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClimateSettingExists(climateSetting.ClimateSettingId))
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
            ViewData["RequestId"] = new SelectList(_context.Requests, "RequestId", "RequestId", climateSetting.RequestId);
            return View(climateSetting);
        }

        // GET: ClimateSettings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var climateSetting = await _context.ClimateSettings
                .Include(c => c.Request)
                .FirstOrDefaultAsync(m => m.ClimateSettingId == id);
            if (climateSetting == null)
            {
                return NotFound();
            }

            return View(climateSetting);
        }

        // POST: ClimateSettings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var climateSetting = await _context.ClimateSettings.FindAsync(id);
            _context.ClimateSettings.Remove(climateSetting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClimateSettingExists(int id)
        {
            return _context.ClimateSettings.Any(e => e.ClimateSettingId == id);
        }
    }
}
