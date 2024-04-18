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
    public class AddMenuController : ApiController
    {
        WebApiDemoEntities2 db = new WebApiDemoEntities2();
        //Function to Display Menu
        [HttpGet]
        public HttpResponseMessage DisplayMenu(int id3)
        {
            try
            {
                var image2 = from u in db.AddMenus
                             from w in db.Restaurants
                             where u.restid == w.id & w.id == id3
                             select new
                             {
                                 u.FoodName,
                                 u.FoodType,
                                 u.Category,
                                
                                 u.Price,
                                 u.TimePrepation,
                                 u.pic
                             };
                db.Configuration.ProxyCreationEnabled = false;
                return Request.CreateResponse(HttpStatusCode.OK, image2);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        //Function to Insert New Food Items
        [HttpPost]
        public HttpResponseMessage addMenudb(string foodname, string foodtype, string category,int fprice, string timepreparation,int restid,int peprlvl)
        {
            try
            {
                WebApiDemoEntities2 db = new WebApiDemoEntities2();
                AddMenu am = new AddMenu();
                var request = HttpContext.Current.Request;
                var photo = request.Files["photo"];
                photo.SaveAs(HttpContext.Current.Server.MapPath("~/Content/Uploads/" + photo.FileName));
                System.IO.FileInfo fi = new System.IO.FileInfo(photo.FileName);
                String path = @"/WebApiDemo/Content/Uploads/" + photo.FileName;
                am.pic = path;
                am.FoodName = foodname;
                am.FoodType = foodtype;
                am.Category = category;
                am.Price = fprice;
                am.TimePrepation = timepreparation;
                am.restid = restid;
                am.mirlevel = peprlvl;
                
                db.AddMenus.Add(am);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, am.id);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        //add data in varaition table
        [HttpPost]
        public HttpResponseMessage variation(Variation v)
        {
            try
            {
                db.Variations.Add(v);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, v.id);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
