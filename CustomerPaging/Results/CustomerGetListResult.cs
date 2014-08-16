using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using CustomerPaging.Models;

namespace CustomerPaging.Results
{
    public class CustomerGetListResult : IHttpActionResult
    {
        private readonly HttpRequestMessage _request;
        private readonly List<Customer> _customers;
        private readonly long _from;
        private readonly long _to;
        private readonly long? _length;

        public CustomerGetListResult(HttpRequestMessage request, List<Customer> customers, long from, long to,
            long? length)
        {
            _request = request;
            _customers = customers;
            _from = from;
            _to = to;
            _length = length;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpStatusCode code;
            if (_length.HasValue)
            {
                code = _length - 1 == _to ? HttpStatusCode.OK : HttpStatusCode.PartialContent;
            }
            else
            {
                code = HttpStatusCode.OK;
            }
            var response = _request.CreateResponse(code, _customers);
            response.Content.Headers.ContentRange = _length.HasValue
                ? new ContentRangeHeaderValue(_from, _to, _length.Value)
                : new ContentRangeHeaderValue(_from, _to);
            response.Content.Headers.ContentRange.Unit = "customers";

            return Task.FromResult(response);
        }
    }
}