namespace RepairRequestDB
{
    public class NameProcAttribute: Attribute
    {
        public string Name { get; }

        public NameProcAttribute(string name)
        {
            Name = name;
        }
    }
}
