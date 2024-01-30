namespace RepairRequestDB.Model
{
    [NameScheme("Repair")]
    [NameProc("", "")]
    public class RequestChat
    {
        public int Id { get; set; }

        public RequestMessage[] Message { get; set; }
    }
}
