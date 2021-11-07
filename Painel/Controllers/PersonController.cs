using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Painel.Entities;
using Painel.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Painel.Controllers
{
    [Authorize]
    public class PersonController : Controller
    {
        private readonly PainelDBContext _context;

        public PersonController(PainelDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var people = await _context.Persons.ToListAsync();

            return View(people.Select(person => (PersonViewModel)person));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var person = await _context.Persons
                .FirstOrDefaultAsync(m => m.Code == id);

            if (person == null)
            {
                return NotFound();
            }

            return View((PersonViewModel)person);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Code,Name,Document,Address,PhoneNumber")] PersonViewModel model)
        {
            if (ModelState.IsValid)
            {
                var person = new Person()
                {
                    Code = model.Code,
                    Name = model.Name,
                    Document = model.Document,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                };

                _context.Add(person);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var person = await _context.Persons.FindAsync(id);

            if (person is null)
            {
                return NotFound();
            }

            return View((PersonViewModel)person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Code,Name,Document,Address,PhoneNumber")] PersonViewModel model)
        {
            if (id != model.Code)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var person = new Person()
                    {
                        Code = model.Code,
                        Name = model.Name,
                        Document = model.Document,
                        Address = model.Address,
                        PhoneNumber = model.PhoneNumber,
                    };

                    _context.Update(person);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await PersonExistsAsync(model.Code))
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

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var person = await _context.Persons
                .FirstOrDefaultAsync(m => m.Code == id);

            if (person is null)
            {
                return NotFound();
            }

            return View((PersonViewModel)person);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.Persons.FindAsync(id);

            _context.Persons.Remove(person);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> PersonExistsAsync(int id)
        {
            return await _context.Persons.AnyAsync(e => e.Code == id);
        }
    }
}