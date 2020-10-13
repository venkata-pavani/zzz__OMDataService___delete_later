using System;
using System.ComponentModel.DataAnnotations;

namespace OMSDataService.DomainObjects.Models
{
    public class GridLayout
    {
        [Key]
        public int GridLayoutID { get; set; }
        public string GridName { get; set; }
        public string LayoutName { get; set; }
        public bool IsDefaultLayout { get; set; }
        public string LayoutDataObject { get; set; }
        public DateTime AddDate { get; set; }
        public string AddUserID { get; set; }
        public DateTime ChgDate { get; set; }
        public string ChgUserID { get; set; }
    }
}
