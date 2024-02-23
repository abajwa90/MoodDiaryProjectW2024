using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MoodDiaryProjectW2024.Models;
using System.Diagnostics;

namespace MoodDiaryProjectW2024.Controllers
{
    public class MoodDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/MoodData/ListMoods
        [HttpGet]
        public IEnumerable<MoodDto> ListMoods()
        {
            List<Mood> Moods = db.Moods.ToList();
            List<MoodDto> MoodDtos = new List<MoodDto>();

            Moods.ForEach(m => MoodDtos.Add(new MoodDto()
            {
                MoodId = m.MoodId,
                MoodNum = m.MoodNum,
                MoodDay = m.MoodDay,
                DiaryId = m.DiaryId
            }));
            return MoodDtos;
        }

        // GET: api/MoodData/FindMood/4
        [ResponseType(typeof(Mood))]
        [HttpGet]
        [Route("api/MoodData/FindMood/{id}")]

        public IHttpActionResult FindMood(int id)
        {
            Mood Mood = db.Moods.Find(id);
            MoodDto MoodDto = new MoodDto()
            {
                MoodId = Mood.MoodId,
                MoodNum = Mood.MoodNum,
                MoodDay = Mood.MoodDay,
                DiaryId = Mood.DiaryId
            };
            if (Mood == null)
            {
                return NotFound();
            }

            return Ok(MoodDto);
        }

        // POST: api/MoodData/UpdateMood/2
        [ResponseType(typeof(void))]
        [HttpPost]
        [Route("api/MoodData/UpdateMood/{id}")]

        public IHttpActionResult UpdateMood(int id, Mood Mood)
        {
            Debug.WriteLine("Update Mood Error");// help to isolate errors when running api
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model State is invalid");
                return BadRequest(ModelState);
            }

            if (id != Mood.MoodId)
            {
                Debug.WriteLine("Mood ID mismatch");
                Debug.WriteLine("GET parameter" + id);
                Debug.WriteLine("POST parameter" + Mood.MoodId);
                return BadRequest();
            }

            db.Entry(Mood).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MoodExists(id))
                {
                    Debug.WriteLine("Mood entry not found");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            Debug.WriteLine("None of the conditions triggered");
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/MoodData/AddMood
        [ResponseType(typeof(Mood))]
        [HttpPost]
        [Route("api/MoodData/AddMood")]

        public IHttpActionResult AddMood(Mood Mood)
        {
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model State is invalid");
                return BadRequest(ModelState);
            }

            db.Moods.Add(Mood);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Mood.MoodId }, Mood);
        }

        // POST: api/MoodData/DeleteMood/3
        [ResponseType(typeof(Mood))]
        [HttpPost]
        [Route("api/MoodData/DeleteMood/{id}")]

        public IHttpActionResult DeleteMood(int id)
        {
            Mood Mood = db.Moods.Find(id);
            if (Mood == null)
            {
                Debug.WriteLine("Mood not found.");
                return NotFound();
            }

            db.Moods.Remove(Mood);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MoodExists(int id)
        {
            return db.Moods.Count(e => e.MoodId == id) > 0;
        }
    }
}
