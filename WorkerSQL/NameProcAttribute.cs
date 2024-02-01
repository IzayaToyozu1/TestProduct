namespace WorkerSQL
{
    public class NameProcAttribute: Attribute
    {
        public string NameGet { get; }
        public string NameSet { get; }
        public string SchemeProcedure { get; }

        public NameProcAttribute(string nameGet, string nameSet, string schemeProcedure)
        {
            NameGet = nameGet;
            NameSet = nameSet;
            SchemeProcedure = schemeProcedure;
        }
    }
}
