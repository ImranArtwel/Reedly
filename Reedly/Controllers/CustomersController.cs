using Reedly.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

       
    }
}