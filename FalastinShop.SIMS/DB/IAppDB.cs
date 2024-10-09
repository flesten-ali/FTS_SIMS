using FalastinShop.SIMS.ProductManagment;
using System.Data.SqlClient;
namespace FalastinShop.SIMS.DB;

public interface IAppDB
{
    void DeleteProduct(string name);
    int? FindCurrency(string code);
    Product? GetProductByName(string productName, out int currencyId);
    int InsertCurrency(string code, SqlConnection connection, SqlTransaction transaction);
    void InsertProduct(Product product, SqlConnection connection, SqlTransaction transaction, int? currencyId);
    void SaveProduct(Product product);
    void UpdateProduct(string name, Product product);
}
