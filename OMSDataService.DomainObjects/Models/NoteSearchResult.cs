using System;

namespace OMSDataService.DomainObjects.Models
{
    public class NoteSearchResult
    {
        public int NoteID { get; set; }
        public int? AccountID { get; set; }
        public int? AdvisorID { get; set; }
        public string AdvisorName { get; set; }
        public string NoteText { get; set; }
        public int? NotesActivityTypeID { get; set; }
        public string NotesActivityType { get; set; }
        public int? NotesPriorityTypeID { get; set; }
        public string NotesPriority { get; set; }
        public int? NotesStatusTypeID { get; set; }
        public string NotesStatus { get; set; }
        public DateTime AddDate { get; set; }
        public string AddUserID { get; set; }
        public DateTime ChgDate { get; set; }
        public string ChgUserID { get; set; }
    }
}
