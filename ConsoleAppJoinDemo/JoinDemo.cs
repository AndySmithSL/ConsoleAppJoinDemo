using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppJoinDemo
{
    class JoinDemo
    {
        // Specify the first data source.
        List<Category> categories = new List<Category>()
        {
            new Category(){Name="Beverages", Id=001},
            new Category(){ Name="Condiments", Id=002},
            new Category(){ Name="Vegetables", Id=003},
            new Category() {  Name="Grains", Id=004},
            new Category() {  Name="Fruit", Id=005}
        };

        // Specify the second data source.
        List<Product> products = new List<Product>()
        {
            new Product{Name="Cola",  CategoryId=001},
            new Product{Name="Tea",  CategoryId=001},
            new Product{Name="Mustard", CategoryId=002},
            new Product{Name="Pickles", CategoryId=002},
            new Product{Name="Carrots", CategoryId=003},
            new Product{Name="Bok Choy", CategoryId=003},
            new Product{Name="Peaches", CategoryId=005},
            new Product{Name="Melons", CategoryId=005},
        };

        public void InnerJoin()
        {
            // Create the query that selects 
            // a property from each element.
            var innerJoinQuery =
               from category in categories
               join prod in products on category.Id equals prod.CategoryId
               select new { Category = category.Id, Product = prod.Name };

            Console.WriteLine("InnerJoin:");
            // Execute the query. Access results 
            // with a simple foreach statement.
            foreach (var item in innerJoinQuery)
            {
                Console.WriteLine("{0,-10}{1}", item.Product, item.Category);
            }
            Console.WriteLine("InnerJoin: {0} items in 1 group.", innerJoinQuery.Count());
            Console.WriteLine(System.Environment.NewLine);
        }

        public void GroupJoin()
        {
            // This is a demonstration query to show the output
            // of a "raw" group join. A more typical group join
            // is shown in the GroupInnerJoin method.
            var groupJoinQuery =
               from category in categories
               join prod in products on category.Id equals prod.CategoryId into prodGroup
               select prodGroup;

            // Store the count of total items (for demonstration only).
            int totalItems = 0;

            Console.WriteLine("Simple GroupJoin:");

            // A nested foreach statement is required to access group items.
            foreach (var prodGrouping in groupJoinQuery)
            {
                Console.WriteLine("Group:");
                foreach (var item in prodGrouping)
                {
                    totalItems++;
                    Console.WriteLine("   {0,-10}{1}", item.Name, item.CategoryId);
                }
            }
            Console.WriteLine("Unshaped GroupJoin: {0} items in {1} unnamed groups", totalItems, groupJoinQuery.Count());
            Console.WriteLine(System.Environment.NewLine);
        }

        public void GroupInnerJoin()
        {
            var groupJoinQuery2 =
                from category in categories
                orderby category.Id
                join prod in products on category.Id equals prod.CategoryId into prodGroup
                select new
                {
                    Category = category.Name,
                    Products = from prod2 in prodGroup
                               orderby prod2.Name
                               select prod2
                };

            //Console.WriteLine("GroupInnerJoin:");
            int totalItems = 0;

            Console.WriteLine("GroupInnerJoin:");
            foreach (var productGroup in groupJoinQuery2)
            {
                Console.WriteLine(productGroup.Category);
                foreach (var prodItem in productGroup.Products)
                {
                    totalItems++;
                    Console.WriteLine("  {0,-10} {1}", prodItem.Name, prodItem.CategoryId);
                }
            }
            Console.WriteLine("GroupInnerJoin: {0} items in {1} named groups", totalItems, groupJoinQuery2.Count());
            Console.WriteLine(System.Environment.NewLine);
        }

        public void GroupJoin3()
        {

            var groupJoinQuery3 =
                from category in categories
                join product in products on category.Id equals product.CategoryId into prodGroup
                from prod in prodGroup
                orderby prod.CategoryId
                select new { Category = prod.CategoryId, ProductName = prod.Name };

            //Console.WriteLine("GroupInnerJoin:");
            int totalItems = 0;

            Console.WriteLine("GroupJoin3:");
            foreach (var item in groupJoinQuery3)
            {
                totalItems++;
                Console.WriteLine("   {0}:{1}", item.ProductName, item.Category);
            }

            Console.WriteLine("GroupJoin3: {0} items in 1 group", totalItems, groupJoinQuery3.Count());
            Console.WriteLine(System.Environment.NewLine);
        }

        public void LeftOuterJoin()
        {
            // Create the query.
            var leftOuterQuery =
               from category in categories
               join prod in products on category.Id equals prod.CategoryId into prodGroup
               select prodGroup.DefaultIfEmpty(new Product() { Name = "Nothing!", CategoryId = category.Id });

            // Store the count of total items (for demonstration only).
            int totalItems = 0;

            Console.WriteLine("Left Outer Join:");

            // A nested foreach statement  is required to access group items
            foreach (var prodGrouping in leftOuterQuery)
            {
                Console.WriteLine("Group:", prodGrouping.Count());
                foreach (var item in prodGrouping)
                {
                    totalItems++;
                    Console.WriteLine("  {0,-10}{1}", item.Name, item.CategoryId);
                }
            }
            Console.WriteLine("LeftOuterJoin: {0} items in {1} groups", totalItems, leftOuterQuery.Count());
            Console.WriteLine(System.Environment.NewLine);
        }

        public void LeftOuterJoin2()
        {
            // Create the query.
            var leftOuterQuery2 =
               from category in categories
               join prod in products on category.Id equals prod.CategoryId into prodGroup
               from item in prodGroup.DefaultIfEmpty()
               select new { Name = item == null ? "Nothing!" : item.Name, CategoryId = category.Id };

            Console.WriteLine("LeftOuterJoin2: {0} items in 1 group", leftOuterQuery2.Count());
            // Store the count of total items
            int totalItems = 0;

            Console.WriteLine("Left Outer Join 2:");

            // Groups have been flattened.
            foreach (var item in leftOuterQuery2)
            {
                totalItems++;
                Console.WriteLine("{0,-10}{1}", item.Name, item.CategoryId);
            }
            Console.WriteLine("LeftOuterJoin2: {0} items in 1 group", totalItems);
        }
    }
}
