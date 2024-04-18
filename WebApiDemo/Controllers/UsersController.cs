using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Data.Entity;
using WebApiDemo.Models;

namespace WebApiDemo.Controllers
{
    public class UsersController : ApiController
    {
        WebApiDemoEntities2 db = new WebApiDemoEntities2();
        [HttpGet]
        public HttpResponseMessage AllUsers()
        {
            //Select * from User
            var users = from u in db.Users
                        select new
                        {                          
                            u.id,
                            u.Name,
                            u.username,
                            u.Email,
                            u.LoginStatus,        
                     };
            return Request.CreateResponse(HttpStatusCode.OK, users);
        }

        [HttpPost]
        public HttpResponseMessage RegisterUsers(User user)
        {
            //Insert into User
            try
            {
                var users = db.Users.Add(user);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, users.id);
            }catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed,ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage DeleteUsers(int id)
        {
            //Insert into User
            try
            {
                var users = db.Users.FirstOrDefault(x => x.id == id);
                //If User not found
                if(users == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, "User Not Found");
                db.Users.Remove(users);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK,"Deleted Successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [HttpGet]
        public HttpResponseMessage ModifyUsers(User user)
        {
            //Insert into User
            try
            {
                var users1 = db.Users.FirstOrDefault(x => x.id == user.id);
                //If User not found
                if (users1 == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, "User Not Found");
                users1.Name = user.Name;
                users1.Email = user.Email;
                users1.Password = user.Password;
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, "Modify Successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.ExpectationFailed, ex.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage LoginUser(string email, string password)
        {
            //Select * from User where
            try
            {
                var users = db.Users.Where(x => x.Email == email && x.Password == password);
                if (users.Count() > 0)
                {
                    var userdata = from u in db.Users
                                   where u.Email == email & u.Password==password
                                   select new
                                {
                                    u.id,
                                    u.Name,
                                    u.Email,
                                   
                                    u.LoginStatus
                                   };
                    return Request.CreateResponse(HttpStatusCode.OK, userdata);
                }
                else
                    return Request.CreateResponse(HttpStatusCode.NotFound, false);
            }
            catch(Exception e)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage adduser(User u)
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
        [HttpPost]
        public HttpResponseMessage login_user(User u)
        {
            try
            {
                var userFound = db.Users.FirstOrDefault(b => b.Email == u.Email && b.Password == u.Password);
                if (userFound != null)
                {
                     return Request.CreateResponse(HttpStatusCode.OK, u.LoginStatus);
                    //return Request.CreateResponse(HttpStatusCode.OK, new { id = u.id, status = u.loginstatus});
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Invalid Email or Password");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        //[HttpPost]
        //public HttpResponseMessage SaveFile()
        //{
        //    //Create HTTP Response.
        //    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
        //    //Check if Request contains File.
        //    if (HttpContext.Current.Request.Files.Count == 0)
        //    {
        //        throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
        //    }
        //    //Read the File data from Request.Form collection.
        //    HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];

        //    //Convert the File data to Byte Array.
        //    byte[] bytes;
        //    using (BinaryReader br = new BinaryReader(postedFile.InputStream))
        //    {
        //        bytes = br.ReadBytes(postedFile.ContentLength);
        //    }
        //    //Insert the File to Database Table.
        //    WebApiDemoEntities db = new WebApiDemoEntities();
        //    tblFile file = new tblFile
        //    {
        //        Name = Path.GetFileName(postedFile.FileName),
        //        ContentType = postedFile.ContentType,
        //        Data = bytes
        //    };
        //    db.tblFiles.Add(file);
        //    db.SaveChanges();
        //    return Request.CreateResponse(HttpStatusCode.OK, new { id = file.id, Name = file.Name });
        //}

        //[HttpGet]
        //public HttpResponseMessage GetFile(int fileId)
        //{
        //    //Create HTTP Response.
        //    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);

        //    //Fetch the File data from Database.
        //    ApiDemoEntities2 db = new ApiDemoEntities2();
        //    tblFile file = db.tblFiles.ToList().Find(p => p.id == fileId);

        //    //Set the Response Content.
        //    response.Content = new ByteArrayContent(file.Data);

        //    //Set the Response Content Length.
        //    response.Content.Headers.ContentLength = file.Data.LongLength;

        //    //Set the Content Disposition Header Value and FileName.
        //    response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
        //    response.Content.Headers.ContentDisposition.FileName = file.Name;

        //    //Set the File Content Type.
        //    response.Content.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

        //   return response;
        //}

        /*[HttpGet]
        public HttpResponseMessage getImage()
        {
            try
            {
                var image4 = db.caretakers.ToList();
                db.Configuration.ProxyCreationEnabled = false;
                return Request.CreateResponse(HttpStatusCode.OK, image4);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }*/

        /*[HttpPost]
        public HttpResponseMessage UploadImage(string tname)
        {
            try
            {   
                caretaker ct = new caretaker();

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
                ct.pic = path;
                ct.teachername = tname;
                
                //db.caretakers.Add(ct);
                db.caretakers.Add(ct);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, ct.id);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }*/
    }
}


