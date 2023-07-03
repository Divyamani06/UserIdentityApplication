using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IdentityApplication.Context;
using IdentityApplication.Models;
using Microsoft.AspNetCore.Authorization;

namespace IdentityApplication.Controllers
{
    [Authorize]
    public class IdentityUserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IdentityUserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: IdentityUser
        public async Task<IActionResult> Index()
        {
              return _context.IdentityModel != null ? 
                          View(await _context.IdentityModel.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.IdentityModel'  is null.");
        }

        // GET: IdentityUser/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.IdentityModel == null)
            {
                return NotFound();
            }

            var identityModel = await _context.IdentityModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (identityModel == null)
            {
                return NotFound();
            }

            return View(identityModel);
        }

        // GET: IdentityUser/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IdentityUser/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Mobile,Email,Soruces")] IdentityModel identityModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(identityModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(identityModel);
        }

        // GET: IdentityUser/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.IdentityModel == null)
            {
                return NotFound();
            }

            var identityModel = await _context.IdentityModel.FindAsync(id);
            if (identityModel == null)
            {
                return NotFound();
            }
            return View(identityModel);
        }

        // POST: IdentityUser/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Mobile,Email,Soruces")] IdentityModel identityModel)
        {
            if (id != identityModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(identityModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IdentityModelExists(identityModel.Id))
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
            return View(identityModel);
        }

        // GET: IdentityUser/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.IdentityModel == null)
            {
                return NotFound();
            }

            var identityModel = await _context.IdentityModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (identityModel == null)
            {
                return NotFound();
            }

            return View(identityModel);
        }

        // POST: IdentityUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.IdentityModel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.IdentityModel'  is null.");
            }
            var identityModel = await _context.IdentityModel.FindAsync(id);
            if (identityModel != null)
            {
                _context.IdentityModel.Remove(identityModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IdentityModelExists(int id)
        {
          return (_context.IdentityModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
