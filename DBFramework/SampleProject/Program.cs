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

            Context<Books> context = new Context<Books>(connector);

            Books book = new Books();
            book.name = "test 10";

            //List<Books> books = context.getAll();
            context.add(book);
            //context.delete(2);

            //Books book = context.findById(1);

            //System.Console.WriteLine(book.id + " " + book.name);
        }
    }
}
