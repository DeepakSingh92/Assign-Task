using Assign_Task.Models;
using ESCommon;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace Assign_Task.Controllers
{
    public class HomeController : Controller
    {

        string ConnectionString = ConfigurationManager.ConnectionStrings["ConStr"].ToString();
        private string WebApi = clsMain.MyString(System.Configuration.ConfigurationManager.AppSettings["WebApi"]);

        //---------------------------------------------------Login And LogOut-----------------------------------
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection frm)
        {

            string Username = frm["Username"];
            string Password = frm["Password"];
            //string role = frm["role"];

            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(WebApi + "api/Values/GetLogin");

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

                // List data response.
                HttpResponseMessage response = client.GetAsync("?UserId=" + Username + "&Pwd=" + Password).Result;  // Blocking call!

                if (response.IsSuccessStatusCode)
                {
                    var dataObjects = response.Content.ReadAsAsync<IEnumerable<User>>().Result;

                    foreach (var d in dataObjects)
                    {

                        Session["UserName"] = d.Name;
                        Session["UserCode"] = d.Code;
                        Session["UserType"] = d.UserType;
                    }
                    if(clsMain.MyLen(Session["UserName"]) > 0 )
                    {
                        return View("Index");
                    }
                    else
                    {
                        return View("Login");
                    }
                   
                }
                else
                {
                    return View();
                }
            }
            catch (Exception ex)
            {
                return View();
            }

        }

        public ActionResult LogOut()
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();



            return View("Login");
        }
        //---------------------------------------------------Login And LogOut-----------------------------------

        //------------------------------------------------------Main Pages----------------------------------------

        //-----------------------------------------------------------Admin Pages Done--------------------------------------------

        public ActionResult Index()
        {
            if (clsMain.MyLen(Session["UserCode"]) > 0)
            {
                return View();
            }
            else
            {
                return View("Login");
            }
        }

        public ActionResult ProjectList()
        {
            if (clsMain.MyLen(Session["UserCode"]) > 0)
            {
                return View();
            }
            else
            {
                return View("Login");
            }
        }

        public ActionResult AddProject(string VisitPId)
        {
            if (clsMain.MyLen(Session["UserCode"]) > 0)
            {
                ViewBag.VisitorPID = VisitPId;
                return View();
            }
            else
            {
                return View("Login");
            }

           
        }

        public ActionResult AddTaskList()
        {
            if (clsMain.MyLen(Session["UserCode"]) > 0)
            {
                return View();
            }
            else
            {
                return View("Login");
            }
        }
        public ActionResult AddTask(string VisitId, string CallId)
        {
            if (clsMain.MyLen(Session["UserCode"]) > 0)
            {
                ViewBag.VisitorID = VisitId;
                ViewBag.CallId = CallId;
                return View();
            }
            else
            {
                return View("Login");
            }
        }

        public ActionResult UserList()
        {
            if (clsMain.MyLen(Session["UserCode"]) > 0)
            {
                return View();
            }
            else
            {
                return View("Login");
            }
        }
        public ActionResult UserEntry(string VisitUId)
        {
            if (clsMain.MyLen(Session["UserCode"]) > 0)
            {
                ViewBag.VisitorUID = VisitUId;
                return View();
            }
            else
            {
                return View("Login");
            }
        }

        public ActionResult ClientList()
        {
            if (clsMain.MyLen(Session["UserCode"]) > 0)
            {
                return View();
            }
            else
            {
                return View("Login");
            }
        }
        public ActionResult ClientEntry(string VisitCId)
        {
            if (clsMain.MyLen(Session["UserCode"]) > 0)
            {
                ViewBag.VisitorCID = VisitCId;
                return View();
            }
            else
            {
                return View("Login");
            }
        }

        public ActionResult References()
        {
            if (clsMain.MyLen(Session["UserCode"]) > 0)
            {
                return View();
            }
            else
            {
                return View("Login");
            }
        }
        public ActionResult ReferenceEntry(string VisitRId)
        {
            if (clsMain.MyLen(Session["UserCode"]) > 0)
            {
                ViewBag.VisitorRID = VisitRId;
                return View();
            }
            else
            {
                return View("Login");
            }
           
           
        }

        //------------------------------------------------------Main Pages----------------------------------------

        //-----------------------------------------------------Reports--------------------------------------------

        public ActionResult UserTaskStatus()
        {
            if (clsMain.MyLen(Session["UserCode"]) > 0)
            {
                return View();
            }
            else
            {
                return View("Login");
            }
        }
        public ActionResult ProjectTaskStatus()
        {
            if (clsMain.MyLen(Session["UserCode"]) > 0)
            {
                return View();
            }
            else
            {
                return View("Login");
            }
        }
        public ActionResult TodayUserTask()
        {
            if (clsMain.MyLen(Session["UserCode"]) > 0)
            {
                return View();
            }
            else
            {
                return View("Login");
            }
        }
        public ActionResult CheckinList()
        {
            if (clsMain.MyLen(Session["UserCode"]) > 0)
            {
                return View();
            }
            else
            {
                return View("Login");
            }
        }
        public ActionResult CompletedTaskList()
        {
            if (clsMain.MyLen(Session["UserCode"]) > 0)
            {
                return View();
            }
            else
            {
                return View("Login");
            }
        }

        //-----------------------------------------------------Reports--------------------------------------------

        //-----------------------------------------------------------Admin Pages Done--------------------------------------------
        public ActionResult UserTaskEntryList()
        {
            if (clsMain.MyLen(Session["UserCode"]) > 0)
            {
                return View();
            }
            else
            {
                return View("Login");
            }
        }
        public ActionResult EditUserTaskEntryList(string VisitId)
        {
            if (clsMain.MyLen(Session["UserCode"]) > 0)
            {
                ViewBag.VisitorID = VisitId;
                return View();
            }
            else
            {
                return View("Login");
            }
        }

    }

}
