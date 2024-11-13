using FalastinShop.SIMS.ProductManagment;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
namespace FalastinShop.SIMS.DB;

public class AppDB : IAppDB
{
    public AppDB(IConfiguration configuration)
    {
        _connectionStr = configuration.GetConnectionString("DBconnection");
    }

    private readonly string _connectionStr;

    public void SaveProduct(Product product)
    {
        using SqlConnection connection = new(_connectionStr);
        connection.Open();
        SqlTransaction transaction = connection.BeginTransaction();
        try
        {
            var currencyCode = product.Price.Currency.ToString();
            var currencyId = FindCurrency(currencyCode);
            currencyId ??= InsertCurrency(currencyCode, connection, transaction);
            InsertProduct(product, connection, transaction, currencyId);

            transaction.Commit();
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            Console.WriteLine(ex.Message);
        }
    }

    public void InsertProduct(Product product, SqlConnection connection, SqlTransaction transaction, int? currencyId)
    {
        var productCommand = new SqlCommand(""" 
                                               INSERT INTO Product(Name,Quantity,ProductPrice,CurrencyId) 
                                                VALUES(@Name,@Quantity,@ProductPrice,@CurrencyId)
                                            """,
                                            connection,
                                            transaction);

        productCommand.Parameters.AddWithValue("@Name", product.Name);
        productCommand.Parameters.AddWithValue("@Quantity", product.Quantity);
        productCommand.Parameters.AddWithValue("@ProductPrice", product.Price.ItemPrice);
        productCommand.Parameters.AddWithValue("@CurrencyId", currencyId);
        productCommand.ExecuteNonQuery();
    }

    public Product? GetProductByName(string productName, out int currencyId)
    {
        Product? dbProduct = null;
        currencyId = 0;
        using SqlConnection connection = new(_connectionStr);

        connection.Open();
        var command = new SqlCommand("""
                select Top 1 ProductId,Name,Quantity,ProductPrice,Currency.CurrencyId,CurrencyCode
                from Product
                join Currency 
                on Currency.CurrencyId = Product.CurrencyId
                where Name = @productName
                """, connection);

        command.Parameters.AddWithValue("@productName", productName);

        using SqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            reader.Read();
            currencyId = Convert.ToInt32(reader["CurrencyId"]);
            Enum.TryParse<Currency>(reader["CurrencyCode"].ToString(), out var currencyCode);
            dbProduct = new Product
            {
                Id = Convert.ToInt32(reader["ProductId"]),
                Name = reader["Name"].ToString()!,
                Quantity = Convert.ToInt32(reader["Quantity"]),
                Price = new Price
                {
                    ItemPrice = Convert.ToInt32(reader["ProductPrice"]),
                    Currency = currencyCode
                }
            };
        }
        return dbProduct;
    }

    public int? FindCurrency(string code)
    {
        using SqlConnection connection = new(_connectionStr);
        connection.Open();
        var command = new SqlCommand(
            """          
                select CurrencyId 
                from Currency
                where CurrencyCode = @code
             """
             , connection);

        command.Parameters.AddWithValue("@code", code);

        using SqlDataReader reader = command.ExecuteReader();
        if (reader.HasRows)
        {
            reader.Read();
            return Convert.ToInt32(reader["CurrencyId"]);
        }
        return null;
    }

    public void UpdateProduct(string name, Product product)
    {
        var dbProduct = GetProductByName(name, out var currencyId);
        if (dbProduct != null)
        {
            using SqlConnection connection = new(_connectionStr);
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                var ExistCurrencyId = FindCurrency(product.Price.Currency.ToString());

                if (ExistCurrencyId is null)
                {
                    currencyId = InsertCurrency(product.Price.Currency.ToString(), connection, transaction);
                }
                else
                {
                    currencyId = (int)ExistCurrencyId;
                }

                var updateProduct = new SqlCommand(
                   """
                   update Product
                   set Quantity = @newQty,Name = @newName,ProductPrice = @newProductPrice, CurrencyId = @newCurrencyId
                   where ProductId = @productId
                   """
                   , connection, transaction);

                updateProduct.Parameters.AddWithValue("@newQty", product.Quantity);
                updateProduct.Parameters.AddWithValue("@newName", product.Name);
                updateProduct.Parameters.AddWithValue("@newCurrencyId", currencyId);
                updateProduct.Parameters.AddWithValue("@newProductPrice", product.Price.ItemPrice);
                updateProduct.Parameters.AddWithValue("@productId", dbProduct.Id);

                updateProduct.ExecuteNonQuery();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine(ex.Message);
            }
        }
        else
        {
            Console.WriteLine("Product Not Found!");
        }
    }

    public int InsertCurrency(string code, SqlConnection connection, SqlTransaction transaction)
    {
        int currencyId;
        var insertCurrency = new SqlCommand(
                        """
                         Declare @NewCurrencyId INT;
                         insert into Currency(CurrencyCode)
                         Values (@CurrencyCode);
                         SET @NewCurrencyId =  SCOPE_IDENTITY();
                         select @NewCurrencyId;
                        """
                                 , connection, transaction);

        insertCurrency.Parameters.AddWithValue("@CurrencyCode", code);
        currencyId = Convert.ToInt32(insertCurrency.ExecuteScalar());
        return currencyId;
    }

    public void DeleteProduct(string name)
    {
        var product = GetProductByName(name, out var currencyId);
        if (product != null)
        {
            using SqlConnection connection = new SqlConnection(_connectionStr);
            connection.Open();
            var deleteCommand = new SqlCommand(
                """ 
                delete Product
                where ProductId = @ProductId
                """, connection);

            deleteCommand.Parameters.AddWithValue("@ProductId", product.Id);
            deleteCommand.ExecuteNonQuery();
        }
        else
        {
            Console.WriteLine("Product Not Found!");
        }
    }
}
