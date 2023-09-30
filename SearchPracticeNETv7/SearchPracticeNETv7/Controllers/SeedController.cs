using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using SearchPracticeNETv7.DAL;

namespace SearchPracticeNETv7.Controllers
{
    public class SeedController : Controller
    {
        private AppDbContext _context;

        public SeedController(AppDbContext dbContext)
        {
            _context = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SeedAllMonths()
        {
            try
            {
                Seeding.SeedMonths.SeedAllMonths(_context);
            }
            catch (Exception ex)
            {
                List<String> Errors = new List<String>();

                //Add the errors
                Errors.Add(ex.Message);

                //see if there are any inner exceptions
                //if there are inner exceptions, add their messages
                if (ex.InnerException != null)
                {
                    Errors.Add(ex.InnerException.Message);
                }

                if (ex.InnerException.InnerException != null)
                {
                    Errors.Add(ex.InnerException.InnerException.Message);
                }

                return View("Error", Errors);
            }

            //if code gets this far, return confirmation message
            return View("Confirm");
        }
    }
}
