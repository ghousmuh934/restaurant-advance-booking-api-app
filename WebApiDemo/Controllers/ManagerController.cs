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
    public class ManagerController : ApiController
    {
        WebApiDemoEntities2 db = new WebApiDemoEntities2();
       
        //Function to display All Waiter
        [HttpGet]
        public HttpResponseMessage DisplayWaiter(int idd)
        {
            try
            {
                var image = from w in db.AddWaiters
                            from u in db.Users
                            from r in db.Restaurants
                            

                            
                            where w.uid == u.id & u.id==idd
                            select new
                            {
                                u.Name,
                                u.username,
                                u.Email,

                                w.contactno,
                                w.pic,
                                



            };
                db.Configuration.ProxyCreationEnabled = false;
                return Request.CreateResponse(HttpStatusCode.OK, image);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        //Add Waiter By Manager
        [HttpPost]
        public HttpResponseMessage AddWaiterByManager(User u)
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
        //Function to Insert New Waiter
        [HttpPost]
        public HttpResponseMessage AddWaiter(string name,string contactno,string username, string password)
        {
            try
            {
                //caretaker ct = new caretaker();

                WebApiDemoEntities2 db = new WebApiDemoEntities2();
                //ImageandText ct = new ImageandText();
                AddWaiter aw = new AddWaiter();

                //var cid = db.caretakers.OrderByDescending(o => o.caretakerid).Select(s => new { s.caretakerid }).FirstOrDefault();
                //int id = cid.caretakerid + 1;
                var request = HttpContext.Current.Request;
                // photo is used on android side you can use any key // instead of photo
                var photo = request.Files["photo"];
                photo.SaveAs(HttpContext.Current.Server.MapPath("~/Content/Uploads/" + photo.FileName));


                System.IO.FileInfo fi = new System.IO.FileInfo(photo.FileName);
                // Check if file is there  
                //String ext = fi.Extension;
                //String newname = username.ToString() + ext;
                String path = @"/WebApiDemo/Content/Uploads/" + photo.FileName;
                //String newpath = @"/memoryjogger/Content/Uploads/" + newname;
                aw.pic = path;
                //aw.Name = name;
                //aw.contactno = contactno;
                //aw.username = username;
                //aw.password = password;
                //db.caretakers.Add(ct);
                db.AddWaiters.Add(aw);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, aw.id);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        //Function to display Chef
        [HttpGet]
        public HttpResponseMessage DisplayChef(int id5)
        {
            try
            {
               var chefdata = from c in db.AddChefs
                              from u in db.Users
                              from r in db.Restaurants
                              where  c.userid==u.id & c.rsid== r.id & r.id == id5
                                         select new
                                         {
                                             u.id,
                                             u.Name,
                                             u.username,
                                             u.Email,
                                             u.Password,
                                             c.speciality,
                                             c.specific,
                                             c.contactno,
                                             c.picture

                                         };
                db.Configuration.ProxyCreationEnabled = false;
                return Request.CreateResponse(HttpStatusCode.OK, chefdata);
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
        public HttpResponseMessage AddChefbyManagerCheftbl(string chefcontactno,string speciality,string specific,int userid1,int rsid1)
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
                ac.speciality= speciality;
                ac.specific=  specific;
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
