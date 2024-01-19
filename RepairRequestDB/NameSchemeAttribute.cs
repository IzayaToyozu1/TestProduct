namespace RepairRequestDB
{
    public class NameSchemeAttribute: Attribute
    {
        public string Name { get; set; }

        public NameSchemeAttribute(string name)
        {
            Name = name;
        }
    }
}
