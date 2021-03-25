
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NorthwindAPI.ContextService;
using System.Collections.Generic;
using System.Linq;


namespace NorthwindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        //Obtaining an instance from Northwind ContextFactory to avoid creating multiple instances.
        private readonly NorthwindContext northwindContext = NorthwindContextFactory.INSTANCE;

        // Summary:
        //     The getting is performed by OrderId.
        //
        // Parameters:
        //   OrderId:
        //     Id of Order
        //
        // Returns:
        //     Returns Order json with Employee,Customer,OrderDetails with their Product
        //     and Product with their Supplier.
        [HttpGet("{OrderId}")]
        public string GetOrder(int OrderId)
        {
            var order = northwindContext.Orders.Where(o => o.OrderId == OrderId).Join(
                northwindContext.Employees,
                o => o.EmployeeId,
                e => e.EmployeeId,
                (o, e) => new
                {
                    OrderId = o.OrderId,
                    CustomerId = o.CustomerId,
                    EmployeeId = o.EmployeeId,
                    OrderDate = o.OrderDate,
                    RequiredDate = o.RequiredDate,
                    ShippedDate = o.ShippedDate,
                    ShipVia = o.ShipVia,
                    Freight = o.Freight,
                    ShipName = o.ShipName,
                    ShipAddress = o.ShipAddress,
                    ShipCity = o.ShipCity,
                    ShipRegion = o.ShipRegion,
                    ShipPostalCode = o.ShipPostalCode,
                    ShipCountry = o.ShipCountry,
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
                     CustomerId = o.CustomerId,
                     EmployeeId = o.EmployeeId,
                     OrderDate = o.OrderDate,
                     RequiredDate = o.RequiredDate,
                     ShippedDate = o.ShippedDate,
                     ShipVia = o.ShipVia,
                     Freight = o.Freight,
                     ShipName = o.ShipName,
                     ShipAddress = o.ShipAddress,
                     ShipCity = o.ShipCity,
                     ShipRegion = o.ShipRegion,
                     ShipPostalCode = o.ShipPostalCode,
                     ShipCountry = o.ShipCountry,
                     Employee = o.Employee,
                     Customer = new
                     {
                         CustomerId = c.CustomerId,
                         CompanyName = c.CompanyName,
                         ContactName = c.ContactName,
                         ContactTitle = c.ContactTitle,
                         Fax = c.Fax
                     },
                     OrderDetails = northwindContext.OrderDetails.Where(od => od.OrderId == OrderId).Join(
                         northwindContext.Products,
                         od => od.ProductId,
                         p => p.ProductId,
                         (od, p) => new
                         {
                             OrderId = od.OrderId,
                             ProductId = od.ProductId,
                             UnitPrice = od.UnitPrice,
                             Quantity = od.Quantity,
                             Discount = od.Discount,
                             Product = northwindContext.Products.Where(pr => pr.ProductId == p.ProductId).Join(
                                 northwindContext.Suppliers,
                                 p => p.SupplierId,
                                 s => s.SupplierId,
                                 (p, s) => new
                                 {
                                     ProductId = p.ProductId,
                                     ProductName = p.ProductName,
                                     SupplierId = p.SupplierId,
                                     CategoryId = p.CategoryId,
                                     QuantityPerUnit = p.QuantityPerUnit,
                                     UnitPrice = p.UnitPrice,
                                     UnitsInStock = p.UnitsInStock,
                                     UnitsOnOrder = p.UnitsOnOrder,
                                     ReorderLevel = p.ReorderLevel,
                                     Discontinued = p.Discontinued,
                                     Supplier = s
                                 }
                                 ).FirstOrDefault()

                         }
                         ).ToList()
                 }
                ).ToList().FirstOrDefault();


            if (order == null)
            {
                HttpContext.Response.StatusCode = 404;
                return JsonConvert.SerializeObject("Order is not found.");
            }

            return JsonConvert.SerializeObject(order);
        }
    }
}

