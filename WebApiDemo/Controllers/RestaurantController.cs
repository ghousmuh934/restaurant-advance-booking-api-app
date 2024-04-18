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
    public class RestaurantController : ApiController
    {
        WebApiDemoEntities2 db = new WebApiDemoEntities2();

        
        [HttpGet]
        public HttpResponseMessage DisplayRestaurantProfile(int id2)
        {
            try
            {
                var restdata = from r in db.Restaurants
                            from u in db.Users

                            where r.rid == u.id & r.rid == id2
                            select new
                            {   
                                r.id,
                                r.RestName,
                                r.RestLocation,
                                r.RestPhNum,
                                r.RestStars,
                                r.RestPic
                            };
                db.Configuration.ProxyCreationEnabled = false;
                return Request.CreateResponse(HttpStatusCode.OK, restdata);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage AddRestaurant(string restname, string restlocation, string restphno, string reststars,int rid)
        {
            try
            {



                Restaurant rs = new Restaurant();

                var request = HttpContext.Current.Request;
                var photo = request.Files["photo"];
                photo.SaveAs(HttpContext.Current.Server.MapPath("~/Content/Uploads/" + photo.FileName));


                System.IO.FileInfo fi = new System.IO.FileInfo(photo.FileName);
               
                String path = @"/WebApiDemo/Content/Uploads/" + photo.FileName;
                rs.RestPic = path;
                rs.RestName = restname;
                rs.RestLocation = restlocation;
                rs.RestPhNum = restphno;
                rs.RestStars = reststars;
                rs.rid=rid;
                //db.caretakers.Add(ct);
                db.Restaurants.Add(rs);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, rs.id);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage DisplayRestauranttoCustomer()
        {
            try
            {
                var displayRest = from r in db.Restaurants
                
                             select new
                             {
                                 r.id,
                                 r.RestName,
                                 r.RestLocation,
                                 r.RestPhNum,
                                 r.RestStars,
                                 r.RestPic
                             };
                db.Configuration.ProxyCreationEnabled = false;
                return Request.CreateResponse(HttpStatusCode.OK, displayRest);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
