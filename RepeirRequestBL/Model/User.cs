using System.ComponentModel.DataAnnotations.Schema;

namespace TestProduct.Model
{
    [Table("ListUsers", Schema = "dbo")]
    public class User
    {
        public int id { get; set; }

        public int id_Access { get; set; }

        public string FIO { get; set; }

        public int? id_Departments { get; set; }

        public int? id_Posts { get; set; }

        public DateTime? DateUnemploy { get; set; }

        public bool? IsActive { get; set; }

        public int? id_rang { get; set; }

        public int Priveledge { get; set; }

        public override string ToString()
        {
            return FIO;
        }
    }
}
