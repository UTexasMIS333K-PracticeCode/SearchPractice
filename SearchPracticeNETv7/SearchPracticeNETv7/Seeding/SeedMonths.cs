using System;
using System.Text;
using SearchPracticeNETv7.DAL;
using SearchPracticeNETv7.Models;

namespace SearchPracticeNETv7.Seeding
{
    public static class SeedMonths
    {
        public static void SeedAllMonths(AppDbContext db)
        {
            List<Month> AllMonths = new List<Month>();

            //Create all the months and add them to the list
            AllMonths.Add(new Month() { MonthName = "January" });
            AllMonths.Add(new Month() { MonthName = "February" });
            AllMonths.Add(new Month() { MonthName = "March" });
            AllMonths.Add(new Month() { MonthName = "April" });
            AllMonths.Add(new Month() { MonthName = "May" });
            AllMonths.Add(new Month() { MonthName = "June" });
            AllMonths.Add(new Month() { MonthName = "July" });
            AllMonths.Add(new Month() { MonthName = "August" });
            AllMonths.Add(new Month() { MonthName = "September" });
            AllMonths.Add(new Month() { MonthName = "October" });
            AllMonths.Add(new Month() { MonthName = "November" });
            AllMonths.Add(new Month() { MonthName = "December" });

            //create some counters to help us debug
            String strMonth = "Begin";

            //adding stuff to the database could cause problems, so we need to
            //wrap this code in a try/catch block

            try
            {
                foreach (Month seedMonth in AllMonths)
                {
                    //set flag value
                    strMonth = seedMonth.MonthName;

                    //try to see if the Month is in the database
                    Month dbMonth = db.Months.FirstOrDefault(m => m.MonthName == seedMonth.MonthName);

                    //if dbMonth is null, that means this record hasn't been added yet
                    if (dbMonth == null)
                    {
                        //Add the month to the database
                        db.Months.Add(seedMonth);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                //build a message about what is going on
                StringBuilder msg = new StringBuilder();
                msg.Append("There was a problem adding ");
                msg.Append(strMonth);
                msg.Append("to the database.  Please try again.");

                //throw an exception if something is wrong
                throw new Exception(msg.ToString(), ex);
            }

        }
    }
}
