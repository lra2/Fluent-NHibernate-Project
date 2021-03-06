﻿using System;
using System.IO;
using AutomappedProj.Entities;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace AutomappedProj
{
    class Program
    {
        private const string DbFile = "firstProgram.db";

        static void Main()
        {
            var sessionFactory = CreateSessionFactory();

            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var barginBasin = new Store { Name = "Bargin Basin" };
                    var superMart = new Store { Name = "SuperMart" };
                    var meatHouse = new Store { Name = "Meat House" };

                    var potatoes = new Product { Name = "Potatoes", Price = 3.60 };
                    var fish = new Product { Name = "Fish", Price = 4.49 };
                    var milk = new Product { Name = "Milk", Price = 0.79 };
                    var bread = new Product { Name = "Bread", Price = 1.29 };
                    var cheese = new Product { Name = "Cheese", Price = 2.10 };
                    var waffles = new Product { Name = "Waffles", Price = 2.41 };
                    var babbybeef = new Product { Name = "Baby Beef", Price = 15.99 };
                    var steak = new Product { Name = "Steak", Price = 20.99 };

                    var daisy = new Employee { FirstName = "Daisy", LastName = "Harrison" };
                    var jack = new Employee { FirstName = "Jack", LastName = "Torrance" };
                    var sue = new Employee { FirstName = "Sue", LastName = "Walkters" };
                    var bill = new Employee { FirstName = "Bill", LastName = "Taft" };
                    var joan = new Employee { FirstName = "Joan", LastName = "Pope" };
                    var harry = new Employee { FirstName = "Harry", LastName = "Mitchell" };

                    AddProductsToStore(barginBasin, potatoes, fish, milk, bread, cheese);
                    AddProductsToStore(superMart, bread, cheese, waffles);
                    AddProductsToStore(meatHouse, babbybeef, steak);

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
                        WriteStorePretty(store);
                    }
                }
            }

            Console.ReadKey();
        }

        static AutoPersistenceModel CreateAutomappings()
        {
            return AutoMap.AssemblyOf<Employee>(new ExampleAutomappingConfiguration())
                .Conventions.Add<CascadeConvention>();
        }

        /// <summary>
        /// Configure NHibernate. This method returns an ISessionFactory instance that is
        /// populated with mappings created by Fluent NHibernate.
        /// 
        /// Line 1:   Begin configuration
        ///      2+3: Configure the database being used
        ///      4+5: Specify what mappings are going to be used (Automappings from the CreateAutomappings method)
        ///      6:   Expose the underlying configuration instance to the BuildSchema method,
        ///           this creates the database.
        ///      7:   Finally, build the session factory.
        /// </summary>
        /// <returns></returns>
        private static ISessionFactory CreateSessionFactory()
        {
            ISessionFactory iSessionFactory =  Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012
                .ConnectionString(@"Data Source=DESKTOP-0QUBDNJ\SQLEXPRESS;Initial Catalog=nhdb;Integrated Security=True"))
                .Mappings(m =>
                    m.AutoMappings.Add(CreateAutomappings))
                .ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();

            return iSessionFactory;
        }

        private static void BuildSchema(Configuration config)
        {
            if (File.Exists(DbFile))
                File.Delete(DbFile);
            
            var schema = new SchemaExport(config);
            schema.Create(false, true);

            //var schemaAux = new SchemaUpdate(config);
            //schemaAux.Execute(false, true);
        }

        private static void WriteStorePretty(Store store)
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
    }
}