using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IMDB.Models;
using System.Net;
using Newtonsoft.Json;

namespace IMDB.Controllers
{
    public class HomeController : Controller
    {
        DBContext DB = new DBContext();
        public ActionResult Index()
        {
            IEnumerable<Movie> Movies = DB.Movies.AsEnumerable();
            return View(Movies.ToList());
        }
        [HttpGet]
        public ActionResult NewMovie()
        {
            ViewData["Producers"] = (IEnumerable<SelectListItem>)DB.Producers.Select(p => new SelectListItem { Value = p.ID.ToString(), Text = p.Name });
            ViewData["Actors"] = (IEnumerable<SelectListItem>)DB.Actors.Select(p => new SelectListItem { Value = p.ID.ToString(), Text = p.Name });
            return View();
        }
        [HttpPost]
        public HttpStatusCodeResult NewMovie(MovieVM movieVM)
        {
            Movie Movie = new Movie
            {
                Name = movieVM.Name,
                Plot = movieVM.Plot,
                Poster = movieVM.Poster,
                ProducerID = movieVM.ProducerID
            };

            DB.Movies.Add(Movie);
            DB.SaveChanges();

            foreach (var Actor in movieVM.ActorList)
            {
                ActorMovie AM = new ActorMovie
                {
                    ActorID = Actor,
                    MovieID = Movie.ID
                };
                DB.ActorsMovies.Add(AM);
            }

            DB.SaveChanges();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
        public HttpStatusCodeResult UploadFile()
        {
            if (Request.Files.Count > 0)
            {
                var img = Request.Files[0];
                img.SaveAs(Server.MapPath("~/MovieImages/") + img.FileName);
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        [HttpGet]
        public ActionResult EditMovie(int id)
        {


            Movie Movie = DB.Movies.Where(x => x.ID == id).FirstOrDefault();
            MovieVM mvm = new MovieVM
            {
                Name = Movie.Name,
                Plot = Movie.Plot,
                Poster = Movie.Poster,
                ProducerID = Movie.ProducerID,
                ActorList = Movie.ActorsMovies.Where(x => x.MovieID == id).Select(x => x.ActorID)
            };

            var Producers = DB.Producers.Select(p => new SelectListItem { Value = p.ID.ToString(), Text = p.Name, Selected = (p.ID == Movie.ID) ? true : false });
            ViewData["Producers"] = (IEnumerable<SelectListItem>)Producers;
            var Actors = DB.Actors.Select(p => new SelectListItem { Value = p.ID.ToString(), Text = p.Name, Selected = (mvm.ActorList.Contains(p.ID)) ? true : false });
            ViewData["Actors"] = (IEnumerable<SelectListItem>)Actors;
            return View(mvm);
        }
        [HttpPost]
        public HttpStatusCodeResult EditMovie(MovieVM movieVM)
        {
            if (movieVM != null)
            {

                Movie Movie = DB.Movies.Where(m => m.ID == movieVM.ID).FirstOrDefault();

                Movie.Name = movieVM.Name;
                Movie.Plot = movieVM.Plot;
                Movie.Poster = movieVM.Poster ?? Movie.Poster;
                Movie.ProducerID = movieVM.ProducerID;
                IEnumerable<ActorMovie> ActMov = DB.ActorsMovies.Where(a => a.MovieID == movieVM.ID);
                DB.ActorsMovies.RemoveRange(ActMov);
                foreach (var Actor in movieVM.ActorList)
                {
                    ActorMovie AM = new ActorMovie
                    {
                        ActorID = Actor,
                        MovieID = Movie.ID
                    };
                    DB.ActorsMovies.Add(AM);
                }
                DB.SaveChanges();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        [HttpGet]
        public ActionResult GetActor()
        {
            var Actor = new Actor();
            return PartialView("_Actors", Actor);
        }

        [HttpGet]
        public ActionResult GetProducer()
        {
            var Producer = new Producer();
            return PartialView("_Producer", Producer);
        }
        [HttpGet]
        public string GetProducerList()
        {
            var ProdList = DB.Producers.Select(x => new { x.Name, x.ID }).OrderBy(x => x.ID);
            return JsonConvert.SerializeObject(ProdList);
        }

        [HttpGet]
        public string GetActorList()
        {
            var ActList = DB.Actors.Select(x => new { x.Name, x.ID }).OrderBy(x => x.ID);
            return JsonConvert.SerializeObject(ActList);
        }
        [HttpPost]
        public HttpStatusCodeResult AddProducer(Producer Producer)
        {
            if (Producer != null)
            {
                try
                {
                    DB.Producers.Add(Producer);
                    DB.SaveChanges();
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception e)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, e.Message);
                }
            }
            else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        public HttpStatusCodeResult AddActor(Actor Actor)
        {
            if (Actor != null)
            {
                try
                {
                    DB.Actors.Add(Actor);
                    DB.SaveChanges();
                    return new HttpStatusCodeResult(HttpStatusCode.OK, "success");
                }
                catch (Exception e)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, e.Message);
                }
            }
            else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No data");
        }
    }
}
