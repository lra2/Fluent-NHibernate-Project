using System;
using System.IO;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using FluentNHibernateProj.Entities;

namespace FluentNHibernateProj
{
    class Program
    {
        private const string DbFile = "firstProject.db";

        static void Main()
        {
            var sessionFactory = CreateSessionFactory();

            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var barginBasin = new Store { Name = "Bargin Basin" };
                    var superMart = new Store { Name = "SuperMart" };
                    var meatHouse = new Store { Name = "Meat House"};

                    var potatoes = new Product { Name = "Potatoes", Price = 3.60 };
                    var fish = new Product { Name = "Fish", Price = 4.49 };
                    var milk = new Product { Name = "Milk", Price = 0.79 };
                    var bread = new Product { Name = "Bread", Price = 1.29 };
                    var cheese = new Product { Name = "Cheese", Price = 2.10 };
                    var waffles = new Product { Name = "Waffles", Price = 2.41 };
                    var babybeef = new Product { Name = "Baby Beef", Price = 15.99 };

                    var daisy = new Employee { FirstName = "Daisy", LastName = "Harrison" };
                    var jack = new Employee { FirstName = "Jack", LastName = "Torrance" };
                    var sue = new Employee { FirstName = "Sue", LastName = "Walkters" };
                    var bill = new Employee { FirstName = "Bill", LastName = "Taft" };
                    var joan = new Employee { FirstName = "Joan", LastName = "Pope" };
                    var harry = new Employee { FirstName = "Harry", LastName = "Potter" };

                    AddProductsToStore(barginBasin, potatoes, fish, milk, bread, cheese);
                    AddProductsToStore(superMart, bread, cheese, waffles);
                    AddProductsToStore(meatHouse, babybeef);

                    AddEmployeesToStore(barginBasin, daisy, jack, sue);
                    AddEmployeesToStore(superMart, bill, joan);
                    AddEmployeesToStore(meatHouse, harry);

                    session.SaveOrUpdate(barginBasin);
                    session.SaveOrUpdate(superMart);
                    session.SaveOrUpdate(meatHouse);

                    transaction.Commit();
                }
            }

            using (var session = sessionFactory.OpenSession())
            {
                using (session.BeginTransaction())
                {
                    var stores = session.CreateCriteria(typeof(Store))
                        .List<Store>();

                    foreach (var store in stores)
                    {
                        PrintStoreDetails(store);
                    }
                }
            }

            Console.ReadKey();
        }

        public static void AddProductsToStore(Store store, params Product[] products)
        {
            foreach (var product in products)
            {
                store.AddProduct(product);
            }
        }

        public static void AddEmployeesToStore(Store store, params Employee[] employees)
        {
            foreach (var employee in employees)
            {
                store.AddEmployee(employee);
            }
        }

        private static void PrintStoreDetails(Store store)
        {
            Console.WriteLine(store.Name);
            Console.WriteLine("  Products:");

            foreach (var product in store.Products)
            {
                Console.WriteLine("    " + product.Name);
            }

            Console.WriteLine("  Staff:");

            foreach (var employee in store.Staff)
            {
                Console.WriteLine("    " + employee.FirstName + " " + employee.LastName);
            }

            Console.WriteLine();
        }

        private static ISessionFactory CreateSessionFactory()
        {
            ISessionFactory isessionFactory = Fluently.Configure()
           .Database(MsSqlConfiguration.MsSql2012
                .ConnectionString(@"Data Source=DESKTOP-0QUBDNJ\SQLEXPRESS;Initial Catalog=nhdb;Integrated Security=True"))
           .Mappings(m => m
                .FluentMappings.AddFromAssemblyOf<Program>())
           .ExposeConfiguration(BuildSchema)
           .BuildSessionFactory();

            return isessionFactory;
        }

        private static void BuildSchema(Configuration config)
        {
            if (File.Exists(DbFile))
                File.Delete(DbFile);

            var schema = new SchemaExport(config);
            schema.Create(false, true);

            //var aux = new SchemaUpdate(config);
            //aux.Execute(false, true);
        }
    }
}
