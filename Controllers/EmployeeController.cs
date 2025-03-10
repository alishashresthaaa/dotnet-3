using Assignment3.Data;
using Assignment3.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment3.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly AppDbContext _context; // Database context to interact with the database

        // Constructor to inject the database context into the controller
        public EmployeeController(AppDbContext context)
        {
            _context = context; // Assign the injected context to the class-level context variable
        }

        // GET: /Employee/List
        // Action to display the list of all employees
        public IActionResult List()
        {
            // Retrieve all employees from the database and pass them to the view
            return View(_context.Employees.ToList()); // Views/Employee/List.cshtml
        }

        // GET: /Employee/Details/{id}
        // Action to display the details of a specific employee by ID
        public IActionResult Details(int id)
        {
            // Retrieve the employee based on the provided ID
            Employee employee = _context.Employees.Where(e => e.EmployeeId == id)
                .FirstOrDefault() ?? new Employee(); // If not found, return a new empty employee object

            // Pass the employee to the view
            return View(employee); // Views/Employee/Details.cshtml
        }

        // GET: /Employee/Add
        // Action to render the Add form to create a new employee
        [HttpGet]
        public IActionResult Add()
        {
            // Return the "Update" view with a new employee object (this view is shared for Add/Update operations)
            return View("Update", new Employee()); // Views/Employee/Update.cshtml
        }

        // POST: /Employee/Add
        // Action to handle the form submission for creating a new employee
        [HttpPost]
        public IActionResult Add(Employee employee)
        {
            // Check if the submitted form data is valid
            if (ModelState.IsValid)
            {
                // Add the new employee to the database
                _context.Employees.Add(employee);
                _context.SaveChanges(); // Save changes to the database
                return RedirectToAction("List"); // Redirect to the List page after successful addition
            }

            // If model state is not valid, return to the Update view with the current employee data
            return View("Update", employee); // Views/Employee/Update.cshtml
        }

        // GET: /Employee/Update/{id}
        // Action to render the Update form for editing an existing employee by ID
        [HttpGet]
        public IActionResult Update(int id)
        {
            // Retrieve the employee to be updated
            Employee employee = _context.Employees.Where(e => e.EmployeeId == id)
                .FirstOrDefault() ?? new Employee(); // If not found, return a new empty employee object

            // Pass the employee to the view
            return View(employee); // Views/Employee/Update.cshtml
        }

        // POST: /Employee/Update
        // Action to handle the form submission for updating an existing employee
        [HttpPost]
        public IActionResult Update(Employee employee)
        {
            // Check if the submitted form data is valid
            if (ModelState.IsValid)
            {
                // Update the employee in the database
                _context.Employees.Update(employee);
                _context.SaveChanges(); // Save changes to the database
                return RedirectToAction("List"); // Redirect to the List page after successful update
            }

            // If model state is not valid, return to the Update view with the current employee data
            return View(employee); // Views/Employee/Update.cshtml
        }

        // GET: /Employee/GetDelete/{id}
        // Action to render the confirmation page for deleting an employee
        public IActionResult GetDelete(int id)
        {
            // Retrieve the employee to be deleted
            Employee employee = _context.Employees.Where(e => e.EmployeeId == id)
                .FirstOrDefault() ?? new Employee();

            // If the employee doesn't exist, return a 404 Not Found response
            if (employee == null)
            {
                return View("List"); // Employee not found, return to list
            }

            // Pass the employee to the confirmation view
            return View(employee); // Views/Employee/GetDelete.cshtml
        }

        // POST: /Employee/Delete/{id}
        // Action to handle the deletion of an employee after confirmation
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            // Retrieve the employee to be deleted
            Employee employee = _context.Employees.Where(e => e.EmployeeId == id)
                .FirstOrDefault() ?? new Employee();

            // If the employee is found, remove it from the database
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges(); // Save changes to the database
            }

            // Redirect to the List page after successful deletion
            return RedirectToAction("List");
        }
    }
}
