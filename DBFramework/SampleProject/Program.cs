using DBFramework;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;
using System.Collections.Generic;

namespace SampleProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Connector connector = new Connector("DESKTOP-Q1IUNEN\\XUANNAM", "Library", false, "sa", "123456");
            Entity.setConnector(connector);
            object book = Entity.instance.Books;

            Context<Books> context = new Context<Books>(connector);

            List<Books> books = context.getAll(book);
            //context.add();
        }
    }
}
