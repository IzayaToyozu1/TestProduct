using System.ComponentModel.DataAnnotations.Schema;

namespace TestProduct.Model
{
    [Table("j_RequestRepair", Schema = "Repair")]
    public class RequestRepair
    {
        public int id { get; set; }
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
        public string? name_comp { get; set; }
        public int? id_Object { get; set; }
        public int TypeService { get; set; }
        public int? id_Device { get; set; }
        public bool isGK { get; set; }
        public int? id_StageRequests { get; set; }
        public int id_Priority { get; set; }

        [ForeignKey("id_Start")]
        public User User1 { get; set; }

        [ForeignKey("id_Confirm")]
        public User User2 { get; set; }

        [ForeignKey("id_End")]
        public User User3 { get; set; }

        [ForeignKey("id_Submission")]
        public User User4 { get; set; }

        public override string ToString()
        {
            return $"Заявка под номером {Number}; User1 {User1}; User2 {User2}; User3 {User3}; User4 {User4}";
        }
    }
}
