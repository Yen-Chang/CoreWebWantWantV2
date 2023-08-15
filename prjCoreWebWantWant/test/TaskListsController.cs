using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using prjCoreWebWantWant.Models;

namespace prjCoreWebWantWant.test
{
    public class TaskListsController : Controller
    {
        private readonly NewIspanProjectContext _context;

        public TaskListsController(NewIspanProjectContext context)
        {
            _context = context;
        }

        // GET: TaskLists
        public async Task<IActionResult> Index()
        {
            var newIspanProjectContext = _context.TaskLists.Include(t => t.Payment).Include(t => t.PaymentDate).Include(t => t.Salary).Include(t => t.Town);
            return View(await newIspanProjectContext.ToListAsync());
        }

        // GET: TaskLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TaskLists == null)
            {
                return NotFound();
            }

            var taskList = await _context.TaskLists
                .Include(t => t.Payment)
                .Include(t => t.PaymentDate)
                .Include(t => t.Salary)
                .Include(t => t.Town)
                .FirstOrDefaultAsync(m => m.CaseId == id);
            if (taskList == null)
            {
                return NotFound();
            }

            return View(taskList);
        }

        // GET: TaskLists/Create
        public IActionResult Create()
        {
            ViewData["PaymentId"] = new SelectList(_context.Payments, "PaymentId", "PaymentId");
            ViewData["PaymentDateId"] = new SelectList(_context.PaymentDates, "PaymentDateId", "PaymentDateId");
            ViewData["SalaryId"] = new SelectList(_context.Salaries, "SalaryId", "SalaryId");
            ViewData["TownId"] = new SelectList(_context.Towns, "TownId", "TownId");
            return View();
        }

        // POST: TaskLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CaseId,AccountId,TaskNameId,TaskTitle,TaskDetail,WorkingHoursId,PayFrom,PayTo,PaymentId,PaymentDateId,SalaryId,TaskPlace,TownId,WorkPlace,Address,RequiredNum,TaskPeriod,TaskStartHour,TaskEndHour,TaskStartDate,TaskEndDate,Requirement,HumanList,LanguageRequired,ServiceStatus,StatusChangeReasonId,PublishOrNot,PublishStart,PublishEnd,CaseStatusId,OnTop,DataCreateDate,DataModifyDate,DataModifyPerson,IsExpert")] TaskList taskList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taskList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PaymentId"] = new SelectList(_context.Payments, "PaymentId", "PaymentId", taskList.PaymentId);
            ViewData["PaymentDateId"] = new SelectList(_context.PaymentDates, "PaymentDateId", "PaymentDateId", taskList.PaymentDateId);
            ViewData["SalaryId"] = new SelectList(_context.Salaries, "SalaryId", "SalaryId", taskList.SalaryId);
            ViewData["TownId"] = new SelectList(_context.Towns, "TownId", "TownId", taskList.TownId);
            return View(taskList);
        }

        // GET: TaskLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TaskLists == null)
            {
                return NotFound();
            }

            var taskList = await _context.TaskLists.FindAsync(id);
            if (taskList == null)
            {
                return NotFound();
            }
            ViewData["PaymentId"] = new SelectList(_context.Payments, "PaymentId", "PaymentId", taskList.PaymentId);
            ViewData["PaymentDateId"] = new SelectList(_context.PaymentDates, "PaymentDateId", "PaymentDateId", taskList.PaymentDateId);
            ViewData["SalaryId"] = new SelectList(_context.Salaries, "SalaryId", "SalaryId", taskList.SalaryId);
            ViewData["TownId"] = new SelectList(_context.Towns, "TownId", "TownId", taskList.TownId);
            return View(taskList);
        }

        // POST: TaskLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CaseId,AccountId,TaskNameId,TaskTitle,TaskDetail,WorkingHoursId,PayFrom,PayTo,PaymentId,PaymentDateId,SalaryId,TaskPlace,TownId,WorkPlace,Address,RequiredNum,TaskPeriod,TaskStartHour,TaskEndHour,TaskStartDate,TaskEndDate,Requirement,HumanList,LanguageRequired,ServiceStatus,StatusChangeReasonId,PublishOrNot,PublishStart,PublishEnd,CaseStatusId,OnTop,DataCreateDate,DataModifyDate,DataModifyPerson,IsExpert")] TaskList taskList)
        {
            if (id != taskList.CaseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taskList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskListExists(taskList.CaseId))
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
            ViewData["PaymentId"] = new SelectList(_context.Payments, "PaymentId", "PaymentId", taskList.PaymentId);
            ViewData["PaymentDateId"] = new SelectList(_context.PaymentDates, "PaymentDateId", "PaymentDateId", taskList.PaymentDateId);
            ViewData["SalaryId"] = new SelectList(_context.Salaries, "SalaryId", "SalaryId", taskList.SalaryId);
            ViewData["TownId"] = new SelectList(_context.Towns, "TownId", "TownId", taskList.TownId);
            return View(taskList);
        }

        // GET: TaskLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TaskLists == null)
            {
                return NotFound();
            }

            var taskList = await _context.TaskLists
                .Include(t => t.Payment)
                .Include(t => t.PaymentDate)
                .Include(t => t.Salary)
                .Include(t => t.Town)
                .FirstOrDefaultAsync(m => m.CaseId == id);
            if (taskList == null)
            {
                return NotFound();
            }

            return View(taskList);
        }

        // POST: TaskLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TaskLists == null)
            {
                return Problem("Entity set 'NewIspanProjectContext.TaskLists'  is null.");
            }
            var taskList = await _context.TaskLists.FindAsync(id);
            if (taskList != null)
            {
                _context.TaskLists.Remove(taskList);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskListExists(int id)
        {
          return (_context.TaskLists?.Any(e => e.CaseId == id)).GetValueOrDefault();
        }
    }
}
