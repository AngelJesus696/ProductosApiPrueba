using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult GetAll()
        {
            ML.Product product = new ML.Product();
            ML.Result result = GetAllRest();
            product.Products = result.Objects;
            return View(product);
        }
        public ActionResult Fromulario(int? IdUsuario)
        {
            return View();
        }
        public ML.Result GetAllRest()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (var httpclient = new HttpClient())
                {
                    httpclient.BaseAddress = new Uri("http://localhost:64402/api/");
                    var respuesta = httpclient.GetAsync("products");
                    respuesta.Wait();
                    var respuestaService = respuesta.Result;
                    if (respuestaService.IsSuccessStatusCode)
                    {
                        var leerTask = respuestaService.Content.ReadAsAsync<ML.Result>();
                        leerTask.Wait();
                        var resultTask = leerTask.Result;
                        if (resultTask.Objects != null)
                        {
                            result.Objects = new List<object>();
                            foreach (var resulitem in resultTask.Objects)
                            {
                                ML.Product resultProducts= JsonConvert.DeserializeObject<ML.Product>(resulitem.ToString());

                                result.Objects.Add(resultProducts);
                            }
                            result.Correct = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMesage = ex.Message;
            }
            return result;
        }
    }
}