using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CAG_DW.Data;
using CAG_DW.Models;
using Microsoft.AspNetCore.Authorization;

namespace CAG_DW.Agreements
{
    [Authorize(Roles = "admin")]
    public class AgreementsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AgreementsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Agreements
        public async Task<IActionResult> Index()
        {
              return _context.Agreements != null ? 
                          View(await _context.Agreements.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Agreements'  is null.");
        }

        // GET: Agreements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Agreements == null)
            {
                return NotFound();
            }

            var agreement = await _context.Agreements
                .FirstOrDefaultAsync(m => m.AgreementID == id);
            if (agreement == null)
            {
                return NotFound();
            }

            return View(agreement);
        }

        // GET: Agreements/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Agreements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AgreementID,OrderDate,CustomerID")] Agreement agreement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agreement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(agreement);
        }

        // GET: Agreements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Agreements == null)
            {
                return NotFound();
            }

            var agreement = await _context.Agreements.FindAsync(id);
            if (agreement == null)
            {
                return NotFound();
            }
            return View(agreement);
        }

        // POST: Agreements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AgreementID,OrderDate,CustomerID")] Agreement agreement)
        {
            if (id != agreement.AgreementID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agreement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgreementExists(agreement.AgreementID))
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
            return View(agreement);
        }

        // GET: Agreements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Agreements == null)
            {
                return NotFound();
            }

            var agreement = await _context.Agreements
                .FirstOrDefaultAsync(m => m.AgreementID == id);
            if (agreement == null)
            {
                return NotFound();
            }

            return View(agreement);
        }

        // POST: Agreements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Agreements == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Agreements'  is null.");
            }
            var agreement = await _context.Agreements.FindAsync(id);
            if (agreement != null)
            {
                _context.Agreements.Remove(agreement);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgreementExists(int id)
        {
          return (_context.Agreements?.Any(e => e.AgreementID == id)).GetValueOrDefault();
        }
    }
}
