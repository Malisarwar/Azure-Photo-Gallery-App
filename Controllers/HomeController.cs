using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using PhotoGallery.Data;
using PhotoGallery.Models;

namespace PhotoGallery.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly PhotoGallery_dbEntities _db = new PhotoGallery_dbEntities();

        public ActionResult Index()
        {
            SetSessionValues();

            var userId = User.Identity.GetUserId();

            var barChartDataFree = new List<DataPoint>();

            var dateSelected = DateTime.Now.AddDays(-6).Date;
            var temp = _db.Photos.Where(a => a.UserId == userId && DbFunctions.TruncateTime(a.DateAdded) == dateSelected);
            var userUsage = (double)(temp.Any() ? temp.Sum(a => a.PhotoSize) : 0m);
            barChartDataFree.Add(new DataPoint(1, 10 - userUsage, dateSelected.ToString("dddd")));

            dateSelected = DateTime.Now.AddDays(-5).Date;
            temp = _db.Photos.Where(a => a.UserId == userId && DbFunctions.TruncateTime(a.DateAdded) == dateSelected);
            userUsage = (double)(temp.Any() ? temp.Sum(a => a.PhotoSize) : 0m);
            barChartDataFree.Add(new DataPoint(2, 10 - userUsage, dateSelected.ToString("dddd")));

            dateSelected = DateTime.Now.AddDays(-4).Date;
            temp = _db.Photos.Where(a => a.UserId == userId && DbFunctions.TruncateTime(a.DateAdded) == dateSelected);
            userUsage = (double)(temp.Any() ? temp.Sum(a => a.PhotoSize) : 0m);
            barChartDataFree.Add(new DataPoint(3, 10 - userUsage, dateSelected.ToString("dddd")));

            dateSelected = DateTime.Now.AddDays(-3).Date;
            temp = _db.Photos.Where(a => a.UserId == userId && DbFunctions.TruncateTime(a.DateAdded) == dateSelected);
            userUsage = (double)(temp.Any() ? temp.Sum(a => a.PhotoSize) : 0m);
            barChartDataFree.Add(new DataPoint(4, 10 - userUsage, dateSelected.ToString("dddd")));

            dateSelected = DateTime.Now.AddDays(-2).Date;
            temp = _db.Photos.Where(a => a.UserId == userId && DbFunctions.TruncateTime(a.DateAdded) == dateSelected);
            userUsage = (double)(temp.Any() ? temp.Sum(a => a.PhotoSize) : 0m);
            barChartDataFree.Add(new DataPoint(5, 10 - userUsage, dateSelected.ToString("dddd")));

            dateSelected = DateTime.Now.AddDays(-1).Date;
            temp = _db.Photos.Where(a => a.UserId == userId && DbFunctions.TruncateTime(a.DateAdded) == dateSelected);
            userUsage = (double)(temp.Any() ? temp.Sum(a => a.PhotoSize) : 0m);
            barChartDataFree.Add(new DataPoint(6, 10 - userUsage, "Yesterday"));

            dateSelected = DateTime.Now.Date;
            temp = _db.Photos.Where(a => a.UserId == userId && DbFunctions.TruncateTime(a.DateAdded) == dateSelected);
            userUsage = (double)(temp.Any() ? temp.Sum(a => a.PhotoSize) : 0m);
            barChartDataFree.Add(new DataPoint(7, 10 - userUsage, "Today"));
            ViewBag.BarDataPointsFree = JsonConvert.SerializeObject(barChartDataFree);

            var barChartDataUsed = new List<DataPoint>();
            dateSelected = DateTime.Now.AddDays(-6).Date;
            temp = _db.Photos.Where(a => a.UserId == userId && DbFunctions.TruncateTime(a.DateAdded) == dateSelected);
            userUsage = (double)(temp.Any() ? temp.Sum(a => a.PhotoSize) : 0m);
            barChartDataUsed.Add(new DataPoint(1, userUsage, dateSelected.ToString("dddd")));

            dateSelected = DateTime.Now.AddDays(-5).Date;
            temp = _db.Photos.Where(a => a.UserId == userId && DbFunctions.TruncateTime(a.DateAdded) == dateSelected);
            userUsage = (double)(temp.Any() ? temp.Sum(a => a.PhotoSize) : 0m);
            barChartDataUsed.Add(new DataPoint(2, userUsage, dateSelected.ToString("dddd")));

            dateSelected = DateTime.Now.AddDays(-4).Date;
            temp = _db.Photos.Where(a => a.UserId == userId && DbFunctions.TruncateTime(a.DateAdded) == dateSelected);
            userUsage = (double)(temp.Any() ? temp.Sum(a => a.PhotoSize) : 0m);
            barChartDataUsed.Add(new DataPoint(3, userUsage, dateSelected.ToString("dddd")));

            dateSelected = DateTime.Now.AddDays(-3).Date;
            temp = _db.Photos.Where(a => a.UserId == userId && DbFunctions.TruncateTime(a.DateAdded) == dateSelected);
            userUsage = (double)(temp.Any() ? temp.Sum(a => a.PhotoSize) : 0m);
            barChartDataUsed.Add(new DataPoint(4, userUsage, dateSelected.ToString("dddd")));

            dateSelected = DateTime.Now.AddDays(-2).Date;
            temp = _db.Photos.Where(a => a.UserId == userId && DbFunctions.TruncateTime(a.DateAdded) == dateSelected);
            userUsage = (double)(temp.Any() ? temp.Sum(a => a.PhotoSize) : 0m);
            barChartDataUsed.Add(new DataPoint(5, userUsage, dateSelected.ToString("dddd")));

            dateSelected = DateTime.Now.AddDays(-1).Date;
            temp = _db.Photos.Where(a => a.UserId == userId && DbFunctions.TruncateTime(a.DateAdded) == dateSelected);
            userUsage = (double)(temp.Any() ? temp.Sum(a => a.PhotoSize) : 0m);
            barChartDataUsed.Add(new DataPoint(6, userUsage, "Yesterday"));

            dateSelected = DateTime.Now.Date;
            temp = _db.Photos.Where(a => a.UserId == userId && DbFunctions.TruncateTime(a.DateAdded) == dateSelected);
            userUsage = (double)(temp.Any() ? temp.Sum(a => a.PhotoSize) : 0m);
            barChartDataUsed.Add(new DataPoint(7, userUsage, "Today"));
            ViewBag.BarDataPointsUsed = JsonConvert.SerializeObject(barChartDataUsed);

            temp = _db.Photos.Where(a => a.UserId == userId);
            userUsage = (double)(temp.Any() ? temp.Sum(a => a.PhotoSize) : 0m);
            var pieChartData = new List<DataPoint>();
            pieChartData.Add(new DataPoint(Math.Round(userUsage * 10, 2), "Used", "Used"));
            pieChartData.Add(new DataPoint(Math.Round((10 - userUsage) * 10, 2), "Free", "Free"));
            ViewBag.PieDataPoints = JsonConvert.SerializeObject(pieChartData);

            return View();
        }

        public ActionResult Storage(string msg = null)
        {
            SetSessionValues();

            var userId = User.Identity.GetUserId();
            ViewBag.Msg = msg;
            return View(_db.Photos.Where(a => a.UserId == userId).ToList());
        }

        public ActionResult Logs()
        {
            SetSessionValues();

            var userId = User.Identity.GetUserId();
            return View(_db.Logs.Where(a => a.UserId == userId).ToList());
        }

        public ActionResult UploadPhoto(HttpPostedFileBase file)
        {
            var userId = User.Identity.GetUserId();
            var msg = string.Empty;

            try
            {
                // get uploaded file size in mb
                decimal byteCount = file.ContentLength;
                decimal kb = byteCount / 1024m;
                decimal mb = kb / 1024m;

                var temp = _db.Photos.Where(a => a.UserId == userId);
                decimal usedStorage = temp.Any() ? temp.Sum(b => b.PhotoSize) : 0m;

                if (usedStorage + mb > 10m)
                    throw new Exception("Your storage is full. Please empty some space before adding new Photos.");

                // save file on the server
                var fileName = Path.GetFileName(file.FileName);
                string path = Path.Combine(Server.MapPath("~/UsersFiles"), fileName);
                file.SaveAs(path);

                // add file entry in db
                _db.Photos.Add(new Photo
                {
                    DateAdded = DateTime.Now,
                    PhotoPath = Path.Combine("\\UsersFiles", fileName),
                    PhotoSize = mb,
                    UserId = userId
                });

                // success log
                _db.Logs.Add(new Log
                {
                    ActionPerformed = "Upload Photo",
                    ActionStatus = "Success",
                    DateAdded = DateTime.Now,
                    IpAddress = GetIPAddress(),
                    UserId = userId
                });

                _db.SaveChanges();

                msg = "Success: Photo uploaded successfully.";
            }
            catch (Exception ex)
            {
                // failure log
                _db.Logs.Add(new Log
                {
                    ActionPerformed = "Upload Photo",
                    ActionStatus = "Failed",
                    DateAdded = DateTime.Now,
                    IpAddress = GetIPAddress(),
                    UserId = userId,
                    Message = ex.Message
                });

                _db.SaveChanges();

                msg = "Error: " + ex.Message;
            }
            return RedirectToAction("Storage", new { msg });
        }

        public ActionResult DeletePhoto(string selectedPhotos)
        {
            var userId = User.Identity.GetUserId();
            var msg = string.Empty;
            bool singleProcessed = false;

            try
            {
                foreach (var photoIdString in selectedPhotos.Split(','))
                {
                    if (string.IsNullOrEmpty(photoIdString))
                        continue;

                    int.TryParse(photoIdString, out var photoId);

                    var photo = _db.Photos.Find(photoId);
                    if (photo == null)
                        throw new Exception("Photo not found.");

                    // delete photo from server dir
                    string path = Server.MapPath("~" + photo.PhotoPath);
                    System.IO.File.Delete(path);

                    // remove photo from db
                    _db.Photos.Remove(photo);

                    singleProcessed = true;
                }

                // success log
                _db.Logs.Add(new Log
                {
                    ActionPerformed = "Delete Photo",
                    ActionStatus = "Success",
                    DateAdded = DateTime.Now,
                    IpAddress = GetIPAddress(),
                    UserId = userId
                });

                _db.SaveChanges();

                msg = "Success: Photo delete successfully.";
            }
            catch (Exception ex)
            {
                // failure log
                _db.Logs.Add(new Log
                {
                    ActionPerformed = "Delete Photo",
                    ActionStatus = "Failed",
                    DateAdded = DateTime.Now,
                    IpAddress = GetIPAddress(),
                    UserId = userId,
                    Message = ex.Message
                });

                _db.SaveChanges();

                msg = "Error: " + ex.Message;
            }

            if (!singleProcessed)
                msg = "Error: No photo selected.";

            return RedirectToAction("Storage", new { msg });
        }

        private string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        private void SetSessionValues()
        {
            var userId = User.Identity.GetUserId();

            var temp = _db.Photos.Where(a => a.UserId == userId);

            var userUsage = temp.Any() ? temp.Sum(a => a.PhotoSize) : 0m;

            if (userUsage >= 8)
                Session.Add("StorageWarning", true);
            else
                Session.Remove("StorageWarning");
        }
    }
}