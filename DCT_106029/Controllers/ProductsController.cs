﻿using System;
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
    /// <summary>
    /// 商品相關 APIs
    /// </summary>
    [RoutePrefix("products")]
    public class ProductsController : ApiController
    {
        private FabricsEntities db = new FabricsEntities();

        public ProductsController()
        {
            db.Configuration.LazyLoadingEnabled = false;
        }

        // GET: api/Products
        [Route("")]
        public IQueryable<Product> GetProduct()
        {
            return db.Product;
        }

        // GET: api/Products/5
        /// <summary>
        /// 取得單筆商品
        /// </summary>
        /// <param name="id">ProductId</param>
        /// <returns></returns>
        [ResponseType(typeof(Product))]
        [Route("{id:int}", Name = "GetProductById")]
        public IHttpActionResult GetProduct(int id)
        {
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }


        [ResponseType(typeof(List<OrderLine>))]
        [Route("{id:int}/orderlines")]
        public IHttpActionResult GetProductOrderLines(int id)
        {
            var orderlines = db.OrderLine.Where(p => p.ProductId == id);
            return Ok(orderlines);
        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        [Route("{id:int}")]
        [ValidateModel]
        public IHttpActionResult PutProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Products
        [ResponseType(typeof(Product))]
        [Route("")]
        [HttpPost]
        [ValidateModel]
        public IHttpActionResult CreateProduct(Product product)
        {
            db.Product.Add(product);
            db.SaveChanges();

            return CreatedAtRoute("GetProductById", new { id = product.ProductId }, product);
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(Product))]
        [Route("{id:int}")]
        public IHttpActionResult DeleteProduct(int id)
        {
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            db.Product.Remove(product);
            db.SaveChanges();

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Product.Count(e => e.ProductId == id) > 0;
        }
    }
}