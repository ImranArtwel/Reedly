using Reedly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Reedly.ViewModels;

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
            //var movies = _context.Movies.Include(c => c.Genre).ToList();
            return View();
        }
        public ViewResult Details(int id)
        {
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id);

            return View(movie);
        }
        
        //add a new movie form
        public ViewResult New()
        {
            var genres = _context.Genres.ToList(); // get all genre types from db
            var viewModel = new MovieFormViewModel
            {
                
               Genres = genres
            };
            return View("MovieForm",viewModel);
        }

       // save new movie to database
       [HttpPost]
       [ValidateAntiForgeryToken]
       public ActionResult Save(Movie movie)
        {
            if(movie.Id == 0)
            {
                
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var existinMovie = _context.Movies.Single(m => m.Id == movie.Id);
                existinMovie.Name = movie.Name;
                existinMovie.ReleaseDate = movie.ReleaseDate;
                existinMovie.GenreId = movie.GenreId;
                existinMovie.NumberInStock = movie.NumberInStock;
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }

        //edit movie form
        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel
            {
                Movie = movie,
                Genres = _context.Genres.ToList()
            };
            return View("MovieForm", viewModel);
        }
    }
}