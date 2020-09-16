using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ShopApi.Models.People;

namespace ShopApi.Controllers.Home
{
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult<IEnumerable<object>> Index()
        {
            var list = new List<object>();
            list.Add("Welcome in Shop Web API");
            list.Add("Available requests:");
            var returnObj = new
            {
                Home = new []
                {
                    new NavigationHelper()
                    {
                        Method = Method.GET,
                        Path = "/",
                        Description = "Home Page"
                    }
                },
                People = new {
                    Customer = new[]
                    {
                        new NavigationHelper()
                        {
                            Method = Method.GET,
                            Path = "/customer",
                            Description = "Return full list of customers"
                        },
                        new NavigationHelper()
                        {
                            Method = Method.POST,
                            Path = "/customer/:id",
                            Description = "Return customer with taken id or NotFound"
                        }
                    },
                    Employee = new[]
                    {
                        new NavigationHelper()
                        {
                            Method = Method.GET,
                            Path = "/employee",
                            Description = "Return full list of employees"
                        },
                        new NavigationHelper()
                        {
                            Method = Method.POST,
                            Path = "/employee/:id",
                            Description = "Return employee with taken id or NotFound"
                        }
                    },
                },
                Orders = new[]
                {
                    new NavigationHelper()
                    {
                        Method = Method.GET,
                        Path = "/order",
                        Description = "Return full list of orders"
                    },
                    new NavigationHelper()
                    {
                        Method = Method.POST,
                        Path = "/order/:id",
                        Description = "Return order with taken id or NotFound"
                    }
                },
                Furnitures = new
                {
                    BaseType = new[]
                    {
                        new NavigationHelper()
                        {
                            Method = Method.GET,
                            Path = "/furniture",
                            Description = "Return full list of furniture"
                        },
                        new NavigationHelper()
                        {
                            Method = Method.POST,
                            Path = "/furniture/:id",
                            Description = "Return furniture with taken id or NotFound"
                        }
                    },
                    Chairs = new[]
                    {
                        new NavigationHelper()
                        {
                            Method = Method.GET,
                            Path = "/chair",
                            Description = "Return full list of chairs"
                        },
                        new NavigationHelper()
                        {
                            Method = Method.POST,
                            Path = "/chair/:id",
                            Description = "Return chair with taken id or NotFound"
                        }
                    },
                    Corners = new[]
                    {
                        new NavigationHelper()
                        {
                            Method = Method.GET,
                            Path = "/corner",
                            Description = "Return full list of corners"
                        },
                        new NavigationHelper()
                        {
                            Method = Method.POST,
                            Path = "/corner/:id",
                            Description = "Return corner with taken id or NotFound"
                        }
                    },
                    Sofas = new[]
                    {
                        new NavigationHelper()
                        {
                            Method = Method.GET,
                            Path = "/sofa",
                            Description = "Return full list of sofas"
                        },
                        new NavigationHelper()
                        {
                            Method = Method.POST,
                            Path = "/sofa/:id",
                            Description = "Return sofa with taken id or NotFound"
                        }
                    },
                    Tables = new[]
                    {
                        new NavigationHelper()
                        {
                            Method = Method.GET,
                            Path = "/table",
                            Description = "Return full list of tables"
                        },
                        new NavigationHelper()
                        {
                            Method = Method.POST,
                            Path = "/table/:id",
                            Description = "Return table with taken id or NotFound"
                        }
                    }
                }
            };
            list.Add(returnObj);
            return Ok(list);
        }
    }
}