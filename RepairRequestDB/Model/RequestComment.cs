using RepairRequestDB;

namespace RepairRequestDB.Model
{
    [NameProc("GetCommentsById", "SetComment")]
    [NameScheme("Repair")]
    public class RequestComment
    {
        public DateTime DateCreate { get; set; }
        public string Comment { get; set; }
        public string FIO { get; set; }
    }
}
