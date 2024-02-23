using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MoodDiaryProjectW2024.Models;
using MoodDiaryProjectW2024.Migrations;

namespace MoodDiaryProjectW2024.Controllers
{
    public class WeatherDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/WeatherData/ListWeathers
        [HttpGet]
        public IEnumerable<WeatherDto> ListWeathers()
        {
            List<Weather> Weathers = db.Weathers.ToList();
            List<WeatherDto> WeatherDtos = new List<WeatherDto>();

            Weathers.ForEach(w => WeatherDtos.Add(new WeatherDto()
            {
                WeatherId = w.WeatherId,
                WeatherTemperature = w.WeatherTemperature,
                WeatherSun = w.WeatherSun,
                WeatherClouds = w.WeatherClouds,
                WeatherPrecip = w.WeatherPrecip,
                WeatherDay = w.WeatherDay,
                DiaryId = w.DiaryId
            }));
            return WeatherDtos;
        }

        // GET: api/WeatherData/FindWeather/3
        [ResponseType(typeof(Weather))]
        [HttpGet]
        public IHttpActionResult FindWeather(int id)
        {
            Weather Weather = db.Weathers.Find(id);
            WeatherDto WeatherDto = new WeatherDto()
            {
                WeatherId = Weather.WeatherId,
                WeatherTemperature = Weather.WeatherTemperature,
                WeatherSun = Weather.WeatherSun,
                WeatherClouds = Weather.WeatherClouds,
                WeatherPrecip = Weather.WeatherPrecip,
                WeatherDay = Weather.WeatherDay,
                DiaryId = Weather.DiaryId
            };
            if (Weather == null)
            {
                return NotFound();
            }

            return Ok(WeatherDto);
        }

        // POST: api/WeatherData/UpdateWeather/2
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateWeather(int id, Weather Weather)
        {
            Debug.WriteLine("Update Weather Error");// help to isolate errors when running api
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model State is invalid");
                return BadRequest(ModelState);
            }

            if (id != Weather.WeatherId)
            {
                Debug.WriteLine("Weather ID mismatch");
                Debug.WriteLine("GET parameter" + id);
                Debug.WriteLine("POST parameter" + Weather.WeatherId);
                return BadRequest();
            }

            db.Entry(Weather).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Weather_Exists(id))
                {
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

        private bool WeatherExists(int id)
        {
            throw new NotImplementedException();
        }

        // POST: api/WeatherData/AddWeather
        [ResponseType(typeof(Weather))]
        [HttpPost]
        public IHttpActionResult AddWeather(Weather Weather)
        {
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model State is invalid");
                return BadRequest(ModelState);
            }

            db.Weathers.Add(Weather);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Weather.WeatherId }, Weather);
        }

        // POST: api/WeatherData/DeleteWeather/4
        [ResponseType(typeof(Weather))]
        [HttpPost]
        public IHttpActionResult DeleteWeather(int id)
        {
            Weather Weather = db.Weathers.Find(id);
            if (Weather == null)
            {
                Debug.WriteLine("Weather not found.");
                return NotFound();
            }

            db.Weathers.Remove(Weather);
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

        private bool Weather_Exists(int id)
        {
            return db.Weathers.Count(e => e.WeatherId == id) > 0;
        }
    }
}