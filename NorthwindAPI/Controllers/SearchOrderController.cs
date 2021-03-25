using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NorthwindAPI.ContextService;
using System.Collections.Generic;
using System.Linq;

namespace NorthwindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchOrderController : ControllerBase
    {
        //Obtaining an instance from Northwind ContextFactory to avoid creating multiple instances.
        private readonly NorthwindContext northwindContext = NorthwindContextFactory.INSTANCE;

        // Summary:
        //     The search is performed by CustomerId,EmployeeId.
        //
        // Parameters:
        //   CustomerId:
        //     Id of Customer
        //   EmployeeId:
        //     Id of Employee
        //
        // Returns:
        //     Returns Orders json with Employees and Customers,
        //     that EmloyeeId is equal to the EmployeeId of the parameters
        //     and that CustomerId is equal to the CustomerId of the parameters.

        [HttpGet("{CustomerId}/{EmployeeId}")]
        public string SearchOrder(string CustomerId, int EmployeeId)
        {
            var order = northwindContext.Orders.Where(o => o.CustomerId == CustomerId && o.EmployeeId == EmployeeId).Join(
                    northwindContext.Employees,
                    o => o.EmployeeId,
                    e => e.EmployeeId,
                    (o, e) => new
                    {
                        OrderId = o.OrderId,
                        OrderDate = o.OrderDate,
                        CustomerId = o.CustomerId,
                        ShipName = o.ShipName,
                        Employee = new
                        {
                            EmployeeId = e.EmployeeId,
                            LastName = e.LastName,
                            FirstName = e.FirstName,
                            Title = e.Title,
                            TitleOfCourtesy = e.TitleOfCourtesy,
                            BirthDate = e.BirthDate,
                            HireDate = e.HireDate,
                        },
                    }
                ).Join(
                     northwindContext.Customers,
                 o => o.CustomerId,
                 c => c.CustomerId,
                 (o, c) => new
                 {
                     OrderId = o.OrderId,
                     OrderDate = o.OrderDate,
                     Employee = o.Employee,
                     ShipName = o.ShipName,
                     Customer = new
                     {
                         CustomerId = c.CustomerId,
                         CompanyName = c.CompanyName,
                         ContactName = c.ContactName,
                         ContactTitle = c.ContactTitle,
                         Fax = c.Fax
                     }
                 }
                );
            if (order.FirstOrDefault() == null)
            {
                HttpContext.Response.StatusCode = 404;
                return JsonConvert.SerializeObject("Order is not found.");
            }

            return JsonConvert.SerializeObject(order);
        }

        // Summary:
        //     The search is performed by ShipName,CustomerId,EmployeeId.
        //
        // Parameters:
        //   ShipName:
        //     Ship name
        //   CustomerId:
        //     Id of Customer
        //   EmployeeId:
        //     Id of Employee
        //
        // Returns:
        //     Returns Orders json with Employees and Customers,
        //     that contains a ShipName in their ShipNames and that EmloyeeId is equal to the EmployeeId of the parameters
        //     and that CustomerId is equal to the CustomerId of the parameters.

        [HttpGet("{ShipName}/{CustomerId}/{EmployeeId}")]
        public string SearchOrder(string ShipName, string CustomerId, int EmployeeId)
        {
            var order = northwindContext.Orders.Where(o => o.CustomerId == CustomerId &&
                                                      o.EmployeeId == EmployeeId &&
                                                      o.ShipName.Contains(ShipName))
                                               .Join(
                                                    northwindContext.Employees,
                                                    o => o.EmployeeId,
                                                    e => e.EmployeeId,
                                                    (o, e) => new
                                                    {
                                                        OrderId = o.OrderId,
                                                        OrderDate = o.OrderDate,
                                                        ShipName = o.ShipName,
                                                        CustomerId = o.CustomerId,
                                                        Employee = new
                                                        {
                                                            EmployeeId = e.EmployeeId,
                                                            LastName = e.LastName,
                                                            FirstName = e.FirstName,
                                                            Title = e.Title,
                                                            TitleOfCourtesy = e.TitleOfCourtesy,
                                                            BirthDate = e.BirthDate,
                                                            HireDate = e.HireDate,
                                                        },
                                                    }
                                                ).Join(
                                                     northwindContext.Customers,
                                                 o => o.CustomerId,
                                                 c => c.CustomerId,
                                                 (o, c) => new
                                                 {
                                                     OrderId = o.OrderId,
                                                     OrderDate = o.OrderDate,
                                                     ShipName = o.ShipName,
                                                     Employee = o.Employee,
                                                     Customer = new
                                                     {
                                                         CustomerId = c.CustomerId,
                                                         CompanyName = c.CompanyName,
                                                         ContactName = c.ContactName,
                                                         ContactTitle = c.ContactTitle,
                                                         Fax = c.Fax
                                                     }
                                                 }
                                                );
            if (order.FirstOrDefault() == null)
            {
                HttpContext.Response.StatusCode = 404;
                return JsonConvert.SerializeObject("Order is not found.");
            }

            return JsonConvert.SerializeObject(order);
        }

        // Summary:
        //     The search is performed by ShipName.
        //
        // Parameters:
        //   ShipName:
        //     Ship name
        //
        // Returns:
        //     Returns Orders json with Employees and Customers,
        //     that contains a ShipName in their ShipNames  
        [HttpGet("{ShipName}")]
        public string SearchOrder(string ShipName)
        {
            var order = northwindContext.Orders.Where(o => o.ShipName.Contains(ShipName))
                                               .Join(
                                                    northwindContext.Employees,
                                                    o => o.EmployeeId,
                                                    e => e.EmployeeId,
                                                    (o, e) => new
                                                    {
                                                        OrderId = o.OrderId,
                                                        OrderDate = o.OrderDate,
                                                        ShipName = o.ShipName,
                                                        CustomerId = o.CustomerId,
                                                        Employee = new
                                                        {
                                                            EmployeeId = e.EmployeeId,
                                                            LastName = e.LastName,
                                                            FirstName = e.FirstName,
                                                            Title = e.Title,
                                                            TitleOfCourtesy = e.TitleOfCourtesy,
                                                            BirthDate = e.BirthDate,
                                                            HireDate = e.HireDate,
                                                        },
                                                    }
                                                ).Join(
                                                     northwindContext.Customers,
                                                 o => o.CustomerId,
                                                 c => c.CustomerId,
                                                 (o, c) => new
                                                 {
                                                     OrderId = o.OrderId,
                                                     OrderDate = o.OrderDate,
                                                     ShipName = o.ShipName,
                                                     Employee = o.Employee,
                                                     Customer = new
                                                     {
                                                         CustomerId = c.CustomerId,
                                                         CompanyName = c.CompanyName,
                                                         ContactName = c.ContactName,
                                                         ContactTitle = c.ContactTitle,
                                                         Fax = c.Fax
                                                     }
                                                 }
                                                );
            if (order.FirstOrDefault() == null)
            {
                HttpContext.Response.StatusCode = 404;
                return JsonConvert.SerializeObject("Order is not found.");
            }

            return JsonConvert.SerializeObject(order);
        }
    }
}
