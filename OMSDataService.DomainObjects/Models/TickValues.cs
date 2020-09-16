using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OMSDataService.DomainObjects.Models
{
    public class TickValues
    {
        [Key]
        public int TickValueID { get; set; }
        public string Symbol { get; set; }
        public DateTime TickTime { get; set; }
        public float Value { get; set; }
    } 
}
