using System;
//add this if you want to add display name annotation to enum values below
using System.ComponentModel.DataAnnotations;

namespace SearchPracticeNETv7.Models
{
    public enum Classification { All, Freshman, Sophomore, Junior, Senior }
    public enum SortOrder { Ascending, Descending }
    public enum Major
    {
        [Display(Name = "Accounting")] Accounting,
        [Display(Name = "Business Analytics")] BusinessAnalytics,
        [Display(Name = "Canfield Business Honors")] CBHP,
        [Display(Name = "Finance")] Finance,
        [Display(Name = "International Business")] IB,
        [Display(Name = "Management")] Management,
        [Display(Name = "Marketing")] Marketing,
        [Display(Name = "Mgmt Info Systems")] MIS,
        [Display(Name = "Supply Chain Mgmt")] SCM
    }
    public class SearchViewModel
    {
        [Display(Name = "Name:")]
        public String SearchName { get; set; }

        [Display(Name = "Select a Classification:")]
        public Classification SelectedClass { get; set; }

        [Display(Name = "Select a month:")]
        public Int32 SelectedMonthID { get; set; }

        //? means it is??
        [Display(Name = "Enter desired GPA:")]
        [Range(minimum: 0, maximum: 4, ErrorMessage = "GPA must be between 0.0 and 4.0")]
        public Decimal? SearchGPA { get; set; }

        [Display(Name = "Select a starting date:")]
        [DataType(DataType.Date)]
        //DateTime?  means this date is nullable - we want to allow them to 
        //be able to NOT select a date
        public DateTime? SelectedDate { get; set; }

        [Display(Name = "Out of state?")]
        public Boolean OutOfState { get; set; }

        [Display(Name = "Select a major:")]

        //This is nullable so they can select the "All Majors" option that doesn't exist in the enum
        //otherwise it would default to first option
        public Major? SelectedMajor { get; set; }

    }
}
