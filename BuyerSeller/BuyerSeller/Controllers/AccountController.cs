using BuyerSeller.Helpers;
using BuyerSeller.Models;
using sahalBuyerSeller.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BuyerSeller.Controllers
{
    public class AccountController : BaseController
    {
        private BuyerSellerModel db = new BuyerSellerModel();
        public ActionResult Index()
        {
            if (TempData["message"] != null)
            {
                TempData["msg"] = TempData["message"];
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(string email, string password)
        {
            var response = db.Users.Where(x => x.EmailID == email ||x.MobileNo==email && x.Password == password).FirstOrDefault();
            if (response == null)
            {
                TempData["message"] = "username or password is invalid.";
                return RedirectToAction("Index");
            }
            Session["Role"] = response.RoleID;
            Session["User"] = response.ID;
            Session["UserName"] = response.Name;
            Session["UserId"] = response.ID;
            if (response.RoleID == 2)
                return RedirectToAction("Index", "Home");

            return RedirectToAction("Index", "DashBoard");
        }

        public ActionResult Logout()
        {
            Session["Role"] = null;
            return RedirectToAction("Index", "Account");
        }
        public ActionResult LogOff()
        {
            Session["User"] = null; //it's my session variable
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut(); //you write this when you use FormsAuthentication
            return RedirectToAction("Index", "Account");
        }
        public ActionResult Register()
        {
            if (TempData["message"] != null)
            {
                TempData["msg"] = "Registered Successfully";
            }

            return View();
        }
        [HttpPost]
        public ActionResult BuyerRegistration(FormCollection form, HttpPostedFileBase Files)
        {
            string MobileNo1 = form["Phone"];
            if (Files!=null)
            {
                var ImageUpload1 = new byte[Files.ContentLength];
                Files.InputStream.Read(ImageUpload1, 0, Files.ContentLength);

                var user = new User
                {
                    Name = form["Name"],
                    MobileNo = form["Phone"],
                    EmailID = form["email"],
                    Password = form["Password"],
                    RoleID = 2,
                    CreatedOn = DateTime.Now,
                    ImageUpload = ImageUpload1,
                    OtpPassword = GetOTPPassword(),
                    OtpGeneratedTime = DateTime.Now
                    
            };
                db.Users.Add(user);
                int i = db.SaveChanges();
                string response = new smsIntegration().SendSMS("SBSNEC", "SBS6352", MobileNo1, "Otp for Buyer Registration. Otp is" + user.OtpPassword, "N");

                if (i > 0)
                {
                    TempData["message"] = "Registred Successfully";

                }
                else
                {
                    TempData["message"] = "Registration  Failed";

                }
            }
            else
            {
                

                var user = new User
                {
                    Name = form["Name"],
                    MobileNo = form["Phone"],
                    EmailID = form["email"],
                    Password = form["Password"],
                    RoleID = 2,
                    CreatedOn = DateTime.Now,
                   // ImageUpload = ImageUpload1,
                    OtpPassword = GetOTPPassword(),
                    OtpGeneratedTime = DateTime.Now
                };
                db.Users.Add(user);
                int i = db.SaveChanges();
                string response = new smsIntegration().SendSMS("SBSNEC", "SBS6352", MobileNo1, "Otp for Buyer Registration. Otp is" + user.OtpPassword, "N");

                if (i > 0)
                {
                    TempData["message"] = "Registred Successfully";

                }
                else
                {
                    TempData["message"] = "Registration  Failed";

                }
            }




            ViewBag.Phone = MobileNo1;

            return RedirectToAction("OtpAuthentication",new { MobileNo = MobileNo1 });
        }


        [NonAction]
        public string GetOTPPassword()
        {
            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string small_alphabets = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "1234567890";

            string characters = numbers;
            characters += alphabets + small_alphabets + numbers;

            int length = 5;
            string otp = string.Empty;
            for (int i = 0; i < length; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }
            return otp;
        }

        [HttpGet]
        public ActionResult OtpAuthentication(string MobileNo)
        {
            ViewBag.Mobile = MobileNo;
            return View();
        }

        [HttpPost]
        public ActionResult OtpAuthentication(string Mobile, string OtpPassword)
        {
            var user = db.Users.Where(x=>x.MobileNo==Mobile&&x.OtpPassword==OtpPassword).FirstOrDefault();

            if (user != null)
            {
                DateTime OTPTime = user.OtpGeneratedTime.Value;
                DateTime expTime = OTPTime.AddMinutes(15);
                if (DateTime.Now <= expTime)
                {
                    user.IsOtpCheck = true;
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.msg = " Otp Expired";
                    return RedirectToAction("Index", "Account");
                }
            }

            ViewBag.msg = "Incorrect Otp";

            return View();
        }

        [HttpPost]
        public ActionResult Register(FormCollection form, HttpPostedFileBase Files)
        {

            if (Files != null)
            {
                var ImageUpload1 = new byte[Files.ContentLength];
                Files.InputStream.Read(ImageUpload1, 0, Files.ContentLength);
                var user = new User
                {
                    RoleID = 2,
                    Name = form["Name"],
                    MobileNo = form["Phone"],
                    EmailID = form["email"],
                    Password = form["Password"],
                    Latitude = form["Latitude"],
                    Longitude = form["Longitude"],
                    Address = form["Address"],
                    ImageUpload = ImageUpload1,
                    IsActive = true,
                    BrandId = Convert.ToInt64(form["BrandId"])
                };

                db.Users.Add(user);
                db.SaveChanges();
                TempData["message"] = "Registred Successfully";
            }
            else
            {
                var user = new User
                {
                    RoleID = 2,
                    Name = form["Name"],
                    MobileNo = form["Phone"],
                    EmailID = form["email"],
                    Password = form["Password"],
                    Latitude = form["Latitude"],
                    Longitude = form["Longitude"],
                    Address = form["Address"],
                    //ImageUpload = ImageUpload1,
                    IsActive = true,
                    BrandId = Convert.ToInt64(form["BrandId"])
                };

                db.Users.Add(user);
                db.SaveChanges();
                TempData["message"] = "Registred Successfully";
            }


            return View();
        }


        public ActionResult Sellers()
        {
            var res = db.Users.Where(x=>x.RoleID==3).ToList().OrderByDescending(x => x.ID);
            return View(res);
        }

        public ActionResult Employees()
        {
            if (TempData["message"]!=null)
            {
                TempData["msg"] = TempData["message"];
            }
            var res = db.Users.ToList().OrderByDescending(x => x.ID);
            return View(res);
        }
        public ActionResult RegisterSeller()
        {
            if (TempData["message"] != null)
            {
                TempData["msg"] = "Registered Successfully";
            }
            ViewBag.BrandList = db.Brands.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult RegisterSeller(FormCollection form, HttpPostedFileBase Files)
        {
            
            if (Files!=null)
            {
                var ImageUpload1 = new byte[Files.ContentLength];
                Files.InputStream.Read(ImageUpload1, 0, Files.ContentLength);
                var user = new User
                {
                    RoleID = 3,
                    Name = form["Name"],
                    MobileNo = form["Phone"],
                    EmailID = form["email"],
                    Password = form["Password"],
                    Latitude = form["Latitude"],
                    Longitude = form["Longitude"],
                    Address = form["Address"],
                    ImageUpload = ImageUpload1,
                    IsActive = true,
                    BrandId = Convert.ToInt64(form["BrandId"])
                };

                db.Users.Add(user);
                db.SaveChanges();
                TempData["message"] = "Registred Successfully";
            }
            else
            {
                var user = new User
                {
                    RoleID = 3,
                    Name = form["Name"],
                    MobileNo = form["Phone"],
                    EmailID = form["email"],
                    Password = form["Password"],
                    Latitude = form["Latitude"],
                    Longitude = form["Longitude"],
                    Address = form["Address"],
                    //ImageUpload = ImageUpload1,
                    IsActive = true,
                    BrandId = Convert.ToInt64(form["BrandId"])
                };

                db.Users.Add(user);
                db.SaveChanges();
                TempData["message"] = "Registred Successfully";
            }
          
           

            return RedirectToAction("RegisterSeller");
        }

        public ActionResult RegisterEmployee()
        {
            if (TempData["message"] != null)
            {
                TempData["msg"] = "Registered Successfully";
            }
 
            return View();
        }

        [HttpPost]
        public ActionResult RegisterEmployee(FormCollection form, HttpPostedFileBase Files)
        {

            if (Files != null)
            {
                var ImageUpload1 = new byte[Files.ContentLength];
                Files.InputStream.Read(ImageUpload1, 0, Files.ContentLength);
                var user = new User
                {
                    RoleID = 5,
                    Name = form["Name"],
                    MobileNo = form["Phone"],
                    EmailID = form["email"],
                    Password = form["Password"],
                    Latitude = form["Latitude"],
                    Longitude = form["Longitude"],
                    Address = form["Address"],
                    ImageUpload = ImageUpload1,
                    IsActive = true,
                  
                };

                db.Users.Add(user);
                db.SaveChanges();
                TempData["message"] = "Registred Successfully";
            }
            else
            {
                var user = new User
                {
                    RoleID = 5,
                    Name = form["Name"],
                    MobileNo = form["Phone"],
                    EmailID = form["email"],
                    Password = form["Password"],
                    Latitude = form["Latitude"],
                    Longitude = form["Longitude"],
                    Address = form["Address"],
                    //ImageUpload = ImageUpload1,
                    IsActive = true,
                    BrandId = Convert.ToInt64(form["BrandId"])
                };

                db.Users.Add(user);
                db.SaveChanges();
                TempData["message"] = "Registred Successfully";
            }



            return RedirectToAction("RegisterSeller");
        }


        [HttpGet]
        public ActionResult ManageRole(Int64 userid)
        {
            var res= db.Users.Find(userid);
            ViewBag.RoleList = db.Roles.ToList();
            return View(res);

        }

       public ActionResult ManageRole(User model)
        {
            var res = db.Users.Find(model.ID);

            res.RoleID = model.RoleID;
            if (res.RoleID == model.RoleID)
            {
                TempData["message"] = "Role Updated Successfully";
            }
           db.Entry(res).State= EntityState.Modified;
            db.SaveChanges();
           
            return RedirectToAction("Employees");
        }
    }
}