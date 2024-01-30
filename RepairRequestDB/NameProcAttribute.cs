namespace RepairRequestDB
{
    public class NameProcAttribute: Attribute
    {
        public string NameGet { get; }
        public string NameSet { get; }

        public NameProcAttribute(string nameGet, string nameSet)
        {
            NameGet = nameGet;
            NameSet = nameSet;
        }
    }
}
