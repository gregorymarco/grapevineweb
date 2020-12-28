using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using website.Models;
using System;
using System.Linq;
using System.Globalization;

namespace website.Controllers
{
    public class HomeController : Controller
    {
        private const string NAME_LONG = "lng";
        private const string NAME_LAT = "lat";

        NumberFormatInfo formatProvider;
        private readonly ILogger<HomeController> _logger;
        
        private websitedbContext db;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            formatProvider = new NumberFormatInfo();
            formatProvider.NumberDecimalSeparator = ".";
            db = new websitedbContext();
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Submit()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public IActionResult submitMessage(MessageSubmissionModel m)
        {
            byte[] latbytes;
            HttpContext.Session.TryGetValue(NAME_LAT, out latbytes);
            byte[] longbytes;
            HttpContext.Session.TryGetValue(NAME_LONG, out longbytes);
            if(latbytes == null || longbytes == null){
                //TODO: make them give location to work
                return View("Index");
            }
            double lat = BitConverter.ToDouble(latbytes);
            double lng = BitConverter.ToDouble(longbytes);
            ViewBag.lat = lat;
            ViewBag.lng = lng;
            // Creating a new item and saving it to the database
            Messages dbm = new Messages();
            dbm.Messagecontent = m.messageContent;
            dbm.Lat = lat;
            dbm.Long = lng;
            dbm.Heure = DateTime.Now;
            db.Messages.Add(dbm);
            if(db.SaveChanges() != 0){
                //TODO: more error handling
            }
            return View("Index");
        }

        [HttpPost]
        public JsonResult updateSessionCoords(string lat,string lng){
            HttpContext.Session.Set(NAME_LAT, BitConverter.GetBytes(Convert.ToDouble(lat, formatProvider)));
            HttpContext.Session.Set(NAME_LONG, BitConverter.GetBytes(Convert.ToDouble(lng, formatProvider)));
            //TODO Error handling
            return Json(200);
        }
        public IActionResult browse(){
            byte[] latbytes;
            HttpContext.Session.TryGetValue(NAME_LAT, out latbytes);
            byte[] longbytes;
            HttpContext.Session.TryGetValue(NAME_LONG, out longbytes);
            if(latbytes == null || longbytes == null){
                //TODO: make them give location to work
                return View("Index");
            }
            double lat = BitConverter.ToDouble(latbytes);
            double lng = BitConverter.ToDouble(longbytes);
            var nearbyMessages = from    msg in db.Messages 
                        where   msg.Lat <= (lat + 0.001) && 
                                msg.Lat >= (lat - 0.001) &&
                                msg.Long <= (lng + 0.001) &&
                                msg.Long >= (lng -0.0001)
                                select msg;
            ViewBag.lat = lat;
            ViewBag.lng = lng;
            return View(nearbyMessages);
        }
    }
}
