using System;
using System.Collections.Generic;
using System.Text;

namespace HRJ.LMS.Domain
{
    public class LeadDashLock
    {
        public Guid Id { get; set; }
        
        
        public DateTime LeadCreatedAt { get; set; }

        public int Leadsource_Id { get; set; }

        //Added on 18/11/2022
        public string caller_number { get; set; } //1

        public string called_number { get; set; } //2

        public string routing_number { get; set; } //3
        public string location_id { get; set; }//4

        public string location_external_id { get; set; }//5

        public string location_name { get; set; }//6

        public string locality { get; set; }//7

        public string city { get; set; }//8

        public string state { get; set; }//9

        public string pincode { get; set; }//10

        public string lead_date { get; set; }//11
        public string type { get; set; }//12

        //Added on 18/11/2022
    }
}
