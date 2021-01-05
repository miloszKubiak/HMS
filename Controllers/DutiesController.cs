using HMS.Data;
using HMS.Models;
using HMS.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HMS.Controllers
{

    public class DutiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DutiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin, User")]
        public List<SelectListItem> getEmployeeSelectList()
        {
            var employees = _context.Employee.ToList().OrderBy(e => e.LastName);
            var employeeSelectList = new List<SelectListItem>();
            foreach (var employee in employees)
            {
                employeeSelectList.Add(new SelectListItem
                {
                    Text = $"{employee.LastName} {employee.FirstName}",
                    Value = employee.Id.ToString()
                });
            };
            return employeeSelectList;
        }

        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Index()
        {
            var allDuties = await _context.Duty.ToListAsync();
            var dutyList = new List<DutyEmployeeViewModel>();

            foreach (var duty in allDuties)
            {
                var dutyListItem = new DutyEmployeeViewModel();
                var employee = _context.Employee.FirstOrDefault(e => e.Id == duty.EmployeeId);
                dutyListItem.Duty = duty;
                dutyListItem.EmployeeName = $"{employee.LastName} {employee.FirstName}";
                dutyList.Add(dutyListItem);
            }

            return View(dutyList);
        }

        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var duty = await _context.Duty
                .FirstOrDefaultAsync(m => m.Id == id);
            if (duty == null)
            {
                return NotFound();
            }

            return View(duty);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["EmployeeList"] = getEmployeeSelectList();

            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EmployeeId,DutyDate")] Duty duty)
        {
            ViewData["EmployeeList"] = getEmployeeSelectList();
            bool isError = false;
            ViewData["CreateError"] = "";
            var employeeDutiesCount = _context.Duty.Count(d => d.EmployeeId == duty.EmployeeId);
            if (employeeDutiesCount >= 10)
            {
                ViewData["CreateError"] = "This employee already has the maximum number of duties!";
                isError = true;
            }


            if (ModelState.IsValid && !isError)
            {
                _context.Add(duty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(duty);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var duty = await _context.Duty.FindAsync(id);
            if (duty == null)
            {
                return NotFound();
            }
            return View(duty);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EmployeeId,DutyDate")] Duty duty)
        {
            if (id != duty.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(duty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DutyExists(duty.Id))
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
            return View(duty);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var duty = await _context.Duty
                .FirstOrDefaultAsync(m => m.Id == id);
            if (duty == null)
            {
                return NotFound();
            }

            return View(duty);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var duty = await _context.Duty.FindAsync(id);
            _context.Duty.Remove(duty);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DutyExists(int id)
        {
            return _context.Duty.Any(e => e.Id == id);
        }
    }
}
