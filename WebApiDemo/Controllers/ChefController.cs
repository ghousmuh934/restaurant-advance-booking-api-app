using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiDemo.Models;

namespace WebApiDemo.Controllers
{
    public class ChefController : ApiController
    {
        WebApiDemoEntities2 db = new WebApiDemoEntities2();

        [HttpGet]
        public HttpResponseMessage ChefOrders(int id)
        {
            try
            {
                //var cheforder = db.Order_Detail.Where(x => x.FName == Speciality);       
                    var cheforder = from o in db.Order_Detail
                                   from c in db.AddChefs
                                   from u in db.Users
                                   
                                   where o.FName == c.speciality & c.rsid == o.retid & u.id==id
                                   select new
                                   {
                                       o.id,
                                       o.FName,
                                       o.Ftype,
                                       o.FCat,
                                       o.FTimePrep,
                                       o.FQuant,
                                       o.FPrice,
                                       o.TimeDate,
                                       o.FImage,
                                       o.Status,
                                      
                                   };
                    db.Configuration.ProxyCreationEnabled = false;
                    return Request.CreateResponse(HttpStatusCode.OK, cheforder);
               
            }
            catch (Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }


        

    }
}
