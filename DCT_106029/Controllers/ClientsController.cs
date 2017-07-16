using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DCT_106029.Models;

namespace DCT_106029.Controllers
{
    [RoutePrefix("clients")]
    public class ClientsController : ApiController
    {
        private FabricsEntities db = new FabricsEntities();

        public ClientsController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        // GET: api/Clients
        [Route("")]
        public IHttpActionResult GetClient()
        {
            return Ok(db.Client);
        }

        // GET: api/Clients/5
        [ResponseType(typeof(Client))]
        [Route("{id:int}")]
        public HttpResponseMessage GetClient(int id)
        {
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new ObjectContent<Client>(client,
                    GlobalConfiguration.Configuration.Formatters.JsonFormatter)
            };
        }

        [Route("test1")]
        [HttpGet]
        public HttpResponseMessage Test1()
        {
            return new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("酷奇資訊")
            };
        }

        [Route("test2")]
        [HttpGet]
        public string Test2()
        {
            return "酷奇資訊";
        }

        [ResponseType(typeof(List<Order>))]
        [Route("{id:int}/orders")]
        public IHttpActionResult GetOrdersByClientId(int id)
        {
            var orders = db.Order.Where(p => p.ClientId == id);
            return Ok(orders);
        }


        [ResponseType(typeof(List<Order>))]
        [Route("{id:int}/orders")]
        public IHttpActionResult GetOrdersByClientIdAndDateTime(int id, DateTime dt)
        {
            var orders = db.Order
                .Where(p => p.ClientId == id && p.OrderDate > dt);
            return Ok(orders);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClientExists(int id)
        {
            return db.Client.Count(e => e.ClientId == id) > 0;
        }
    }
}