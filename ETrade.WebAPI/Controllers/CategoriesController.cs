﻿using ETrade.DAL.Abstract;
using ETrade.Entity.Models.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ETrade.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        ICategoryDAL _categoryDAL;

        public CategoriesController(ICategoryDAL categoryDAL)
        {
            this._categoryDAL = categoryDAL;
        }

        // GET: api/<CategoriesController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_categoryDAL.GetAll());
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0 || _categoryDAL.GetAll() == null)
            {
                return BadRequest("Kategori bilgisi alınamadı.");
            }
            var category = _categoryDAL.Get(id);
            if (category == null)
            {
                return NotFound("Kategori Bulunamadı.");
            }
            return Ok(category);
        }

        // POST api/<CategoriesController>
        [HttpPost]
        public IActionResult Post([FromBody] Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryDAL.Add(category);
                return Ok(category);
                //return CreatedAtAction("Get", new {id=category.Id}, category);
            }
            return BadRequest();
           
        }

        // PUT api/<CategoriesController>/5
        [HttpPut]
        public IActionResult Put([FromBody] Category category)
        {
            var model = _categoryDAL.Get(category.Id);
            model.Name = category.Name;
            model.Description = category.Description;
            if (ModelState.IsValid)
            {
                _categoryDAL.Update(category);
                return Ok(model);
               
            }
            return BadRequest();
        }

        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var category = _categoryDAL.Get(id);
            if (category == null)
                return NotFound();
            _categoryDAL.Delete(category);
            return Ok (category); // herhangi bir sayfaya actiona yönlendirme yapmak istemiyorsak ok yazabiliriz.
        }
    }
}
