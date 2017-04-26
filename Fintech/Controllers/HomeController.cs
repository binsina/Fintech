using Fintech.HelperClass;
using Fintech.Models;
using Fintech.Models.ModelClass;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Fintech.Controllers
{
    public class HomeController : Controller

    {
        ApplicationDbContext db = new ApplicationDbContext();

        //get
        [Authorize]
        public ActionResult CreateJoinHouseHold(HouseHoldViewModel model)
        {
            HouseHoldViewModel vm = new HouseHoldViewModel();
            return View(vm);
        }


        //Get
        [Authorize]
        public ActionResult JoinHouseHold(HouseHoldViewModel model)
        {
            HouseHold hh = db.HouseHolds.Find(model.HHId);
            model.Member = db.Users.Find(User.Identity.GetUserId());
            hh.Users.Add(model.Member);
            db.SaveChanges();

            return RedirectToAction("Index","HouseHolds");

        }

        //Post
        [HttpPost]
        public ActionResult CreateHouseHold(HouseHoldViewModel model)
        {
            HouseHold hh = new HouseHold();
          

            hh.Name = model.HHName;
          
            model.Member = db.Users.Find(User.Identity.GetUserId());
              db.HouseHolds.Add(hh);
            db.SaveChanges();

            hh.Users.Add(model.Member);
            db.SaveChanges();

            return RedirectToAction("Index", "HouseHolds");
              

        }

        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public FileContentResult UserPhotos()
        {
            if (User.Identity.IsAuthenticated)
            {
                //let pass User.Identity into userId
                String userId = User.Identity.GetUserId();

                if (userId == null)
                {
                    //if there is no photo chosen then use Stock photo- I am using CoderFoundry image
                    string fileName = HttpContext.Server.MapPath(@"~/admin/images/adminPicture.png");
                    //convert import image into byte file that can read by using FileStream and BinaryReader Method
                    byte[] imageData = null;
                    FileInfo fileInfo = new FileInfo(fileName);
                    long imageFileLength = fileInfo.Length;
                    FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    imageData = br.ReadBytes((int)imageFileLength);

                    return File(imageData, "image/png");

                }
                // to get the user details to load user Image 
                var bdUsers = HttpContext.GetOwinContext().Get<ApplicationDbContext>();
                var UserImage = bdUsers.Users.Where(photo => photo.Id == userId).FirstOrDefault();

                return new FileContentResult(UserImage.UserPhoto, "image/jpeg");
            }
            else
            {
                string fileName = HttpContext.Server.MapPath(@"~/admin/images/adminPicture.png");

                byte[] imageData = null;
                FileInfo fileInfo = new FileInfo(fileName);
                long imageFileLength = fileInfo.Length;
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                imageData = br.ReadBytes((int)imageFileLength);
                return File(imageData, "image/png");

            }
        }











        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}