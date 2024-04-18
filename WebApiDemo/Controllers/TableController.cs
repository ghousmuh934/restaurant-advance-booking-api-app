using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiDemo.Models;

namespace WebApiDemo.Controllers
{
    public class TableController : ApiController
    {
        WebApiDemoEntities2 db = new WebApiDemoEntities2();
        [HttpGet]
        public HttpResponseMessage table()
        {
            try
            {
                var image = from w in db.table_layout
                            
                            select new
                            {
                                w.table1

                            };
                db.Configuration.ProxyCreationEnabled = false;
                return Request.CreateResponse(HttpStatusCode.OK, image);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage setTableLayout(SetTable u)
        {
            try
            {
                db.SetTables.Add(u);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, u.id);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpGet]
        public HttpResponseMessage DisplayTable(int id)
        {
            try
            {
                var displaytable = from r in db.Restaurants
                            from s in db.SetTables
                            where s.tblrestid==r.id & s.tblrestid == id orderby s.id ascending
                            select new
                            {
                                s.id,
                                s.table_image,
                                s.table_no,
                                s.capacity,
                                s.status

                            };
                db.Configuration.ProxyCreationEnabled = false;
                return Request.CreateResponse(HttpStatusCode.OK, displaytable);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}
