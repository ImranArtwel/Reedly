using Reedly.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Reedly.ViewModels;

namespace Reedly.Controllers
{
    public class CustomersController : Controller
    {
        //db context to access the database
        private ApplicationDbContext _context;

        //initialize the variable _context
        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        //dispose the db context object securely

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Customers
        public ViewResult Index()
        {
            //get all the customers together with membership types
            var customers = _context.Customers.Include(c => c.MembershipType).ToList(); //to get all e customers in the db
            return View(customers);
        }

        //get details of individual customer
        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c=> c.MembershipType).SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return Content("Customer with Id = {0} does not exist" + id);
            else
                return View(customer);
        }

        //adding a new customer
        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };
            return View("CustomerForm",viewModel);
        }
        
        //saving the new customer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            //check if model is valid
            if(!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer= customer,
                    MembershipTypes = _context.MembershipTypes.ToList()

                };

                return View("CustomerForm",viewModel);
            }

            else
            {
                if (customer.Id == 0)
                    _context.Customers.Add(customer);
                else
                {
                    var existingCustomer = _context.Customers.Single(c => c.Id == customer.Id);

                    existingCustomer.Name = customer.Name;
                    existingCustomer.Birthday = customer.Birthday;
                    existingCustomer.MembershipTypeId = customer.MembershipTypeId;
                    existingCustomer.isSubscribedToNewsLetter = customer.isSubscribedToNewsLetter;
                }
                _context.SaveChanges();

                return RedirectToAction("Index", "Customers");

            }
            
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }

       
    }
}