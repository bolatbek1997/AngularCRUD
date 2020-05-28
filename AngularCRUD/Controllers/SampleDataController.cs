using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularCRUD.Models;
using Microsoft.AspNetCore.Mvc;

namespace AngularCRUD.Controllers
{
    [Route("api/[controller]")]
    public class Model2Controller : Controller
    {
        ApplicationContext db;
        
        public Model2Controller(ApplicationContext context)
        {
            db = context;
            if (!db.Models.Any())
            {
                db.Models.Add(new ModelCRUD { Id=1, Name = "1"});
                db.Models.Add(new ModelCRUD { Id = 2, Name = "2" });
                db.Models.Add(new ModelCRUD { Id = 3, Name = "3" });
                db.SaveChanges();
            }
        }
        [HttpGet]
        public IEnumerable<ModelCRUD> Get()
        {
            return db.Models.ToList();
        }

        [HttpGet("{id}")]
        public ModelCRUD Get(int id)
        {
            ModelCRUD ModelCRUD = db.Models.FirstOrDefault(x => x.Id == id);
            return ModelCRUD;
        }

        [HttpPost]
        public IActionResult Post(ModelCRUD ModelCRUD)
        {
            if (ModelState.IsValid)
            {
                db.Models.Add(ModelCRUD);
                db.SaveChanges();
                return Ok(ModelCRUD);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public IActionResult Put(ModelCRUD ModelCRUD)
        {
            if (ModelState.IsValid)
            {
                db.Update(ModelCRUD);
                db.SaveChanges();
                return Ok(ModelCRUD);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            ModelCRUD ModelCRUD = db.Models.FirstOrDefault(x => x.Id == id);
            if (ModelCRUD != null)
            {
                db.Models.Remove(ModelCRUD);
                db.SaveChanges();
            }
            return Ok(ModelCRUD);
        }
    }
}
