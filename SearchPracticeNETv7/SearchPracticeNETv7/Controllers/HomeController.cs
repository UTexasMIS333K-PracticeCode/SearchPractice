using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SearchPracticeNETv7.DAL;
using SearchPracticeNETv7.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SearchPracticeNETv7.Controllers
{
    public class HomeController : Controller
    {
        //Create an instance of the db_context
        private AppDbContext _context;

        //Create the constructor so that we get an instance of AppDbContext
        public HomeController(AppDbContext dbContext)
        {
            _context = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult DetailedSearch()
        {
            //Populate view bag with output of Get... method list of months (item called AllMonths) using the method
            //this will be needed anytime you need to populate from navigational property
            //what is navigational property in HW3?
            //you'll need a method for ANY navigational prop that is a search input
            ViewBag.AllMonths = GetAllMonthsSelectList();

            //Set default properties by creating new instance of svm
            SearchViewModel svm = new SearchViewModel();
            //all classifications is selected by default
            svm.SelectedClass = Classification.All;
            //all months is selected by default
            svm.SelectedMonthID = 0;

            return View(svm);
        }

        public ActionResult SearchResults(SearchViewModel svm)
        {
            //*************************************************************************************
            //Code for string result
            if (svm.SearchName != null && svm.SearchName != "") //user entered something
            {
                //In this example, we are just showing the output.
                //In a real search, you would put a query here that 
                //selects records that match the name
                ViewBag.SearchName = "The name search string is: " + svm.SearchName;
            }
            //*************************************************************************************
            //code for searching GPA
            if (svm.SearchGPA != null)//they searched for something
            {
                TryValidateModel(svm);
                if (ModelState.IsValid == false)
                {
                    ViewBag.GPAMessage = svm.SearchGPA + " is not a valid number. Please try again.";
                    //re-populate ViewBag to have list of all months
                    ViewBag.AllMonths = GetAllMonthsSelectList();
                    return View("DetailedSearch", svm);
                }

                //In this example, we are just displaying search criteria
                //In a real search, you would add a query to limit by decGPASearch
                Decimal decUpdatedGPA = svm.SearchGPA ?? 0;
                @ViewBag.SearchGPA = "The desired GPA is " + decUpdatedGPA.ToString("n2");
            }

            //*************************************************************************************
            //Code for date
            if (svm.SelectedDate != null)//They selected a date
            {
                //In this example, we are just displaying search criteria
                //In a real search, you would add a query to search by date
                ViewBag.SelectedDate = "The value you selected for date is " + svm.SelectedDate.ToString();
            }

            //*************************************************************************************
            //Code for checkbox 
            //In this example, we are just displaying search criteria
            //In a real search, you would query here for the values you want for OutOfState
            ViewBag.OutOfState = "The value you selected for 'Out of State' is: " + svm.OutOfState.ToString();

            //*************************************************************************************
            //Code for radio buttons
            switch (svm.SelectedClass)
            {
                case Classification.Freshman:
                    //In this example, we are just showing the output
                    //In a real search, you would put a query here that selects Freshmen
                    ViewBag.SelectedClassification = "The selected classification is: Freshman";
                    break;
                case Classification.Sophomore:
                    //In this example, we are just showing the output
                    //In a real search, you would put a query here that selects Sophomores
                    ViewBag.SelectedClassification = "The selected classification is: Sophomore";
                    break;
                case Classification.Junior:
                    //In this example, we are just showing the output
                    //In a real search, you would put a query here that selects Juniors
                    ViewBag.SelectedClassification = "The selected classification is: Junior";
                    break;
                case Classification.Senior:
                    //In this example, we are just showing the output
                    //In a real search, you would put a query here that selects Seniors
                    ViewBag.SelectedClassification = "The selected classification is: Senior";
                    break;
                default: //they didn't pick any of the "real" classifications
                    //They didn't select anything, so you can leave this section blank
                    break;
            } //end of select case

            //*************************************************************************************
            //Code for drop-down list with enum
            if (svm.SelectedMajor != null)
            {
                //In this example, we are just displaying the search criteria
                //In a real search, you would put a query here that selects records
                //with the same value as the enum
                ViewBag.SelectedMajor = "The value you selected for major is: " + svm.SelectedMajor;
            }

            //*************************************************************************************
            //Code for drop-down list with property
            //Selected month is the selected value from the dropdown
            if (svm.SelectedMonthID != 0) //they picked a month
            {
                //In this example, we are just displaying the search criteria
                //In a real search, you would put a query here that selects records
                //with the same MonthID
                //Looking up the month is only necessary because we want to display the month name
                //In a search you can query records with just the ID value
                Month MonthToDisplay = _context.Months.Find(svm.SelectedMonthID);
                ViewBag.SelectedMonth = "The selected month is: " + MonthToDisplay.MonthName;
            }

            //go to the search view
            //in a 'real' search, you would execute the query here and pass the selected records to the view
            return View("SearchResults");
        }
        //SelectList is the object you need to create drop down
        private SelectList GetAllMonthsSelectList()
        {
            //Get the list of months from the database (Select *)  
            //This would be Book in HW3
            List<Month> monthList = _context.Months.ToList();

            //add a dummy entry so the user can select all months
            //type of Month class; name SelectNone -- creating a fake month
            //Don't want this in the DB, but needs to be in the drop down
            //code below creates new instance of Month Class, and add new item and set two properties

            //What happens if I choose MonthID that already exists?
            Month SelectNone = new Month() { MonthID = 0, MonthName = "All Months" };

            //incrementally added this to monthList 
            monthList.Add(SelectNone);

            //convert the list to a SelectList by calling SelectList constructor (create new instance of SelectList)
            //displaying monthList sorted by ID, next is name from the PK of the model class (this is the value in the http source code --> it MUST be pk), 3rd item is what should actually be displayed --> both need to match properties from the model class EXACTLY  
            //MonthID and MonthName are the names of the properties on the Month class
            //MonthID is the primary key

            //other examples of searching on name, might have to create property to concat first+last name to then use as property to display for search

            //what happens when you don't get names correct?
            SelectList monthSelectList = new SelectList(monthList.OrderBy(m => m.MonthID), "MonthID", "MonthName");

            //return the SelectList
            return monthSelectList;
        }
    }
}