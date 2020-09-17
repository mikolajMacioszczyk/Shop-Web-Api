using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

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
                        Method = Method.GET.ToString(),
                        Path = "/",
                        Description = "Home Page"
                    }
                },
                People = new {
                    Person = new[]
                    {
                        new NavigationHelper()
                        {
                            Method = Method.GET.ToString(),
                            Path = "api/people",
                            Description = "Return full list of people"
                        },
                        new NavigationHelper()
                        {
                            Method = Method.GET.ToString(),
                            Path = "api/people/:id",
                            Description = "Return person with taken id or NotFound"
                        },
                        new NavigationHelper()
                        {
                            Method = Method.GET.ToString(),
                            Path = "api/people/search",
                            Description = "Return IENumerable<Person> which meets given criteria"
                        }
                    },
                    Customer = new[]
                    {
                        new NavigationHelper()
                        {
                            Method = Method.GET.ToString(),
                            Path = "api/customer",
                            Description = "Return full list of customers"
                        },
                        new NavigationHelper()
                        {
                            Method = Method.GET.ToString(),
                            Path = "api/customer/:id",
                            Description = "Return customer with taken id or NotFound"
                        },
                        new NavigationHelper()
                        {
                            Method = Method.PUT.ToString(),
                            Path = "api/customer/update/:id",
                            Description = "Update customer with taken id or return BadRequest"
                        },
                        new NavigationHelper()
                        {
                            Method = Method.POST.ToString(),
                            Path = "api/customer/create",
                            Description = "Create customer with valid data or return BadRequest"
                        },
                        new NavigationHelper()
                        {
                            Method = Method.DELETE.ToString(),
                            Path = "api/customer/delete/:id",
                            Description = "Delete Customer with valid id or return NotFound"
                        },
                        new NavigationHelper()
                        {
                            Method = Method.GET.ToString(),
                            Path = "api/customer/search",
                            Description = "Return IENumerable<Customer> which meets given criteria"
                        }
                    },
                    Employee = new[]
                    {
                        new NavigationHelper()
                        {
                            Method = Method.GET.ToString(),
                            Path = "api/employee",
                            Description = "Return full list of employees"
                        },
                        new NavigationHelper()
                        {
                            Method = Method.GET.ToString(),
                            Path = "api/employee/:id",
                            Description = "Return employee with taken id or NotFound"
                        },
                        new NavigationHelper()
                        {
                            Method = Method.PUT.ToString(),
                            Path = "api/employee/update/:id",
                            Description = "Update employee with taken id or return BadRequest"
                        },
                        new NavigationHelper()
                        {
                            Method = Method.POST.ToString(),
                            Path = "api/employee/create",
                            Description = "Create employee with valid data or return BadRequest"
                        },
                        new NavigationHelper()
                        {
                            Method = Method.DELETE.ToString(),
                            Path = "api/employee/delete/:id",
                            Description = "Delete employee with valid id or return NotFound"
                        },
                        new NavigationHelper()
                        {
                            Method = Method.GET.ToString(),
                            Path = "api/employee/search",
                            Description = "Return IENumerable<Employee> which meets given criteria"
                        }
                    },
                },
                Addresses = new[]
                {
                    new NavigationHelper()
                    {
                        Method = Method.GET.ToString(),
                        Path = "api/address",
                        Description = "Return full list of addresses"
                    },
                    new NavigationHelper()
                    {
                        Method = Method.GET.ToString(),
                        Path = "api/address/:id",
                        Description = "Return address with taken id or NotFound"
                    },
                    new NavigationHelper()
                    {
                        Method = Method.PUT.ToString(),
                        Path = "api/address/update/:id",
                        Description = "Update address with taken id or return BadRequest"
                    },
                    new NavigationHelper()
                    {
                        Method = Method.POST.ToString(),
                        Path = "api/address/create",
                        Description = "Create address with valid data or return BadRequest"
                    },
                    new NavigationHelper()
                    {
                        Method = Method.DELETE.ToString(),
                        Path = "api/address/delete/:id",
                        Description = "Delete Address with valid id or return NotFound"
                    },
                    new NavigationHelper()
                    {
                        Method = Method.GET.ToString(),
                        Path = "api/address/search",
                        Description = "Return IENumerable<Address> which meets given criteria"
                    }
                },
                Orders = new[]
                {
                    new NavigationHelper()
                    {
                        Method = Method.GET.ToString(),
                        Path = "api/order",
                        Description = "Return full list of orders"
                    },
                    new NavigationHelper()
                    {
                        Method = Method.GET.ToString(),
                        Path = "api/order/:id",
                        Description = "Return order with taken id or NotFound"
                    },
                    new NavigationHelper()
                    {
                        Method = Method.PUT.ToString(),
                        Path = "api/order/update/:id",
                        Description = "Update order with taken id or return BadRequest"
                    },
                    new NavigationHelper()
                    {
                        Method = Method.POST.ToString(),
                        Path = "api/order/create",
                        Description = "Create order with valid data or return BadRequest"
                    },
                    new NavigationHelper()
                    {
                        Method = Method.DELETE.ToString(),
                        Path = "api/order/delete/:id",
                        Description = "Delete Order with valid id or return NotFound"
                    },
                    new NavigationHelper()
                    {
                        Method = Method.GET.ToString(),
                        Path = "api/order/search",
                        Description = "Return IENumerable<Order> which meets given criteria"
                    }
                },
                Furnitures = new
                {
                    BaseType = new[]
                    {
                        new NavigationHelper()
                        {
                            Method = Method.GET.ToString(),
                            Path = "api/furniture",
                            Description = "Return full list of furniture"
                        },
                        new NavigationHelper()
                        {
                            Method = Method.GET.ToString(),
                            Path = "api/furniture/:id",
                            Description = "Return furniture with taken id or NotFound"
                        },
                        new NavigationHelper()
                        {
                            Method = Method.PUT.ToString(),
                            Path = "api/furniture/update/:id",
                            Description = "Update furniture with taken id or return BadRequest"
                        },
                        new NavigationHelper()
                        {
                            Method = Method.GET.ToString(),
                            Path = "api/furniture/search",
                            Description = "Return IEnumerable<Furniture> which meet given criteria"
                        }
                    },
                    Chairs = new[]
                    {
                        new NavigationHelper()
                        {
                            Method = Method.GET.ToString(),
                            Path = "api/chair",
                            Description = "Return full list of chairs"
                        },
                        new NavigationHelper()
                        {
                            Method = Method.GET.ToString(),
                            Path = "api/chair/:id",
                            Description = "Return chair with taken id or NotFound"
                        },
                        new NavigationHelper()
                        {
                            Method = Method.PUT.ToString(),
                            Path = "api/chair/update/:id",
                            Description = "Update chair with taken id or return BadRequest"
                        },
                        new NavigationHelper()
                        {
                            Method = Method.POST.ToString(),
                            Path = "api/chair/create",
                            Description = "Create chair with valid data or return BadRequest"
                        },
                        new NavigationHelper()
                        {
                            Method = Method.DELETE.ToString(),
                            Path = "api/chair/delete/:id",
                            Description = "Delete Chair with valid id or return NotFound"
                        },
                        new NavigationHelper()
                        {
                            Method = Method.GET.ToString(),
                            Path = "api/chair/search",
                            Description = "Return IENumerable<Chair> which meets given criteria"
                        }
                    },
                    Corners = new[]
                    {
                        new NavigationHelper()
                        {
                            Method = Method.GET.ToString(),
                            Path = "api/corner",
                            Description = "Return full list of corners"
                        },
                        new NavigationHelper()
                        {
                            Method = Method.GET.ToString(),
                            Path = "api/corner/:id",
                            Description = "Return corner with taken id or NotFound"
                        },
                        new NavigationHelper()
                        {
                            Method = Method.PUT.ToString(),
                            Path = "api/corner/update/:id",
                            Description = "Update corner with taken id or return BadRequest"
                        },
                        new NavigationHelper()
                        {
                            Method = Method.POST.ToString(),
                            Path = "api/corner/create",
                            Description = "Create corner with valid data or return BadRequest"
                        },
                        new NavigationHelper()
                        {
                            Method = Method.DELETE.ToString(),
                            Path = "api/corner/delete/:id",
                            Description = "Delete Corner with valid id or return NotFound"
                        },
                        new NavigationHelper()
                        {
                            Method = Method.GET.ToString(),
                            Path = "api/corner/search",
                            Description = "Return IENumerable<Corner> which meets given criteria"
                        }
                    },
                    Sofas = new[]
                    {
                        new NavigationHelper()
                        {
                            Method = Method.GET.ToString(),
                            Path = "api/sofa",
                            Description = "Return full list of sofas"
                        },
                        new NavigationHelper()
                        {
                            Method = Method.GET.ToString(),
                            Path = "api/sofa/:id",
                            Description = "Return sofa with taken id or NotFound"
                        },
                        new NavigationHelper()
                        {
                            Method = Method.PUT.ToString(),
                            Path = "api/sofa/update/:id",
                            Description = "Update sofa with taken id or return BadRequest"
                        },
                        new NavigationHelper()
                        {
                            Method = Method.POST.ToString(),
                            Path = "api/sofa/create",
                            Description = "Create sofa with valid data or return BadRequest"
                        },
                        new NavigationHelper()
                        {
                            Method = Method.DELETE.ToString(),
                            Path = "api/sofa/delete/:id",
                            Description = "Delete Sofa with valid id or return NotFound"
                        },
                        new NavigationHelper()
                        {
                            Method = Method.GET.ToString(),
                            Path = "api/sofa/search",
                            Description = "Return IENumerable<Sofa> which meets given criteria"
                        }
                    },
                    Tables = new[]
                    {
                        new NavigationHelper()
                        {
                            Method = Method.GET.ToString(),
                            Path = "api/table",
                            Description = "Return full list of tables"
                        },
                        new NavigationHelper()
                        {
                            Method = Method.GET.ToString(),
                            Path = "api/table/:id",
                            Description = "Return table with taken id or NotFound"
                        },
                        new NavigationHelper()
                        {
                            Method = Method.PUT.ToString(),
                            Path = "api/table/update/:id",
                            Description = "Update table with taken id or return BadRequest"
                        },
                        new NavigationHelper()
                        {
                            Method = Method.POST.ToString(),
                            Path = "api/table/create",
                            Description = $"Create table with valid data or return BadRequest"
                        },
                        new NavigationHelper()
                        {
                            Method = Method.DELETE.ToString(),
                            Path = "api/table/delete/:id",
                            Description = "Delete Table with valid id or return NotFound"
                        },
                        new NavigationHelper()
                        {
                            Method = Method.GET.ToString(),
                            Path = "api/table/search",
                            Description = "Return IENumerable<Table> which meets given criteria"
                        }
                    }
                },
                Collection = new[]
                {
                    new NavigationHelper()
                    {
                        Method = Method.GET.ToString(),
                        Path = "api/collection",
                        Description = "Return full list of collection"
                    },
                    new NavigationHelper()
                    {
                        Method = Method.GET.ToString(),
                        Path = "api/collection/:id",
                        Description = "Return collection with taken id or NotFound"
                    },
                    new NavigationHelper()
                    {
                        Method = Method.PUT.ToString(),
                        Path = "api/collection/update/:id",
                        Description = "Update collection with taken id or return BadRequest"
                    },
                    new NavigationHelper()
                    {
                        Method = Method.POST.ToString(),
                        Path = "api/collection/create",
                        Description = "Create collection with valid data or return BadRequest"
                    },
                    new NavigationHelper()
                    {
                        Method = Method.DELETE.ToString(),
                        Path = "api/collection/delete/:id",
                        Description = "Delete Collection with valid id or return NotFound"
                    },
                    new NavigationHelper()
                    {
                        Method = Method.GET.ToString(),
                        Path = "api/collection/search",
                        Description = "Return IENumerable<Collection> which meets given criteria"
                    }
                }
            };
            list.Add(returnObj);
            return Ok(list);
        }
    }
}