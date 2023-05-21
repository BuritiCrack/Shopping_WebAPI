using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingWebAPI.DAL;
using ShoppingWebAPI.DAL.Entities;

namespace ShoppingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // todo controlador debe llear este datanotation por default
    public class CategoriesController : ControllerBase
    {
        private readonly DataBaseContext _context;


        public CategoriesController(DataBaseContext context)
        {
            _context = context;  // el _Context nos trae por debajo toda la conexion a la BD
        }

        // Ahora vamos a crear los metodos que en este contexto de basde de datos
        // llamaremos acciones de nuestra base de datos (get,post,put,delete y el patch)

        // Iniciaremos con el GET para traernos todos los paises de nuestra base de datos

        [HttpGet]
        [Route("Get")]

        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _context.Categories.ToListAsync();

            if (categories == null) return NotFound();

            return Ok(categories);
        }

        // Esta nueva accion es para traerme un pais en particular por id

        [HttpGet, ActionName("Get")]
        [Route("Get/{id}")]

        public async Task<ActionResult<Category>> GetCategoryById(Guid? id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category == null) return NotFound();

            return Ok(category);
        }

        // Accion para agregar o crear un nuevo país

        [HttpPost, ActionName("Create")]
        [Route("Create")]

        public async Task<ActionResult> CreateCategory(Category category) 
        {
            try
            {
                category.Id = Guid.NewGuid();
                category.CreatedDate = DateTime.Now;

                _context.Categories.Add(category);
                await _context.SaveChangesAsync(); // Aquí es donde se hace el insert

            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("Duplicate"))
                    return Conflict(String.Format("{0} ya existe", category.Name));
            }
            catch (Exception ex) 
            {
                return Conflict(ex.Message);
            }

            return Ok(category);
           
        }

        // Accion editar elemento 

        [HttpPut, ActionName("Edit")]
        [Route("Edit/{id}")]

        public async Task<ActionResult> EditCategory(Guid? id, Category category)
        {
            try
            {
                if (id != category.Id) return NotFound("Category no found");

                category.ModifiedDate = DateTime.Now;

                _context.Categories.Update(category);
                await _context.SaveChangesAsync(); // Aquí es donde se hace el update

            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("Duplicate"))
                    return Conflict(String.Format("{0} ya existe", category.Name));
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

            return Ok(category);

        }

        // Accion eliminar Elemento 

        [HttpDelete, ActionName("Delete")]
        [Route("Delete/{id}")]
        public async Task<ActionResult> DeleteCategory(Guid? id)
        {
            if (_context.categories == null) return Problem("Entity set 'DataBaseContext.categories' is null");
            var category = await _context.categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category == null) return NotFound("Category no found");  // or NotFound("Category no found");

            _context.categories.Remove(category);
            await _context.SaveChangesAsync(); // Aquí se le hace el delete..

            return Ok(String.Format("El pais {0} fue eliminado!! ",category.Name));
        }
    }
}
