using System;
using System.ComponentModel.DataAnnotations;

namespace OMSDataService.DomainObjects.Models
{
    public class NotesStatusType
    {
        [Key]
        public int NotesStatusTypeID { get; set; }
        public string NotesStatusTypeName { get; set; }
        public bool IsDefault { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
        public DateTime AddDate { get; set; }
        public int AddUserID { get; set; }
        public DateTime ChgDate { get; set; }
        public int ChgUserID { get; set; }
    }
}
