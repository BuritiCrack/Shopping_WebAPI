﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingWebAPI.DAL;
using ShoppingWebAPI.DAL.Entities;

namespace ShoppingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // todo controlador debe llear este datanotation por default
    public class CountriesController : ControllerBase
    {
        private readonly DataBaseContext _context;


        public CountriesController(DataBaseContext context)
        {
            _context = context;  // el _Context nos trae por debajo toda la conexion a la BD
        }

        // Ahora vamos a crear los metodos que en este contexto de basde de datos
        // llamaremos acciones de nuestra base de datos (get,post,put,delete y el patch)

        // Iniciaremos con el GET para traernos todos los paises de nuestra base de datos

        [HttpGet]
        [Route("Get")]

        public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
        {
            var countries = await _context.categories.ToListAsync();

            if (countries == null) return NotFound();

            return countries;
        }

        // Esta nueva accion es para traerme un pais en particular por id

        [HttpGet, ActionName("Get")]
        [Route("Get/{id}")]

        public async Task<ActionResult<Country>> GetCountryById(Guid? id)
        {
            var country = await _context.categories.FirstOrDefaultAsync(c => c.Id == id);

            if (country == null) return NotFound();

            return Ok(country);
        }

        // Accion para agregar o crear un nuevo país

        [HttpPost, ActionName("Create")]
        [Route("Create")]

        public async Task<ActionResult> CreateCountry(Country country) 
        {
            try
            {
                country.Id = Guid.NewGuid();
                country.CreatedDate = DateTime.Now;

                _context.categories.Add(country);
                await _context.SaveChangesAsync(); // Aquí es donde se hace el insert

            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("Duplicate"))
                    return Conflict(String.Format("{0} ya existe", country.Name));
            }
            catch (Exception ex) 
            {
                return Conflict(ex.Message);
            }

            return Ok(country);
           
        }

        // Accion editar elemento 

        [HttpPut, ActionName("Edit")]
        [Route("Edit/{id}")]

        public async Task<ActionResult> EditCountry(Guid? id, Country country)
        {
            try
            {
                if (id != country.Id) return NotFound("Country no found");

                country.ModifiedDate = DateTime.Now;

                _context.categories.Update(country);
                await _context.SaveChangesAsync(); // Aquí es donde se hace el update

            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException.Message.Contains("Duplicate"))
                    return Conflict(String.Format("{0} ya existe", country.Name));
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }

            return Ok(country);

        }

        // Accion eliminar Elemento 

        [HttpDelete, ActionName("Delete")]
        [Route("Delete/{id}")]
        public async Task<ActionResult> DeleteCountry(Guid? id)
        {
            if (_context.categories == null) return Problem("Entity set 'DataBaseContext.categories' is null");
            var country = await _context.categories.FirstOrDefaultAsync(c => c.Id == id);

            if (country == null) return NotFound("Country no found");  // or NotFound("Country no found");

            _context.categories.Remove(country);
            await _context.SaveChangesAsync(); // Aquí se le hace el delete..

            return Ok(String.Format("El pais {0} fue eliminado!! ",country.Name));
        }
    }
}
