using Reedly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;


namespace Reedly.Controllers
{
    public class MoviesController : Controller
    {
        //create db context vairiable
        private ApplicationDbContext _context;

        //initialise _context
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        //dispose the context
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Movies
        public ViewResult Index()
        {
            //get all the movies
            var movies = _context.Movies.Include(c => c.Genre).ToList();
            return View(movies);
        }

      
    }
}