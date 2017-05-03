using Fintech.HelperClass;

using Fintech.Models;
using Fintech.Models.CodeFirst;
using Fintech.Models.ModelClass;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Fintech.Controllers
{
    public class HomeController : Controller

    {
        ApplicationDbContext db = new ApplicationDbContext();

        //get
      
        [Authorize]
        public ActionResult CreateJoinHouseHold(HouseHoldViewModel model, Guid? code)
        {
           
           if (User.Identity.IsInHouseHoldId())
            {
                return RedirectToAction("Index", "Home");
            }
            HouseHoldViewModel vm = new HouseHoldViewModel();


            if (code != null && ValideInvite())
                {
                Invite result = db.Invites.FirstOrDefault(i => i.Token == code);
                vm.isJoinHouseHold = true;
                vm.HHId = result.HouseHoldId;
                vm.HHName = result.HouseHold.Name;

                result.hasbeenUsed = true;

                ApplicationUser user = db.Users.Find(User.Identity.GetUserId());
                user.InviteEmail = result.Email;
                db.SaveChanges();
            }


            return View(vm);
        }

        


        private bool ValideInvite()
        {
            return true;
        }

        //Get
        [Authorize]
        public async Task<ActionResult> JoinHouseHold(HouseHoldViewModel model, Guid? code)
        {
            HouseHold hh = db.HouseHolds.Find(model.HHId);
            var user = db.Users.Find(User.Identity.GetUserId());

            //model.Member = db.Users.Find(User.Identity.GetUserId());

            hh.Users.Add(user);

            db.SaveChanges();


            await ControllerContext.HttpContext.RefreshAuthentication(user);
            return RedirectToAction("Index","HouseHolds");

        }

        //Post
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CreateHouseHold(HouseHoldViewModel model)
        {
            HouseHold hh = new HouseHold();
          
            hh.Name = model.HHName;
            db.HouseHolds.Add(hh);
            db.SaveChanges();

            var user = db.Users.Find(User.Identity.GetUserId());
            hh.Users.Add(user);
            db.SaveChanges();

            await ControllerContext.HttpContext.RefreshAuthentication(user);

            return RedirectToAction("Index", "HouseHolds");
              

        }

        [AuthorizeHouseHold]
        public ActionResult Index()
        {
            var id = User.Identity.GetHouseHoldId();
            HouseHold HH = db.HouseHolds.Find(id);
            if (HH == null)
            {
                return HttpNotFound();
            }
            return View(HH);


           
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