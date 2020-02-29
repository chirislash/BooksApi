using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using BooksApi.Models;

namespace BooksApi.Services
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _books;

        public BookService(IBookstoreDatabaseSettings settings) {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _books = database.GetCollection<Book>(settings.BooksCollectionName);
        }

        public List<Book> Get() =>
        _books.Find(book => true).ToList();

        public Book Get(String Id) =>
            _books.Find<Book>(book => book.Id == Id).FirstOrDefault();

        public Book Create(Book book) {
            _books.InsertOne(book);
            return book;
        } 

        public void Update(String Id, Book BookIn) =>
            _books.ReplaceOne(book => book.Id == Id, BookIn);

        public void Remove(Book BookIn) =>
            _books.DeleteOne(book => book.Id == BookIn.Id);

        public void Remove(String Id) =>
            _books.DeleteOne(book => book.Id == Id);
        
    }
}
