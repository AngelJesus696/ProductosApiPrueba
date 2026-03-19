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
        public ActionResult Fromulario(int? IdProduct)
        {
            ML.Product product = new ML.Product();
            if (IdProduct == null)
            {
                IdProduct = 0;
            }
            if (IdProduct > 0)
            {
                ML.Result result = GetByIdRest(IdProduct);
                product = (ML.Product)result.Object;
                return View(product);
            }
            else
            {
                product.IdProduct = IdProduct.Value;
                return View(product);
            }
        }
        [HttpPost]
        public ActionResult Fromulario(ML.Product product)
        {
            if (product.IdProduct > 0)
            {
                ML.Result result = Update(product);
            }
            else
            {
                ML.Result result = Add(product);
            }
              return RedirectToAction("GetAll");
        }
        public ActionResult Delete(int IdProduct)
        {
            ML.Result result = DeleteRest(IdProduct);
            return RedirectToAction("GetAll");
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
                                ML.Product resultProducts = JsonConvert.DeserializeObject<ML.Product>(resulitem.ToString());

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
        public ML.Result GetByIdRest(int? IdProduct)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (var httpclient = new HttpClient())
                {
                    httpclient.BaseAddress = new Uri("http://localhost:64402/api/");
                    var respuesta = httpclient.GetAsync("products?IdProduct=" + IdProduct);
                    respuesta.Wait();
                    var respuestaService = respuesta.Result;
                    if (respuestaService.IsSuccessStatusCode)
                    {
                        var leerTask = respuestaService.Content.ReadAsAsync<ML.Result>();
                        leerTask.Wait();
                        var resultTask = leerTask.Result;
                        if (resultTask.Object != null)
                        {
                            ML.Product resultProducts = JsonConvert.DeserializeObject<ML.Product>(resultTask.Object.ToString());
                            result.Object = resultProducts;
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
        public ML.Result DeleteRest(int IdProduct)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (var httpclient = new HttpClient())
                {
                    httpclient.BaseAddress = new Uri("http://localhost:64402/api/");
                    var respuesta = httpclient.DeleteAsync("products?IdProduct=" + IdProduct);
                    respuesta.Wait();
                    var respuestaService = respuesta.Result;
                    if (respuestaService.IsSuccessStatusCode)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
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
        public ML.Result Add(ML.Product product)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (var httpclient = new HttpClient())
                {
                    httpclient.BaseAddress = new Uri("http://localhost:64402/api/");
                    var respuesta = httpclient.PostAsJsonAsync("products", product);
                    respuesta.Wait();
                    var respuestaService = respuesta.Result;
                    if (respuestaService.IsSuccessStatusCode)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
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
        public ML.Result Update(ML.Product product)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (var httpclient = new HttpClient())
                {
                    httpclient.BaseAddress = new Uri("http://localhost:64402/api/");
                    var respuesta = httpclient.PutAsJsonAsync("products?IdProduct="+product.IdProduct, product);
                    respuesta.Wait();
                    var respuestaService = respuesta.Result;
                    if (respuestaService.IsSuccessStatusCode)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
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