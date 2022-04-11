using System;

namespace HRJ.LMS.Domain
{
    public class FiscalMonth
    {
        public int Id { get; set; }
        public int FiscalYearId { get; set; }
        public FiscalYear FiscalYear { get; set; }
        public string FiscalYearDuration { get; set; }
        public string FiscalMonthLabel { get; set; }
        public DateTime StartMonthDate { get; set; }
        public DateTime EndMonthDate { get; set; }

    }
}