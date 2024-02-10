using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;
using MoodDiaryProjectW2024.Models;
using System.Web.Caching;
using Microsoft.Ajax.Utilities;
using System.Web.Script.Serialization;

namespace MoodDiaryProjectW2024.Controllers
{
    public class DiaryController : Controller
    {
        private static readonly HttpClient client;
            
            static DiaryController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44381/api/DiaryData/");
        }
        // GET: Diary/List
        public ActionResult List()
        {
            //objective: communicate with diary data api to retrieve list of diaries
            //curl https://localhost:44381/api/DiaryData/ListDiaries

            string url = "ListDiaries";
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            IEnumerable<DiaryDto> Diaries = response.Content.ReadAsAsync<IEnumerable<DiaryDto>>().Result;
            Debug.WriteLine("Number of diaries recieved : ");
            Debug.WriteLine(Diaries.Count());
            return View(Diaries);
        }

        // GET: Diary/Details/5
        public ActionResult Details(int id)
        {
            //objective: communicate with diary data api to retrieve list of animals
            //curl https://localhost:44381/api/DiaryData/FindDiary/{id)

            string url = "https://localhost:44381/api/DiaryData/FindDiary/"+id;
            HttpResponseMessage response = client.GetAsync(url).Result;

            Debug.WriteLine("The response code is ");
            Debug.WriteLine(response.StatusCode);

            DiaryDto selectedDiary = response.Content.ReadAsAsync<DiaryDto>().Result;
            Debug.WriteLine("Diary recieved : ");
            Debug.WriteLine(selectedDiary.DiaryName);
            return View(selectedDiary);
        }

        public ActionResult Error()
        {
            return View();
        }

        // GET: Diary/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Diary/Create
        [HttpPost]  
        public ActionResult Create(Diary Diary)
        {
            Debug.WriteLine("the json payload is:");
            Debug.WriteLine(Diary.DiaryName);
            //objective: add new diary into sytem using API
            //curl -H "Content-type: application/json" -d https://localhost:44381/api/DiaryData/
            string url = "AddDiary";

            JavaScriptSerializer jss = new JavaScriptSerializer();
            string jsonpayload = jss.Serialize(Diary);

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

        // GET: Diary/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }
        
        // POST: Diary/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

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
        [HttpPost]
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
