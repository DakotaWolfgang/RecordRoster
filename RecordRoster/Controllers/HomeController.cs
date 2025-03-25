using System.EnterpriseServices;
using System.Web.Mvc;
using System.Web;
using RecordRoster.Models;
using RecordRoster.DataAccessLayer;
using System.Linq;
using System;

namespace RecordRoster.Controllers
{
    public class HomeController : Controller
    {
        private readonly RecordRosterDb _context = new RecordRosterDb();

        public ActionResult Index()
        {
            var albums = _context.Albums.ToList();
            ViewBag.RandomAlbum = albums.OrderBy(a => Guid.NewGuid()).FirstOrDefault();

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}