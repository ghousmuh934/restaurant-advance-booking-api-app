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
    public class WaiterController : ApiController
    {
        WebApiDemoEntities2 db = new WebApiDemoEntities2();
        [HttpGet]
        public HttpResponseMessage WaiterDashboard(int wid)
        {
            try
            {
                var image = from w in db.AddWaiters
                            from u in db.Users
                            where w.uid == u.id & u.id == wid
                            select new
                            {
                                u.Name,
                                u.username,
                                u.Email,
                                w.contactno,
                                w.pic
                            };
                db.Configuration.ProxyCreationEnabled = false;
                return Request.CreateResponse(HttpStatusCode.OK, image);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //Add Chef by Manager
        [HttpPost]
        public HttpResponseMessage AddChefByManagerUsertbl(User u)
        {
            try
            {
                db.Users.Add(u);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, u.id);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        //Function to Insert New Chef
        [HttpPost]
        public HttpResponseMessage AddChefbyManagerCheftbl(string chefcontactno, string speciality, string specific, int userid1, int rsid1)
        {
            try
            {
                WebApiDemoEntities2 db = new WebApiDemoEntities2();
                AddChef ac = new AddChef();
                var request = HttpContext.Current.Request;
                // photo is used on android side you can use any key // instead of photo
                var photo = request.Files["photo"];
                photo.SaveAs(HttpContext.Current.Server.MapPath("~/Content/Uploads/" + photo.FileName));
                System.IO.FileInfo fi = new System.IO.FileInfo(photo.FileName);
                String path = @"/WebApiDemo/Content/Uploads/" + photo.FileName;
                //String newpath = @"/memoryjogger/Content/Uploads/" + newname;
                ac.picture = path;
                ac.contactno = chefcontactno;
                ac.speciality = speciality;
                ac.specific = specific;
                ac.userid = userid1;
                ac.rsid = rsid1;
                //ac.password = password;
                //db.caretakers.Add(ct);
                db.AddChefs.Add(ac);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, ac.id);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
