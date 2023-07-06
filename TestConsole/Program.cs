using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using ClassLibrary1;
using Microsoft.IdentityModel.Tokens;

namespace TestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
        }
    }

    public class ClassTest
    {
        public string Name { get; set; }
    }

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

    //public class Person
    //{
    //    public int Id { get; set; }

    //    public string? Name { get; set; }

    //    public long Age { get; set; }

    //    public int? PositionInfoKey { get; set; }

    //    [ForeignKey("PositionInfoKey")]
    //    public Position? Position { get; set; }

    //    public override string ToString()
    //    {
    //        return $"Name: {Name} Age: {Age}, Position: {Position}";
    //    }
    //}

    //public class Position
    //{
    //    public int Id { get; set; }
    //    public string? Name { get; set; }
    //    public List<Person> Persons { get; set; } = new();

    //    public override string ToString()
    //    {
    //        return $"{Name}";
    //    }
    //}

}