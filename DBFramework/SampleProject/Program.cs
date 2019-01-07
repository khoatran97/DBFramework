using DBFramework;

namespace SampleProject
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create new connector to connect with DB (Required)
            Connector connector = new Connector("DESKTOP-735FCOO\\SQLEXPRESS", "Library", true);
            DBContext.setConnector(connector);

            // Create new object
            dynamic book = Entity.instance.Books;
            book.Ten = "test test";
            book.TacGia = "Khoa Tran";
            book.NhaXuatBan = "Khoa Tran";
            book.NamXuatBan = 2018;
            
            // Use DBContext to do actions with DB
            DBContext.instance.Books.add(book);
            var books = DBContext.instance.Books.getAll();

            // Delete
            // DBContext.instance.Books.delete(1);
            // var booksafdl = DBContext.instance.Books.getAll();
        }
    }
}
