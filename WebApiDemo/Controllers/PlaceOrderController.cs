using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiDemo.Models;

namespace WebApiDemo.Controllers
{
    public class PlaceOrderController : ApiController
    {
        WebApiDemoEntities2 db = new WebApiDemoEntities2();
        [HttpPost]
        public HttpResponseMessage OrderDetails(Order_Detail u)
        {
            try
            {
                db.Order_Detail.Add(u);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, u.id);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //Show For Customer Place Order to restaurant manager 
        [HttpGet]
        public HttpResponseMessage DisplayCustomerOrders(int rstid)
        {
            try
            {               
                var orderData = from o in db.Order_Detail
                             from c in db.Users
                            
                             where o.retid==rstid & o.usrid==c.id 
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
                                 c.Email
                             };
                db.Configuration.ProxyCreationEnabled = false;
                return Request.CreateResponse(HttpStatusCode.OK, orderData);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


        
    }
}
