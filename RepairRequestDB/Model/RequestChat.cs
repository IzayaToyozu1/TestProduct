using WorkerSQL;

namespace RepairRequestDB.Model
{
    [NameProc("", "", "")]
    public class RequestChat
    {
        public int Id { get; set; }

        public RequestMessage[] Message { get; set; }
    }
}
