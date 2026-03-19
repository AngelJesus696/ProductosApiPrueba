using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL.Controllers
{
    public class productsController : ApiController
    {
        public IHttpActionResult Add([FromBody] ML.Product product)
        {
            ML.Result result = BL.Product.Add(product);
            if (result.Correct)
            {
                return Created("", result.Object);
            }
            else
            {
                return BadRequest(result.ErrorMesage);
            }
        }
        [HttpPut]
        public IHttpActionResult Update(int IdProduct, [FromBody] ML.Product product)
        {
            product.IdProduct = IdProduct;
            ML.Result result = BL.Product.Update(product);
            if (result.Correct)
            {
                return Ok(result.Object);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpDelete]
        public IHttpActionResult Delete(int IdProduct)
        {
            ML.Result result = BL.Product.Delete(IdProduct);
                if (result.Correct)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        public IHttpActionResult GetById(int IdProduct)
        {
            ML.Result result = BL.Product.GetById(IdProduct);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        public IHttpActionResult GetAll()
        {
            ML.Result result = BL.Product.GetAll();
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
