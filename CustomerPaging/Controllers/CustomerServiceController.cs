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
            long fromCustomer = 0;
            long toCustomer = long.MaxValue;
            var range = Request.Headers.Range;
            if (range != null)
            {
                fromCustomer = range.Ranges.First().From.Value;
                toCustomer = range.Ranges.First().To.Value;
            }

            var customerList =
                CustomerTable.Skip((int) fromCustomer).Take((int) toCustomer - (int) fromCustomer + 1).ToList();

            return new CustomerGetListResult(Request, customerList, fromCustomer,
                fromCustomer + customerList.Count() - 1, CustomerTable.Count());
        }

        // GET: api/CustomerService/5
        public Customer Get(long id)
        {
            return CustomerTable.SingleOrDefault(customer => customer.Id == id);
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

        private static readonly Customer[] CustomerTable =
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
    }
}