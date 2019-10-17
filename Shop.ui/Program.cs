using Shop.Domain;
using Shop.DataAccess;
using System;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;
using System.Data.Common;
using System.Data.SqlClient;

/*
 *  1.Регистрация и вход (смс/email)
 *  2.История покупок
 *  3.Категории и товары (картинка в файловой системе)
 *  4.Покупка(корзина), оплата и доставка (PayPal, Qiwi, ETC)
 *  5.Комментарии и рейтинги
 *  6.Поиск (пагинация)
 *  
 *  Кто сделает 3 версии(Подключенный, автономный и EF) получит автомат на экзамене
 */

namespace Shop.ui
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true);
            IConfigurationRoot configurationRoot = builder.Build();
            string providerName = configurationRoot.GetSection("AppConfig").
                GetChildren().
                Single().Value;
            DbProviderFactories.RegisterFactory(providerName, SqlClientFactory.Instance);
            Category category = new Category
            {
                Name = "Бытовая техника",
                ImagePath = "C:/data"
            };
            
            string connectionString = configurationRoot.GetConnectionString("DebugConnectionString");
            using (var context = new ShopContext(connectionString))
            {
                context.Categories.Add(category);
                var res = context.Categories.ToList();
                context.Categories.Remove(category);
                context.Remove(category);
                context.SaveChanges();
            }
        }
    }
}