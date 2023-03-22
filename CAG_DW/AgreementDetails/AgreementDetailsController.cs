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

namespace CAG_DW.AgreementDetails
{
    [Authorize(Roles = "admin")]
    public class AgreementDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AgreementDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AgreementDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.agreementDetails.Include(a => a.Agreement).Include(a => a.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AgreementDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.agreementDetails == null)
            {
                return NotFound();
            }

            var agreementDetail = await _context.agreementDetails
                .Include(a => a.Agreement)
                .Include(a => a.Product)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (agreementDetail == null)
            {
                return NotFound();
            }

            return View(agreementDetail);
        }

        // GET: AgreementDetails/Create
        public IActionResult Create()
        {
            ViewData["AgreementID"] = new SelectList(_context.Agreements, "AgreementID", "AgreementID");
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "ProductID");
            return View();
        }

        // POST: AgreementDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Qty,ProductID,AgreementID")] AgreementDetail agreementDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agreementDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AgreementID"] = new SelectList(_context.Agreements, "AgreementID", "AgreementID", agreementDetail.AgreementID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "ProductID", agreementDetail.ProductID);
            return View(agreementDetail);
        }

        // GET: AgreementDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.agreementDetails == null)
            {
                return NotFound();
            }

            var agreementDetail = await _context.agreementDetails.FindAsync(id);
            if (agreementDetail == null)
            {
                return NotFound();
            }
            ViewData["AgreementID"] = new SelectList(_context.Agreements, "AgreementID", "AgreementID", agreementDetail.AgreementID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "ProductID", agreementDetail.ProductID);
            return View(agreementDetail);
        }

        // POST: AgreementDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Qty,ProductID,AgreementID")] AgreementDetail agreementDetail)
        {
            if (id != agreementDetail.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agreementDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgreementDetailExists(agreementDetail.ID))
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
            ViewData["AgreementID"] = new SelectList(_context.Agreements, "AgreementID", "AgreementID", agreementDetail.AgreementID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "ProductID", agreementDetail.ProductID);
            return View(agreementDetail);
        }

        // GET: AgreementDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.agreementDetails == null)
            {
                return NotFound();
            }

            var agreementDetail = await _context.agreementDetails
                .Include(a => a.Agreement)
                .Include(a => a.Product)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (agreementDetail == null)
            {
                return NotFound();
            }

            return View(agreementDetail);
        }

        // POST: AgreementDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.agreementDetails == null)
            {
                return Problem("Entity set 'ApplicationDbContext.agreementDetails'  is null.");
            }
            var agreementDetail = await _context.agreementDetails.FindAsync(id);
            if (agreementDetail != null)
            {
                _context.agreementDetails.Remove(agreementDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgreementDetailExists(int id)
        {
          return (_context.agreementDetails?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
