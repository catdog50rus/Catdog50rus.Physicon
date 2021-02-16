using Catdog50rus.Physicon.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Catdog50rus.Physicon.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private IEnumerable<CourseTreeModel> _courses;
        private SelectList _subjects; 
        private SelectList _grades; 
        private SelectList _genres; 

        [HttpGet]
        public async Task<IActionResult> Index(string sub)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri("https://localhost:5001/");
            HttpResponseMessage response = await httpClient.GetAsync("api/course");
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();

                var r = JsonConvert.DeserializeObject<List<CourseTreeModel>>(data);
                if (r == null)
                    return NotFound();
                _courses = r;
            }
            else
            {
                return View("Error");
            }

            _subjects = new SelectList(_courses.Select(c => c.Subject).Distinct());
            _grades = new SelectList(_courses.Select(c => c.Grade).Distinct());
            _genres = new SelectList(_courses.Select(c => c.Genre).Distinct());

            if (!string.IsNullOrWhiteSpace(sub))
            {
                string[] str = sub.Split(";");
                for (int i = 0; i < str.Length; i++)
                {
                    var parameter = str[i];
                    if (!string.IsNullOrWhiteSpace(parameter))
                    {
                        switch (i)
                        {
                            case 0:
                                _courses = _courses.Where(c => c.Subject == parameter);
                                _grades = new SelectList(_courses.Select(c => c.Grade).Distinct());
                                _genres = new SelectList(_courses.Select(c => c.Genre).Distinct());
                                break;
                            case 1:
                                _courses = _courses.Where(c => c.Grade == parameter);
                                break;
                            case 2:
                                _courses = _courses.Where(c => c.Genre == parameter);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            ViewBag.Subject = _subjects;
            ViewBag.Grade = _grades;
            ViewBag.Genre = _genres;

            return View(_courses);
        }

    }
}
