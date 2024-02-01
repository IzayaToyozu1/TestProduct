using WorkerSQL;

namespace RepairRequestDB.Model
{
    [NameProc("getStatus", "", "Repair")]
    public class StatusRequest
    {
        public int Id { get; set; }

        [NameFieldDB("cName")] public string Name { get; set; }
    }
}
