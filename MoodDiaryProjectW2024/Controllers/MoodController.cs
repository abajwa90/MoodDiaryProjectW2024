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
    public class MoodController : Controller
    {
        private static readonly HttpClient client;

        static MoodController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44381/api/MoodData/");
        }
        // GET: Mood/List
        public ActionResult List()
        {
            //objective: communicate with mood data api to retrieve list of moods
            //curl https://localhost:44381/api/DiaryData/ListMoods

            string url = "ListMoods";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<MoodDto> Moods = response.Content.ReadAsAsync<IEnumerable<MoodDto>>().Result;
            Debug.WriteLine("Number of moods recieved : ");
            Debug.WriteLine(Moods.Count());
            return View(Moods);
        }

        // GET: Mood/Details/4
        public ActionResult Details(int id)
        {
            //objective: communicate with mood data api to retrieve list of moods
            //curl https://localhost:44381/api/MoodData/FindMood/{id)

            string url = "https://localhost:44381/api/MoodData/FindMood/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            MoodDto selectedMood = response.Content.ReadAsAsync<MoodDto>().Result;
            Debug.WriteLine("Mood recieved : ");
            Debug.WriteLine(selectedMood.MoodNum);
            Debug.WriteLine(selectedMood.MoodDay);
            return View(selectedMood);
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

        // POST: Mood/Create
        public ActionResult Create(Mood Mood)
        {
            Debug.WriteLine("the json payload is:");
            Debug.WriteLine(Mood.MoodNum);
            Debug.WriteLine(Mood.MoodDay);
            //objective: add new mood into sytem using API
            //curl -H "Content-type: application/json" -d https://localhost:44381/api/MoodData/
            string url = "AddMood";

            JavaScriptSerializer jss = new JavaScriptSerializer();
            string jsonpayload = jss.Serialize(Mood);

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

        // GET: Mood/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "FindMood/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result; ;

            MoodDto selectedmood = response.Content.ReadAsAsync<MoodDto>().Result;
            return View(selectedmood);
        }

        // POST: Mood/Update/3
        [System.Web.Http.HttpPost]
        public ActionResult Update(int id, Mood Mood)
        {
            try
            {
                Debug.WriteLine("The new mood info is:");
                Debug.WriteLine(Mood.MoodNum);
                Debug.WriteLine(Mood.MoodDay);
                Debug.WriteLine(Mood.DiaryId);

                string url = "UpdateMood/" + id;
                JavaScriptSerializer jss = new JavaScriptSerializer();
                string jsonpayload = jss.Serialize(Mood);

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

        // GET: Diary/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Diary/Delete/5
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
