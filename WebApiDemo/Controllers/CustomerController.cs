using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApiDemo.Models;

namespace WebApiDemo.Controllers
{
    public class CustomerController : ApiController
    {
        WebApiDemoEntities2 db = new WebApiDemoEntities2();
        [HttpGet]
        public HttpResponseMessage DisplayCustomer(int cid)
        {
            
            try
            {
                var restdata = from u in db.Users

                               where u.id == cid
                               select new
                               {
                                   u.id,
                                   u.Name,
                                   u.Email,
                                   u.username,
                                   u.Password,
                                   

                               };
                db.Configuration.ProxyCreationEnabled = false;
                return Request.CreateResponse(HttpStatusCode.OK, restdata);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage MenuCustomer(int reid)
        {
            try
            {
                var menu = from m in db.AddMenus
                           from r in db.Restaurants
                           

                           where m.restid == r.id & r.id == reid 
                           select new
                           {
                               m.id,
                               m.FoodName,
                               m.FoodType,
                               m.Category,
                               m.TimePrepation,
                               m.pic,
                               m.Price,
                               m.mirlevel
                               


                           };
                db.Configuration.ProxyCreationEnabled = false;
                return Request.CreateResponse(HttpStatusCode.OK, menu);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage VariationData(int vid)
        {
            try
            {
                var variation = from v in db.Variations
                                where v.fid == vid
                                select new
                                {
                                    v.varName,
                                    v.price


                                };
                db.Configuration.ProxyCreationEnabled = false;
                return Request.CreateResponse(HttpStatusCode.OK, variation);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage CustomerOrdersTrack(int id)
        {
            try
            {
                var orderData = from o in db.Order_Detail
                                from c in db.Users
                                from r in db.Restaurants
                                where o.usrid == c.id & c.id == id & o.retid==r.id
                                select new
                                {
                                    o.id,
                                    o.FName,
                                    o.Ftype,
                                    o.FCat,
                                    o.FTimePrep,
                                    o.FQuant,
                                    o.FPrice,
                                    o.UnitTag,
                                    o.TimeDate,
                                    o.FImage,
                                    o.Status,
                                    c.Name,
                                    c.Email,
                                    r.RestName,
                                    r.RestLocation,
                                    r.RestPhNum

                                };
                db.Configuration.ProxyCreationEnabled = false;
                return Request.CreateResponse(HttpStatusCode.OK, orderData);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        public HttpResponseMessage FoodList(int id)
        {
            try
            {
                var menu = from m in db.AddMenus
                           from r in db.Restaurants


                           where m.restid == r.id & r.id == id
                           select new
                           {
                               m.id,
                               m.FoodName,
                               m.FoodType,
                               m.Category,
                               m.TimePrepation,
                               m.pic,
                               m.Price,
                               m.mirlevel

                           };
                db.Configuration.ProxyCreationEnabled = false;
                return Request.CreateResponse(HttpStatusCode.OK, menu);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        //low data
        [HttpGet]
        public HttpResponseMessage low(int id,int mr)
        {
            try
            {
                var menu = from m in db.AddMenus
                           from r in db.Restaurants


                           where m.restid == r.id & r.id == id & m.mirlevel==mr
                           select new
                           {
                               m.id,
                               m.FoodName,
                               m.FoodType,
                               m.Category,
                               m.TimePrepation,
                               m.pic,
                               m.Price,
                               m.mirlevel

                           };
                db.Configuration.ProxyCreationEnabled = false;
                return Request.CreateResponse(HttpStatusCode.OK, menu);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        //medium 
        [HttpGet]
        public HttpResponseMessage medium(int id)
        {
            try
            {
                var menu = from m in db.AddMenus
                           from r in db.Restaurants


                           where m.restid == r.id & r.id == id  & m.mirlevel==2
                           select new
                           {
                               m.id,
                               m.FoodName,
                               m.FoodType,
                               m.Category,
                               m.TimePrepation,
                               m.pic,
                               m.Price,
                               m.mirlevel

                           };
                db.Configuration.ProxyCreationEnabled = false;
                return Request.CreateResponse(HttpStatusCode.OK, menu);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        //high
        [HttpGet]
        public HttpResponseMessage high(int id)
        {
            try
            {
                var menu = from m in db.AddMenus
                           from r in db.Restaurants


                           where m.restid == r.id & r.id == id & m.mirlevel==3
                           select new
                           {
                               m.id,
                               m.FoodName,
                               m.FoodType,
                               m.Category,
                               m.TimePrepation,
                               m.pic,
                               m.Price,
                               m.mirlevel

                           };
                db.Configuration.ProxyCreationEnabled = false;
                return Request.CreateResponse(HttpStatusCode.OK, menu);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}
