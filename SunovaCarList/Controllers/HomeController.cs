using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SunovaCarList.Models;
using System.Net.Http;
using Newtonsoft.Json;

namespace SunovaCarList.Controllers
{
    public class HomeController : Controller
    {


        public ActionResult Index()
        {
            var response = GetXml();

            if (response.IsSuccessStatusCode)
            {
                var data = ReadResponse(response);

                var model = JsonConvert.DeserializeObject<List<CarViewModel>>(data);
                return View(model);
            }
            else
            {
                return View(MakeEmptyModel());
            }
        }

        public ActionResult Sort(string name, bool up)
        {

            var response = GetXml();

            if (response.IsSuccessStatusCode)
            {
                var data = ReadResponse(response);

                var model = JsonConvert.DeserializeObject<List<CarViewModel>>(data);

                var viewModel = SortViewModel(name, up, model);
                return View("Index", viewModel);
            }
            else
            {
                return View("Index", MakeEmptyModel());
            }


        }

        public ActionResult Filter(string filter)
        {
            if (filter != null)
            {
                var response = GetXml();

                filter = filter.ToLower();

                if (response.IsSuccessStatusCode)
                {
                    var data = ReadResponse(response);

                    var model = JsonConvert.DeserializeObject<List<CarViewModel>>(data);

                    model = model.Where(p => p.Color.ToLower().Contains(filter) || p.Engine.ToLower().Contains(filter)
                    || p.Model.ToLower().Contains(filter) || p.Name.ToLower().Contains(filter)
                    || p.Mileage.ToString().Contains(filter))
                        .ToList();

                    if(!model.Any())
                    {
                        ViewBag.ErroMsg = "Bad input. Please try again.";
                    }

                    return View("Index", model);

                }
            }

            return View("Index", MakeEmptyModel());
        }


        private HttpResponseMessage GetXml()
        {
            string theUrl = "https://mobiledev.sunovacu.ca/api/Test/GetCars";
            var httpClient = new HttpClient();

            return httpClient.GetAsync(theUrl).Result;
        }

        private string ReadResponse(HttpResponseMessage response)
        {
            return response.Content.ReadAsStringAsync().Result;
        }

        private enum CarProperty
        {
            Mileage,
            Name,
            Model,
            Engine,
            Color
        }

        private List<CarViewModel> SortViewModel(string name, bool up, List<CarViewModel> model)
        {
            if (name == CarProperty.Mileage.ToString())
            {
                if (up)
                {
                    model = model.OrderBy(p => p.Mileage).ToList();
                }
                else
                {
                    model = model.OrderByDescending(p => p.Mileage).ToList();
                }

            }
            else if (name == CarProperty.Name.ToString())
            {
                if (up)
                {
                    model = model.OrderBy(p => p.Name).ToList();
                }
                else
                {
                    model = model.OrderByDescending(p => p.Name).ToList();
                }
            }
            else if (name == CarProperty.Model.ToString())
            {
                if (up)
                {
                    model = model.OrderBy(p => p.Model).ToList();
                }
                else
                {
                    model = model.OrderByDescending(p => p.Model).ToList();
                }
            }
            else if (name == CarProperty.Engine.ToString())
            {
                if (up)
                {
                    model = model.OrderBy(p => p.Engine).ToList();
                }
                else
                {
                    model = model.OrderByDescending(p => p.Engine).ToList();
                }
            }
            else if (name == CarProperty.Color.ToString())
            {
                if (up)
                {
                    model = model.OrderBy(p => p.Color).ToList();
                }
                else
                {
                    model = model.OrderByDescending(p => p.Color).ToList();
                }
            }
            else
            {
                model = MakeEmptyModel();
            }

            return model;
        }

        private List <CarViewModel> MakeEmptyModel()
        {
            return new List<CarViewModel>();
        }

    }




}