using System.ComponentModel.DataAnnotations.Schema;

namespace RepairRequestDB.Model
{
    [NameProc("", "")]
    [NameScheme("")]
    public class RequestRepair
    {
        public int Id { get; set; }
        public int? Number { get; set; }
        public DateTime DateSubmission { get; set; }
        public int id_Submission { get; set; }
        public string Cabinet { get; set; }
        public string FIO { get; set; }
        public int id_Hardware { get; set; }
        public string Fault { get; set; }
        public int Status { get; set; }
        public int? id_Start { get; set; }
        public DateTime? DataStart { get; set; }
        public int? id_End { get; set; }
        public DateTime? DateEnd { get; set; }
        public int? id_Confirm { get; set; }
        public DateTime? DateConfirm { get; set; }
        public string? ip_address { get; set; }
        public int? id_Responsible { get; set; }
        public string StartName { get; set; }
        public string EndName { get; set; }
        public string ConfirmName { get; set; }
        public string FIOsub { get; set; }
        public string cName { get; set; }
        public string? name_comp { get; set; }
        public string resName { get; set; }
        public string strComment { get; set; }
        public string nameStatus { get; set; }
        public string? countHour { get; set; }
        public int countHourX { get; set; }
        public int countHourY { get; set; }
        public string listIdObject { get; set; }
        public string nameObject { get; set; }
        public string nameMark { get; set; }
        public int? R { get; set; }
        public int? G { get; set; }
        public int? B { get; set; }
        public int IdMark { get; set; }
        public int TypeService { get; set; }
        public int CountDoc { get; set; }
        public int? DevId { get; set; }
        public string? Dev { get; set; }
        public string? TypeDev { get; set; }
        public int? TypeDevId { get; set; }
        public bool IsGK { get; set; }
        public int? IdStage { get; set; }
        public string? StageName { get; set; }
        public int IdPriority { get; set; }
        public string? PriorityName { get; set; }
    }
}
