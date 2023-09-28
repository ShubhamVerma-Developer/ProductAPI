using Dapper;
using ProductAPI3.Models;
using System.Data;
using System.Data.SqlClient;

namespace ProductAPI3.Repository
{
    public class ProductRepo : IProductRepo
    {
        private readonly IDbConnection db;

        public ProductRepo(IConfiguration configuration)
        {
            this.db = new SqlConnection(configuration.GetConnectionString("DBConn"));
        }

        public async Task<string> Create(ProductModel product)
        {
            string response = string.Empty;
                var sql = "SHUBHAM_Product1_Create";
                var parameters = new
                {
                    Name = product.Name,
                    CategoryId = Convert.ToInt32(product.CategoryId),
                    Description = product.Description,
                    Quantity = product.Quantity,
                    TypeId = Convert.ToInt32(product.TypeId),
                    Image = product.Image
                };
               int rowAffected =  db.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
            if(rowAffected > 0) {
            response = "pass";
            }  
            return response; 
        }

        public async Task<List<ProductModel>> GetAll()
        {
            return db.Query<ProductModel>("SHUBHAM_Product1_GetAll", commandType: CommandType.StoredProcedure).ToList();

        }

        public Task<ProductModel> GetById(int id)
        {
            var parameters = new { Id = id };

            var product = db.QueryFirstAsync<ProductModel>("SHUBHAM_Product1_GetById", parameters, commandType: CommandType.StoredProcedure);

            return product;
        }

        public async Task<string> Remove(int id)
        {
            string response = string.Empty;
            var parameters = new { Id = id };
            int rowAffected = db.Execute("SHUBHAM_Product1_Delete", parameters, commandType: CommandType.StoredProcedure);
            if(rowAffected > 0) {
                response = "Pass";
            }
            return response;

        }

        public async Task<string> Update(ProductModel product, int id)
        {
            string response = string.Empty;
            var sql = "SHUBHAM_Product1_Update";
            var parameters = new
            {
                Id = id,
                Name = product.Name,
                CategoryId = Convert.ToInt32(product.CategoryId),
                Description = product.Description,
                Quantity = product.Quantity,
                TypeId = Convert.ToInt32(product.TypeId),
                Image = product.Image
            };
            int rowAffected = db.Execute(sql, parameters, commandType: CommandType.StoredProcedure);
            if (rowAffected > 0)
            {
                response = "pass";
            }
            return response;
        }
    }
}
