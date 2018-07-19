using System;
using System.Linq;

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

        public static void sortNumber()
        {
            int[] a = { 4, 1, 23, 5, 100, 2, 10, 500, 12, 22 };
            for (var i = 1; i < a.Length; i++)
            {
                for (var j = 0; j < a.Length - 1; j++)
                {
                    if (a[j] > a[j + 1])
                    {
                        var temp = a[j + 1];
                        a[j + 1] = a[j];
                        a[j] = temp;
                    }
                }
            }

            foreach (var item in a)
            {
                Console.WriteLine(item);
            }


        }

        public static void IsPalidrome()
        {
            const string str = "NAN";
            string strreverse = "";
            for (var i= str.Length-1;i>=0;i--)
            {
                strreverse += str[i].ToString();
            }

            if(str== strreverse)
            {
                Console.WriteLine("ISPalindrome");
            }
            else
            {
                Console.WriteLine("IS Not Palindrome");

            }
        }

        public static void sortString()
        {
            string s = "g,a,v,s,b,z,a,k,n,p";
            var a = s.Split(',');
            for (var i = 1; i < a.Length; i++)
            {
                for (var j = 0; j < a.Length - 1; j++)
                {
                    if (a[j].CompareTo(a[j + 1]) > 0)
                    {
                        var temp = a[j + 1];
                        a[j + 1] = a[j];
                        a[j] = temp;
                    }
                }
            }

            foreach (var item in a)
            {
                Console.WriteLine(item);
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
                               team.CostCenter,
                               EmployeeName = emp.name,
                               Manager = manager.name
                           }).ToList();

                foreach (var item in res)
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
            Console.WriteLine("---------------------------------------------");
            LinqHelper.sortNumber();
            Console.WriteLine("---------------------------------------------");
            LinqHelper.sortString();
            Console.WriteLine("---------------------------------------------");
            LinqHelper.IsPalidrome();
            Console.WriteLine("---------------------------------------------");


            Test.Test3 obj = new Test.Test3();
            obj.getChanges();
            Console.ReadLine();

        }
    }
}
