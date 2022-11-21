using System;
using System.Collections.Generic;
using System.Text;

namespace HRJ.LMS.Domain
{
    public class LeadDashLock_Form
    {
        public Guid Id { get; set; }
        //public DateTime Date { get; set; }
        public DateTime LeadCreatedAt { get; set; }

        public int Leadsource_Id { get; set; }
        public string location_id { get; set; }//4

        public string location_external_id { get; set; }//5

        public string location_name { get; set; }//6

        public string locality { get; set; }//7

        public string city { get; set; }//8

        public string state { get; set; }//9

        public string pincode { get; set; }//10

        public string lead_date { get; set; }//13
        public string type { get; set; }//14

        public string name { get; set; } //1

        public string email { get; set; }//2

        public string mobile { get; set; }//3

        public string description { get; set; }//11

        public string lead_type { get; set; }//12
        //Added on 18/11/2022
    }
}
