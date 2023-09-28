using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ProductUI.Models;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ProductUI.Controllers
{
    public class ProductController : Controller
    {

        private string url = "https://localhost:7211/api/Product/";
        private HttpClient client = new HttpClient();



        private readonly IDbConnection db;
        private readonly IWebHostEnvironment _webHostEnvironment;

       
        public ProductController(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            
            _webHostEnvironment = webHostEnvironment;
            this.db = new SqlConnection(configuration.GetConnectionString("DBConn"));
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<ProductModel> product = new List<ProductModel>();
            HttpResponseMessage response = client.GetAsync(url + "GetAll").Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<List<ProductModel>>(result);
                if (data != null)
                {
                    product = data;
                }
            }
            return View(product);
        }


        [HttpGet]
        public IActionResult Create()
        {
            string query1 = "select CatId, PName from SHUBHAM_Category;";
            IEnumerable<Item> items = db.Query<Item>(query1);
            ViewBag.Items = new SelectList(items, "CatId", "PName");

            string query2 = "select Pid, Ptype from SHUBHAM_Type;";
            IEnumerable<TypeItem> typeItems = db.Query<TypeItem>(query2);
            ViewBag.TypeItems = new SelectList(typeItems, "Pid", "Ptype");
            return View();
        }



        [HttpPost]
        public IActionResult Create(ProductModel product, IFormFile ImageFile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
                        var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            ImageFile.CopyTo(stream);
                        }
                        product.Image = "~/Images/" + uniqueFileName;
                    }
                }
                string data = JsonConvert.SerializeObject(product);
                StringContent content = new StringContent(data, Encoding.UTF8, "Application/json");
                HttpResponseMessage response = client.PostAsync(url + "Create", content).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["insert_message"] = "Student Added..";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                TempData["insert_message"] = "Some Error occured" + ex.Message;
                throw new Exception(ex.Message);
            }
            return View(product);
        }





        [HttpGet]
        public IActionResult Edit(int id)
        {
            string query = "select CatId, PName from SHUBHAM_Category;";
            IEnumerable<Item> items = db.Query<Item>(query);
            ViewBag.Items = new SelectList(items, "CatId", "PName");

            string query2 = "select Pid, Ptype from SHUBHAM_Type;";
            IEnumerable<TypeItem> typeItems = db.Query<TypeItem>(query2);
            ViewBag.TypeItems = new SelectList(typeItems, "Pid", "Ptype");

            //ProductModel product = new ProductModel();
            HttpResponseMessage response = client.GetAsync(url + $"GetById/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<ProductModel>(result);
                if (data != null)
                {
                    return View(data);
                }
            }
            return View("Product not found");

        }


        [HttpPost]
        public IActionResult Edit(ProductModel product, IFormFile ImageFile)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ImageFile.FileName);
                        var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            ImageFile.CopyTo(stream);
                        }
                        product.Image = "~/Images/" + uniqueFileName;
                    }

                    string data = JsonConvert.SerializeObject(product);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PutAsync(url + $"Update/{product.Id}", content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["update_message"] = "Product Updated";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["update_message"] = "Failed to update product.";
                        // Handle the failure, possibly by returning an error view or message.
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                TempData["update_message"] = $"An error occurred: {ex.Message}";
                // You might want to handle this more gracefully, e.g., return an error view.
            }

            return View(product);
        }




        [HttpGet]
        public IActionResult Delete(int id)
        {
            ProductModel product = new ProductModel();
            HttpResponseMessage response = client.GetAsync(url + $"GetById/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<ProductModel>(result);
                if (data != null)
                {
                    product = data;
                }
            }
            return View(product);
        }


        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                HttpResponseMessage response = client.DeleteAsync(url + $"Remove/{id}").Result;
                TempData["delete_message"] = "Product Deleted";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["delete_message"] = $"An error occurred: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
