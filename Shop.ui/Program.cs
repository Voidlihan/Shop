using Shop.Domain;
using Shop.DataAccess;
using System;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections.Generic;

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
        static string connectionString = "DebugConnectionString";
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
            
            string connectionString = configurationRoot.GetConnectionString("DebugConnectionString");
            using (var context = new ShopContext(connectionString))
            {
                var category = new Category
                {
                    Name = "Бытовая техника",
                    ImagePath = "C:/data"
                };
                context.Categories.Add(category);

                var res = context.Categories.ToList();
                context.SaveChanges();
            }
            string data = "12345";
            var newString = data.ExtractOnlyText();
        }
        static void ProcessCollections()
        {
            List<string> cityNames = new List<string>
            {
                "Алматы", "Анкара", "Борисвиль", "Нур-Султан", "Ялта"
            };
            List<string> processedCityNames = new List<string>();
            foreach(string name in cityNames)
            {
                if(name.Contains("-"))
                {
                    processedCityNames.Add(name);
                }
            }
            var result = from name in cityNames where name.Contains("-") select name;
            var shortResult = cityNames.Where(name => name.Contains("-"));
            var shortResult2 = cityNames.FirstOrDefault(name => name.Contains("-"));
            using(var context = new ShopContext(connectionString))
            {
                var query = from name in context.Items where name.Name.Equals(name) select name;                
            }
            using(var context = new ShopContext(connectionString))
            {
                var query = from user in context.Users where user.Address.Equals(user) select user;
            }            
        }
        static void Pagination()
        {   
            using (var context = new ShopContext(connectionString)) 
            {
                var user = new User
                {
                    Email = "adsada@gmail.com",
                    Address = "Abay street 14",
                    Password = "12asdgf53",
                    VerificationCode = "12WC562CC"
                };
                var category1 = new Category
                {
                    Name = "Tech",
                    ImagePath = "C://folder/img.png"
                };
                var item = new Item
                {
                    Name = "Chiller",
                    ImagePath = "C://folder/img.png",
                    Description = "good",
                    Price = 12_321
                };

            }
        }
    }
}