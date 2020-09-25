using System;
using System.ComponentModel.DataAnnotations;

namespace OMSDataService.DomainObjects.Models
{
    public class Note
    {
        [Key]
        public int NoteID { get; set; }
        public int? AccountID { get; set; }
        public int? AdvisorID { get; set; }
        public string NoteText { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? ReminderDate { get; set; }
        public bool? Reminder { get; set; }
        public int? NotesActivityTypeID { get; set; }
        public int? NotesPriorityTypeID { get; set; }
        public int? NotesStatusTypeID { get; set; }
        public bool IsActive { get; set; }
        public DateTime AddDate { get; set; }
        public string AddUserID { get; set; }
        public DateTime ChgDate { get; set; }
        public string ChgUserID { get; set; }
    }
}
