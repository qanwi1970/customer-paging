using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using CustomerPaging.Models;
using CustomerPaging.Results;

namespace CustomerPaging.Controllers
{
    public class CustomerServiceController : ApiController
    {
        // GET: api/CustomerService
        public CustomerGetListResult Get()
        {
            long? fromCustomer = 0;
            long? toCustomer = 27;
            var range = Request.Headers.Range;
            if (range != null)
            {
                fromCustomer = range.Ranges.First().From;
                toCustomer = range.Ranges.First().To;
            }

            var customerList = new[]
            {
                new Customer
                {
                    Id = 1,
                    Name = "Darth Vader",
                    FavoriteColor = "Black"
                },
                new Customer
                {
                    Id = 2,
                    Name = "Luke Skywalker",
                    FavoriteColor = "White"
                }
            };

            return new CustomerGetListResult(Request, customerList.ToList(), fromCustomer.Value, toCustomer.Value, 1000);
        }

        // GET: api/CustomerService/5
        public Customer Get(long id)
        {
            return new Customer
            {
                Id = 1,
                Name = "Darth Vader",
                FavoriteColor = "Black"
            };
        }

        // POST: api/CustomerService
        public void Post([FromBody] Customer value)
        {
        }

        // PUT: api/CustomerService/5
        public void Put(long id, [FromBody] Customer value)
        {
        }

        // DELETE: api/CustomerService/5
        public void Delete(long id)
        {
        }
    }
}