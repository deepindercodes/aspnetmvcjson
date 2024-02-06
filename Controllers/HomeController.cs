using aspnetmvcjson.Models;
using System.Linq;
using System.Web.Mvc;

namespace aspnetmvcjson.Controllers
{
    public class HomeController : Controller
    {
        jsonData objarticles = new jsonData();

        public ActionResult Index()
        {
            return View(objarticles.GetArticles());
        }

        public ActionResult AddNewArticle()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddNewArticle(Article article)
        {
            if (ModelState.IsValid)
            {
                ViewBag.ArticleAdded = "Y";

                objarticles.AddArticle(article);
            }

            return PartialView(article);
        }

        public ActionResult EditArticle(int id)
        {
            return PartialView(objarticles.GetArticles().Where(u => u.id == id.ToString()).FirstOrDefault());
        }

        [HttpPost]
        public ActionResult EditArticle(Article article)
        {
            if (ModelState.IsValid)
            {
                ViewBag.ArticleEdited = "Y";

                objarticles.EditArticle(article);
            }

            return PartialView(article);
        }

        public ActionResult DelArticle(int id)
        {
            objarticles.DeleteArticle(id.ToString());

            return RedirectPermanent("/");
        }

        public ActionResult ViewArticle(int id)
        {
            return View(objarticles.GetArticles().Where(u => u.id == id.ToString()).FirstOrDefault());
        }
    }
}