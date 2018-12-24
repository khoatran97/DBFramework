using DBFramework;

namespace SampleProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Connector connector = new Connector("DESKTOP-735FCOO\\SQLEXPRESS", "Library", true);
            Entity.setConnector(connector);
            object book = Entity.instance.Books;
        }
    }
}
