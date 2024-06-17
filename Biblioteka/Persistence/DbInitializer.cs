using Biblioteka.Model;
using Biblioteka.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Biblioteka.Persistence
{
    public class DbInitializer : CreateDatabaseIfNotExists<LibraryContext>
    {
        protected override void Seed(LibraryContext context)
        {
            // Seed initial data for categories
            var categories = new List<BookCategory>
            {
                new BookCategory { Name = "Horror" },
                new BookCategory { Name = "Science Fiction" },
                new BookCategory { Name = "Fantasy" },
                new BookCategory { Name = "Mystery" }
            };
            categories.ForEach(c => context.BookCategories.Add(c));
            context.SaveChanges();

            // Seed initial data for books
            var books = new List<Book>
            {
                new Book { Title = "IT", Author = "Stephen King", PublishedDate = new DateTime(1986, 9, 15), CategoryId = categories[0].Id },
                new Book { Title = "Dune", Author = "Frank Herbert", PublishedDate = new DateTime(1965, 8, 1), CategoryId = categories[1].Id },
                new Book { Title = "Harry Potter and the Philosopher's Stone", Author = "J.K. Rowling", PublishedDate = new DateTime(1997, 6, 26), CategoryId = categories[2].Id },
                new Book { Title = "The Da Vinci Code", Author = "Dan Brown", PublishedDate = new DateTime(2003, 4, 1), CategoryId = categories[3].Id }
            };
            books.ForEach(b => context.Books.Add(b));
            context.SaveChanges();
        }
    }
}
