using System;

namespace OMSDataService.DomainObjects.Models
{
    public class ContractExportStatusType
    {
        public int ContractExportStatusTypeID { get; set; }
        public string ContractExportStatusTypeName { get; set; }
        public string Description { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
        public DateTime AddDate { get; set; }
        public int AddUserID { get; set; }
        public DateTime ChgDate { get; set; }
        public int ChgUserID { get; set; }
    }
}
