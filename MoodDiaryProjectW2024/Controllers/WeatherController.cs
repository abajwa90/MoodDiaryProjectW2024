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
using System.Web.Mvc;
using System.Web.Script.Serialization;
using MoodDiaryProjectW2024.Models;
using Microsoft.Ajax.Utilities;
using System.Web.Caching;
using System.Runtime.CompilerServices;

namespace MoodDiaryProjectW2024.Controllers
{
    public class WeatherController : Controller
    {
        private static readonly HttpClient client;

        static WeatherController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44381/api/WeatherData/");
        }
        // GET: Weather/List
        public ActionResult List()
        {
            //objective: communicate with weather data api to retrieve list of weather
            //curl https://localhost:44381/api/DiaryData/ListWeathers

            string url = "ListWeathers";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<WeatherDto> Weathers = response.Content.ReadAsAsync<IEnumerable<WeatherDto>>().Result;
            Debug.WriteLine("Number of weathers recieved : ");
            Debug.WriteLine(Weathers.Count());
            return View(Weathers);
        }

        // GET: Weather/Details/4
        public ActionResult Details(int id)
        {
            //objective: communicate with weather data api to retrieve list of weather
            //curl https://localhost:44381/api/MoodData/FindWeather/{id)

            string url = "https://localhost:44381/api/MoodData/FindWeather/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            WeatherDto selectedWeather = response.Content.ReadAsAsync<WeatherDto>().Result;
            Debug.WriteLine("Weather recieved : ");
            Debug.WriteLine(selectedWeather.WeatherTemperature);
            Debug.WriteLine(selectedWeather.WeatherPrecip);
            Debug.WriteLine(selectedWeather.WeatherClouds);
            Debug.WriteLine(selectedWeather.WeatherDay);
            return View(selectedWeather);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Mood/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Weather/Create
        public ActionResult Create(Weather Weather)
        {
            Debug.WriteLine("the json payload is:");
            Debug.WriteLine(Weather.WeatherTemperature);
            Debug.WriteLine(Weather.WeatherPrecip);
            Debug.WriteLine(Weather.WeatherClouds);
            Debug.WriteLine(Weather.WeatherDay);
            //objective: add new weather into sytem using API
            //curl -H "Content-type: application/json" -d https://localhost:44381/api/WeatherData/
            string url = "AddWeather";

            JavaScriptSerializer jss = new JavaScriptSerializer();
            string jsonpayload = jss.Serialize(Weather);

            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }

        // GET: Weather/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "FindWeather/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result; ;

            WeatherDto selectedWeather = response.Content.ReadAsAsync<WeatherDto>().Result;
            return View(selectedWeather);
        }

        // POST: Weather/Update/3
        [System.Web.Http.HttpPost]
        public ActionResult Update(int id, Weather Weather)
        {
            try
            {
                Debug.WriteLine("The new Weather info is:");
                Debug.WriteLine(Weather.WeatherTemperature);
                Debug.WriteLine(Weather.WeatherPrecip);
                Debug.WriteLine(Weather.WeatherClouds);
                Debug.WriteLine(Weather.WeatherDay);

                string url = "UpdateWeather/" + id;
                JavaScriptSerializer jss = new JavaScriptSerializer();
                string jsonpayload = jss.Serialize(Weather);

                HttpContent content = new StringContent(jsonpayload);
                content.Headers.ContentType.MediaType = "application/json";
                HttpResponseMessage response = client.PostAsync(url, content).Result;
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Weather/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Weather/Delete/5
        [System.Web.Http.HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
