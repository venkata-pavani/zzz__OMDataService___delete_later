using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OMSDataService.DomainObjects.Models
{
    public class Month
    {
        [Key]
        public int MonthID { get; set; }
        public string MonthCode { get; set; }
        public string MonthName { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
        public DateTime AddDate { get; set; }
        public int AddUserId { get; set; }
        public DateTime ChgDate { get; set; }
        public int ChgUserId { get; set; }
    }
}
