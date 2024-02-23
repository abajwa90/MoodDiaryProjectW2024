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
    public class DiaryDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/DiaryData/ListDiaries
        [HttpGet]
        public IHttpActionResult ListDiaries()
        {
            List<Diary> Diaries = db.Diaries.ToList();
            List<DiaryDto> DiaryDtos = new List<DiaryDto>();

            Diaries.ForEach(d => DiaryDtos.Add(new DiaryDto()
            {
                DiaryId = d.DiaryId,
                DiaryName = d.DiaryName,
                DiaryCreation = d.DiaryCreation,
                DiaryMood = d.DiaryMood,
                DiaryWeather = d.DiaryWeather,
                DiaryNotes = d.DiaryNotes
            }));
            return Ok(DiaryDtos);
        }
        
        // GET: api/DiaryData/FindDiary/7
        [ResponseType(typeof(Diary))]
        [HttpGet]
        [Route("api/DiaryData/FindDiary/{id}")]
        public IHttpActionResult FindDiary(int id)
        {
            Diary Diary = db.Diaries.Find(id);
            DiaryDto DiaryDto = new DiaryDto()
            {
                DiaryId = Diary.DiaryId,
                DiaryName = Diary.DiaryName,
                DiaryCreation = Diary.DiaryCreation,
                //DiaryMood = Diary.DiaryMood,
                //DiaryWeather = Diary.DiaryWeather
            };
            if (Diary == null)
            {
                return NotFound();
            }

            return Ok(DiaryDto);
        }

        // POST: api/DiaryData/UpdateDiary/7
        [ResponseType(typeof(void))]
        [HttpPost]
        [Route("api/DiaryData/UpdateDiary/{id}")]
        public IHttpActionResult UpdateDiary(int id, Diary Diary)
        {
            Debug.WriteLine("Update Diary Error");// help to isolate errors when running api
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model State is invalid");
                return BadRequest(ModelState);
            }

            if (id != Diary.DiaryId)
            {
                Debug.WriteLine("Diary ID mismatch");
                Debug.WriteLine("GET parameter" + id);
                Debug.WriteLine("POST parameter" + Diary.DiaryId);
                return BadRequest();
            }

            db.Entry(Diary).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiaryExists(id))
                {
                    Debug.WriteLine("Diary not found");
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

        // POST: api/DiaryData/CreateDiary
        [ResponseType(typeof(Diary))]
        [HttpPost]
        [Route("api/DiaryData/AddDiary")]
        public IHttpActionResult AddDiary(Diary Diary)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Diaries.Add(Diary);
            db.SaveChanges();// error encountered when trying to add new diary (sql inner exception, unable to convert datetime2 datatype to datetime)

            return CreatedAtRoute("DefaultApi", new { id = Diary.DiaryId }, Diary);
        }

        // POST: api/DiaryData/DeleteDiary/2
        [ResponseType(typeof(Diary))]
        [HttpPost]
        [Route("api/DiaryData/DeleteDiary/{id}")]
        public IHttpActionResult DeleteDiary(int id)
        {
            Diary Diary = db.Diaries.Find(id);
            if (Diary == null)
            {
                return NotFound();
            }

            db.Diaries.Remove(Diary);
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

        private bool DiaryExists(int id)
        {
            return db.Diaries.Count(e => e.DiaryId == id) > 0;
        }
    }
}