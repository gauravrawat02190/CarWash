using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConsole
{

    public static class LinqHelper
    {
        public static void GetOrderByCategory()
        {
            using (PracEntities entity = new PracEntities())
            {
                var result = (from order in entity.Orders
                              join category in entity.Categories
                                on order.categoryid equals category.id
                              //where order.amount> 10
                              group order
                              by new
                              {
                                  order.categoryid,
                                  category.name
                              }
                               into
                              Result
                              orderby Result.Key.name descending
                              select new
                              {
                                  TotalAmount = Result.Sum(g => g.amount * g.unit),
                                  categoryId = Result.Key.categoryid,
                                  CategoryName = Result.Key.name
                              }).ToList();

                foreach (var item in result)
                {
                    Console.WriteLine("Category Name :" + item.CategoryName + " - " + "Total Sum " + item.TotalAmount);
                }

            }
        }


        public static void GetEmployeeDetails()
        {
            using (PracEntities entity = new PracEntities())
            {
                var res = (from emp in entity.EmployeeMasters
                           join team in entity.TeamMasters
                            on emp.EmpId equals team.EmpId
                           join manager in entity.EmployeeMasters
                            on team.ReportingManagerId equals manager.EmpId
                           select new
                           {
                               CostCenter = team.CostCenter,
                               EmployeeName = emp.name,
                               Manager = manager.name
                           }).ToList();

                foreach(var item in res)
                {
                    Console.WriteLine("CostCenter: " + item.CostCenter + " Emp Name: " + item.EmployeeName + " Manager Name: " + item.Manager);
                }
            }
        }

        public static void GetOrderByCustomer()
        {
            using (PracEntities entity = new PracEntities())
            {
                var result = (from order in entity.Orders
                              join category in entity.Categories
                                on order.categoryid equals category.id
                              group order
                              by new
                              {
                                  order.custname
                              }
                               into
                              Result
                              orderby Result.Key.custname ascending
                              select new
                              {
                                  TotalAmount = Result.Sum(g => g.amount * g.unit),
                                  CustomerName = Result.Key.custname
                              }).ToList();

                foreach (var item in result)
                {
                    Console.WriteLine("Customer Name :" + item.CustomerName + " - " + "Total Sum " + item.TotalAmount);
                }

            }

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            LinqHelper.GetOrderByCategory();
            Console.WriteLine("---------------------------------------------");
            LinqHelper.GetOrderByCustomer();
            Console.WriteLine("---------------------------------------------");
            LinqHelper.GetEmployeeDetails();
            Console.ReadLine();
        }
    }
}
