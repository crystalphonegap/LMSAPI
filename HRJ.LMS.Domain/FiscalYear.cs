using System;
using System.Collections.Generic;

namespace HRJ.LMS.Domain
{
    public class FiscalYear
    {
        public int Id { get; set; }
        public string FiscalYearDuration { get; set; }
        public DateTime StartYearDate { get; set; }
        public DateTime EndYearDate { get; set; }
        public bool IsActive { get; set; }
        public List<FiscalMonth> FiscalMonths { get; set; }
    }
}