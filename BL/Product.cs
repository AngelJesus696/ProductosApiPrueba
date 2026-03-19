using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Product
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ProductosPruebaEntities context = new DL.ProductosPruebaEntities())
                {
                    var listProducts = context.ProductGetAll().ToList();
                    if (listProducts.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var productDB in listProducts)
                        {
                            ML.Product product = new ML.Product();

                            product.IdProduct = productDB.IdProduct;
                            product.Name = productDB.Name;
                            product.Price = productDB.Price;

                            result.Objects.Add(product);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMesage = "No se encotraron productos";
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
        public static ML.Result GetById(int IdProduct)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ProductosPruebaEntities context = new DL.ProductosPruebaEntities())
                {
                    var Product = context.ProductGetById(IdProduct).SingleOrDefault();
                    if (Product != null)
                    {
                            ML.Product product = new ML.Product();

                            product.IdProduct = Product.IdProduct;
                            product.Name = Product.Name;
                            product.Price = Product.Price;

                            result.Object = product;
                        
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMesage = "No se encontro un producto";
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
        public static ML.Result Delete(int IdProduct)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ProductosPruebaEntities context = new DL.ProductosPruebaEntities())
                {
                    int filasAfectadas = context.ProductDelete(IdProduct);
                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMesage = "No se encontro un producto";
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
        public static ML.Result Add(ML.Product product)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ProductosPruebaEntities context = new DL.ProductosPruebaEntities())
                {
                    int filasAfectadas = context.ProductAdd(product.Name, product.Price);
                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                        result.Object = product;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMesage = "No se encontro un producto";
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
        public static ML.Result Update(ML.Product product)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.ProductosPruebaEntities context = new DL.ProductosPruebaEntities())
                {
                    int filasAfectadas = context.ProductUpdate(product.Name, product.Price, product.IdProduct);
                    if (filasAfectadas > 0)
                    {
                        result.Object = product;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMesage = "No se encontro un producto";
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
