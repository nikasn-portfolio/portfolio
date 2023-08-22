using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Contracts.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using DAL.Contracts.App;
using Domain.App;
using Public.DTO;
using Public.DTO.Mappers;

namespace WebApp.Controllers
{
    public class PaymentMethodsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IAppUOW _uow;
        private readonly PaymentMethodMapper _mapper;
        private readonly IAppBLL _bll;

        public PaymentMethodsController(ApplicationDbContext context, IAppUOW uow, IMapper autoMapper, IAppBLL bll)
        {
            _context = context;
            _uow = uow;
            _bll = bll;
            _mapper = new PaymentMethodMapper(autoMapper);
        }

        // GET: PaymentMethods
        public async Task<List<PaymentMethodDTO?>> Index()
        {
            var data = await  _bll.PaymentMethodService.AllAsync();
            var res = data.Select(e => _mapper.Map(e)).ToList();
            return res!;
        }

        // GET: PaymentMethods/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.PaymentMethods == null)
            {
                return NotFound();
            }

            var paymentMethod = await _context.PaymentMethods
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentMethod == null)
            {
                return NotFound();
            }

            return View(paymentMethod);
        }

        // GET: PaymentMethods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PaymentMethods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentMethodName,MethodImageUrl,Id")] PaymentMethod paymentMethod)
        {
            if (ModelState.IsValid)
            {
                paymentMethod.Id = Guid.NewGuid();
                _context.Add(paymentMethod);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paymentMethod);
        }

        // GET: PaymentMethods/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.PaymentMethods == null)
            {
                return NotFound();
            }

            var paymentMethod = await _context.PaymentMethods.FindAsync(id);
            if (paymentMethod == null)
            {
                return NotFound();
            }
            return View(paymentMethod);
        }

        // POST: PaymentMethods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PaymentMethodName,MethodImageUrl,Id")] PaymentMethod paymentMethod)
        {
            if (id != paymentMethod.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paymentMethod);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentMethodExists(paymentMethod.Id))
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
            return View(paymentMethod);
        }

        // GET: PaymentMethods/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.PaymentMethods == null)
            {
                return NotFound();
            }

            var paymentMethod = await _context.PaymentMethods
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paymentMethod == null)
            {
                return NotFound();
            }

            return View(paymentMethod);
        }

        // POST: PaymentMethods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.PaymentMethods == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PaymentMethods'  is null.");
            }
            var paymentMethod = await _context.PaymentMethods.FindAsync(id);
            if (paymentMethod != null)
            {
                _context.PaymentMethods.Remove(paymentMethod);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentMethodExists(Guid id)
        {
          return (_context.PaymentMethods?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
