using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Market.Models;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Market.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // атрибут
    public class BooksController : ControllerBase
    {
        BooksContext db;
        public BooksController(BooksContext context)
        {
            db = context;
            if (!db.Books.Any())
            {
                db.Books.Add(new Book { Title = "Math", Genre = "Education" });
                db.Books.Add(new Book { Title = "Romeo and Juliet", Genre = "Tragedy" });
                db.SaveChanges();
            }
        }


        // Если запрос Get содержит параметр id (идентификатор объекта), то он обрабатывается другим методом - Get(int id),
        // который возвращает объект по переданному id.

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> Get()
        {
            return await db.Books.ToListAsync();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> Get(int id)
        {
            Book book = await db.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (book == null)
                return NotFound();
            return new ObjectResult(book);
        }

        // Запросы типа Post обрабатываются методом Post(Book book), который получает из тела запроса отправленные данные и добавляет их в базу данных.

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<ActionResult<Book>> Post(Book book)
        {
            if (book == null)
            {
                return BadRequest();
            }
            db.Books.Add(book);
            await db.SaveChangesAsync();
            return Ok();
        }

        // Метод Put(Book book) обрабатывает запросы типа Put - получает данные из запроса и изменяет ими объект в базе данных.

        // PUT api/<ValuesController>/5
        [HttpPut]
        public async Task<ActionResult<Book>> Put(Book book)
        {
            if (book == null)
            {
                return BadRequest();
            }
            if (!db.Books.Any(x => x.Id ==book.Id))
            {
                return NotFound();
            }
            db.Update(book);
            await db.SaveChangesAsync();
            return Ok(book);
        }

        // метод Delete(int id) обрабатывает запросы типа Delete, то есть запросы на удаление - получает из запроса параметр id
        // и по данному идентификатору удаляет объект из БД.

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> Delete(int id)
        {
            Book book = db.Books.FirstOrDefault(x => x.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            db.Books.Remove(book);
            await db.SaveChangesAsync();
            return Ok(book);
        }
    }
}
