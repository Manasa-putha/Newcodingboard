    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper;
    using BookEventread.Dtos;
    using BookEventread.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    
   namespace BookEventread.Controllers
    {
        public class HomeController : Controller
        {
            ApplicationDbContext db = new ApplicationDbContext();

            private readonly IMapper _mapper;
            public HomeController(IMapper mapper)
            {
                _mapper = mapper;
            }
            public HomeController()
            {

            }

            public ActionResult Index()
            {
                var fetch = db.Events.ToList();
                return View(fetch);
            }

            public ActionResult About()
            {
                ViewBag.Message = "Your application description page.";

                return View();
            }

            public ActionResult Contact()
            {
                ViewBag.Message = "Your contact page.";

                return View();
            }
            [Authorize]
            public ActionResult CreateEvent()
            {
                EventModel eventModel = new EventModel();
                return View(eventModel);
            }
            [HttpPost]
            public ActionResult CreateEvent(EventModel eventModel)
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                else
                {
                    eventModel.UserId = User.Identity.GetUserId();
                    db.Events.Add(eventModel);
                    db.SaveChanges();
                    if (eventModel.Invites != null)
                    {
                        char[] separators = new char[] { ' ', ',' };
                        string[] subs = eventModel.Invites.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var sub in subs)
                        {
                            Email email = new Email();
                            email.emailId = sub;
                            email.eventId = eventModel.EventId;
                            db.Invites.Add(email);
                            db.SaveChanges();
                        }
                    }

                    ViewBag.Message = string.Format("Event Created Sucessfully");
                    return RedirectToAction("ViewMyEvents");
                }
            }
            public ActionResult AllEvents()
            {
                var fetch = db.Events.ToList();

                return View(fetch);
            }


            [Authorize]
            public ActionResult InvitedEvents()
            {
                string id = User.Identity.GetUserId();
                var invites = db.Invites.Where(x => x.emailId == id).ToList();
                var eventlist = db.Events.ToList();
                List<EventModel> fetch = new List<EventModel>();
                foreach (var e in invites)
                {
                    foreach (var ev in eventlist)
                    {
                        if (e.eventId == ev.EventId)
                            fetch.Add(ev);
                    }
                }
                return View(fetch);
            }
            [Authorize]
            public ActionResult ViewMyEvents()
            {
                string id = User.Identity.GetUserId();
                var fetch = db.Events.Where(x => x.UserId == id).ToList();
                return View(fetch);
            }

            public ActionResult EventView(int id)
            {
                EventViewModel eventViewModel = new EventViewModel();

                eventViewModel.eventModel = db.Events.Where(x => x.EventId == id).FirstOrDefault();
                eventViewModel.Comments = db.Comments.Where(x => x.EventId == id).OrderBy(x => x.Created).ToList();
                return View(eventViewModel);
            }
            [Authorize]
            public ActionResult Edit(int id)
            {
                EventModel e = db.Events.Where(x => x.EventId == id).First();
                return View(e);
            }
            [HttpPost]
            public ActionResult Edit(EventModel e)
            {
                if (ModelState.IsValid)
                {
                    int id = e.EventId;
                    EventModel t = db.Events.Where(x => x.EventId == id).First();
                    t.Title = e.Title;
                    t.Date = e.Date;
                    t.Location = e.Location;
                    t.StartTime = e.StartTime;
                    t.Type = e.Type;
                    t.Description = e.Description;
                    t.OtherDetails = e.OtherDetails;
                    db.SaveChanges();
                    ViewBag.Message = string.Format("Event upated Sucessfully");
                    if (User.Identity.GetUserName() == "myadmin@bookevents.com")
                    {
                        return RedirectToAction("AllEvents");
                    }
                    else
                        return RedirectToAction("ViewMyEvents");
                }
                return View(e);
            }

            public ActionResult Delete(int id)
            {
                EventModel e = db.Events.Where(x => x.EventId == id).FirstOrDefault();
                db.Events.Remove(e);
                db.SaveChanges();
                ViewBag.Message = string.Format("Event Deleted Sucessfully");
                if (User.Identity.GetUserName() == "myadmin@bookevents.com")
                {
                    return RedirectToAction("AllEvents");
                }
                else
                    return RedirectToAction("ViewMyEvents");
            }
            public JsonResult LeaveComment(CommentViewModel model)
            {
                JsonResult result = new JsonResult();


                var comment = new CommentModel
                {
                    Message = model.Text,
                    EventId = model.EventId,
                    Created = DateTime.Now
                };
                db.Comments.Add(comment);
                db.SaveChanges();
                result.Data = new { Success = true };


                return result;

            }
        }
    }