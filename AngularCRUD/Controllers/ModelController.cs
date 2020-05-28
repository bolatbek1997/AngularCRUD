using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularCRUD.Models;
using Microsoft.AspNetCore.Mvc;

namespace AngularCRUD.Controllers
{
    [Route("api/[controller]")]
    public class ModelController : Controller
    {
        //ApplicationContext db;
        List<ModelCRUD> db = new List<ModelCRUD>();

        public ModelController()
        {
          
                db.Add(new ModelCRUD { Id = 1, Name = "1" });
                db.Add(new ModelCRUD { Id = 2, Name = "2" });
                db.Add(new ModelCRUD { Id = 3, Name = "3" });
           
        }
        [HttpGet]
        public IEnumerable<ModelCRUD> Get()
        {
            return db.ToList();
        }

        [HttpGet("{id}")]
        public ModelCRUD Get(int id)
        {
            ModelCRUD ModelCRUD = db.FirstOrDefault(x => x.Id == id);
            return ModelCRUD;
        }

        [HttpPost]
        public IActionResult Post(ModelCRUD ModelCRUD)
        {
            if (ModelState.IsValid)
            {
                db.Add(ModelCRUD);            
                return Ok(ModelCRUD);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public IActionResult Put(ModelCRUD ModelCRUD)
        {
            if (ModelState.IsValid)
            {
                db.FirstOrDefault(x=>x.Id== ModelCRUD.Id);
                return Ok(ModelCRUD);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            ModelCRUD ModelCRUD = db.FirstOrDefault(x => x.Id == id);
            if (ModelCRUD != null)
            {
                db.Remove(ModelCRUD);
            }
            return Ok(ModelCRUD);
        }
    }
}
