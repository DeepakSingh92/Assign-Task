using Assign_Task.Models;
using ESCommon;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Serialization;

namespace Assign_Task.Controllers
{
    public class ValuesController : ApiController
    {
        string ConnectionString = ConfigurationManager.ConnectionStrings["ConStr"].ToString();

        [HttpGet]
        public IEnumerable<User> GetLogin(string UserId, string Pwd)
        {
            string strQry = "";

            strQry = " Exec sp_login @Email = '" + UserId + "' , @Password = '" + Pwd + "', @OrgId= 1 ";


            SQLHELPER obj = new SQLHELPER(ConnectionString);
            DataTable dt = obj.getTable(strQry);

            List<User> objResult = new List<User>();
            if (dt != null && dt.Rows.Count > 0)
            {

                objResult = dt.AsEnumerable()
                                           .Select(row => new User()
                                           {
                                               Name = clsMain.MyString(row.Field<string>("UserName")),
                                               Code = clsMain.MyString(row.Field<Int32?>("UserCode")),
                                               UserType = clsMain.MyInt(row.Field<Int32?>("UserType")),
                                               Pwd = clsMain.MyString(row.Field<string>("Pwd")),

                                           }).ToList();

            }


            return objResult.ToList();
        }

        [HttpGet]
        public IEnumerable<UserName> GetUserName()
        {
            List<UserName> objResult = new List<UserName>();
            SQLHELPER obj = new SQLHELPER(ConnectionString);

            string str = " select Id,UserName from UserMaster where Id !=1 ";

            try
            {
                DataTable dt = obj.getTable(str);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        UserName row = new UserName();

                        row.Code = clsMain.MyInt(item["Id"]);
                        row.Desc = clsMain.MyString(item["UserName"]);
                        objResult.Add(row);
                    }

                }


            }
            catch (Exception Ex)
            {
                return objResult;
            }
            return objResult;
        }

        [HttpGet]
        public IEnumerable<ReferenceEntry> GetRefRecord()
        {
            List<ReferenceEntry> objResult = new List<ReferenceEntry>();
            SQLHELPER obj = new SQLHELPER(ConnectionString);
            string str = "select * from ReferenceMaster   Order by ReferenceName";
            try
            {
                DataTable dt = obj.getTable(str);

                if (dt.Rows.Count > 0)
                {
                    objResult = dt.AsEnumerable()
                                            .Select(row => new ReferenceEntry()
                                            {
                                                ID = row.Field<int>("Id"),
                                                ReferenceName = row.Field<string>("ReferenceName"),
                                                PhoneNo = row.Field<string>("PhoneNo"),
                                                ContactPerson = row.Field<string>("ContactPerson"),
                                                EmailId = row.Field<string>("EmailId")

                                            }).ToList();

                }
            }
            catch (Exception ex)
            {
                return objResult;
            }


            return objResult;
        }

        [HttpGet]
        public IEnumerable<Prioritylist> GetpriorityRecord()
        {
            List<Prioritylist> objResult = new List<Prioritylist>();
            SQLHELPER obj = new SQLHELPER(ConnectionString);

            string str = " select* from priority ";

            try
            {
                DataTable dt = obj.getTable(str);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        Prioritylist row = new Prioritylist();

                        row.ID = clsMain.MyInt(item["Id"]);
                        row.priority1 = clsMain.MyString(item["priority"]);
                        objResult.Add(row);
                    }

                }


            }
            catch (Exception ex)
            {
                return objResult;
            }


            return objResult;
        }

        [HttpGet]
        public IEnumerable<ClientEntry> GetClientRecord()
        {
            List<ClientEntry> objResult = new List<ClientEntry>();
            SQLHELPER obj = new SQLHELPER(ConnectionString);
            string str = "select * from clientmaster where isnull(PhoneNo,'') <> '' Order by ClientName";
            try
            {
                DataTable dt = obj.getTable(str);

                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow item in dt.Rows)
                    {
                        ClientEntry row = new ClientEntry();

                        row.ID = clsMain.MyInt(item["Id"]);
                        row.ClientName = clsMain.MyString(item["ClientName"]);
                        row.PhoneNo = clsMain.MyString(item["PhoneNo"]);
                        row.ContactPerson = clsMain.MyString(item["ContactPerson"]);
                        row.EmailId = clsMain.MyString(item["EmailId"]);
                        //ID = row.Field<int>("Id"),
                        //ClientName = row.Field<string>("ClientName"),
                        //PhoneNo = row.Field<string>("PhoneNo"),
                        //ContactPerson = row.Field<string>("ContactPerson"),
                        //EmailId = row.Field<string>("EmailId"),
                        //Password = row.Field<string>("Password")
                        objResult.Add(row);
                    }

                }
            }
            catch (Exception ex)
            {
                return objResult;
            }


            return objResult;
        }

        [HttpGet]
        public IEnumerable<ProjectEntry> GetProjectRecordById(int VisitorPID)
        {
            List<ProjectEntry> objResult = new List<ProjectEntry>();
            SQLHELPER obj = new SQLHELPER(ConnectionString);
            string str = "select p.*,u.UserName from ProjectMaster p left outer join usermaster u on p.ProjectLeader=u.id where p.id = '" + VisitorPID + "' ";
            try
            {
                DataTable dt = obj.getTable(str);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        ProjectEntry row = new ProjectEntry();

                        row.ID = clsMain.MyInt(item["Id"]);
                        row.ProjectName = clsMain.MyString(item["ProjectName"]);
                        row.ProjectLeader = clsMain.MyString(item["ProjectLeader"]);
                        row.ContactPerson = clsMain.MyString(item["ContactPerson"]);
                        row.PhoneNo = clsMain.MyString(item["PhoneNo"]);
                        row.ProjectAdvance = clsMain.MyInt(item["ProjectAdvance"]);
                        row.ProjectValue = clsMain.MyInt(item["ProjectValue"]);
                        row.AssignTo = clsMain.MyInt(item["AssignTo"]);
                        row.ProjectDescription = clsMain.MyString(item["ProjectDescription"]);
                        row.ProjectManager = clsMain.MyInt(item["ProjectManager"]);
                        row.ReferenceName = clsMain.MyString(item["ReferenceName"]);
                        row.Priority = clsMain.MyInt(item["Priority"]);
                        row.Client = clsMain.MyInt(item["ClientId"]);
                        row.Status = clsMain.MyString(item["Status"]);
                        row.EstimatedTargetDate = Convert.ToDateTime(item["EstimatedTargetDate"]).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        row.EstimatedStartDate = Convert.ToDateTime(item["EstimatedStartDate"]).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        objResult.Add(row);
                    }

                }


            }
            catch (Exception ex)
            {
                return objResult;
            }


            return objResult;
        }

        [HttpGet]
        public IEnumerable<ProjectEntry> GetProjectRecord()
        {
            List<ProjectEntry> objResult = new List<ProjectEntry>();
            SQLHELPER obj = new SQLHELPER(ConnectionString);
            //string str = "select p.*,u.UserName from ProjectMaster p left outer join usermaster u on p.ProjectLeader=u.id ";
            //string str = "select p.*,u.UserName,c.ClientName,r.ReferenceName ReferenceName1  from ProjectMaster p,usermaster u, ClientMaster c,ReferenceMaster r where p.ProjectLeader=u.id and p.ReferenceName=r.ID and p.ClientId = c.Id";
            string str = "select p.*,up.UserName ProjectLeader,ua.UserName userassingto,pm.UserName ProjectManager," +
                " rn.ReferenceName RN,cn.ClientName cn,py.priority priorityn,((select sum(1) from TaskMaster where" +
                " ProjectId = p.Id and StatusType = 4)* 100/ (select sum(1) from TaskMaster where ProjectId = p.Id)) " +
                " per,(select TaskName from TaskMaster where  ProjectId = p.Id and CreatedDate = (select MAX(CreatedDate)" +
                " from TaskMaster where  ProjectId = p.Id)) TaskName, IsNull((select CreatedDate from TaskMaster where  " +
                " ProjectId = p.Id and CreatedDate = (select MAX(CreatedDate) from TaskMaster where  ProjectId = p.Id)),Getdate())" +
                " TaskDate from ProjectMaster p left outer join usermaster up on p.ProjectLeader=up.id " +
                " left outer join usermaster ua on p.assignto=ua.id left outer join usermaster pm on " +
                " p.ProjectManager=pm.id left outer join ReferenceMaster rn on p.ReferenceName=rn.id " +
                " left outer join ClientMaster cn on p.ClientId=cn.Id left outer join priority py on p.Priority=py.Id";

            try
            {
                DataTable dt = obj.getTable(str);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        ProjectEntry row = new ProjectEntry();

                        row.ID = clsMain.MyInt(item["Id"]);
                        row.ProjectName = clsMain.MyString(item["ProjectName"]);
                        row.ProjectLeader = clsMain.MyString(item["ProjectLeader"]);
                        row.ContactPerson = clsMain.MyString(item["ContactPerson"]);
                        row.PhoneNo = clsMain.MyString(item["PhoneNo"]);
                        row.ProjectAdvance = clsMain.MyInt(item["ProjectAdvance"]);
                        row.ProjectValue = clsMain.MyInt(item["ProjectValue"]);
                        row.Progress = clsMain.MyInt(item["per"]);
                        row.AssignTo = clsMain.MyInt(item["AssignTo"]);
                        row.ProjectDescription = clsMain.MyString(item["ProjectDescription"]);
                        row.ProjectManagerName = clsMain.MyString(item["ProjectManager1"]);
                        row.ReferenceName = clsMain.MyString(item["RN"]);
                        row.Priorityn = clsMain.MyString(item["priorityn"]);
                        row.ClientName = clsMain.MyString(item["cn"]);
                        row.Status = clsMain.MyString(item["Status"]);
                        row.EstimatedTargetDate = Convert.ToDateTime(item["EstimatedTargetDate"]).ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);
                        row.EstimatedStartDate = Convert.ToDateTime(item["EstimatedStartDate"]).ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);
                        row.TaskName = clsMain.MyString(item["TaskName"]);
                        row.TaskDate = Convert.ToDateTime(item["TaskDate"]).ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);
                        objResult.Add(row);
                    }

                }

                
            }
            catch (Exception ex)
            {
                return objResult;
            }


            return objResult;
        }

        [HttpPost]
        public string SaveProjectMaster(SaveProjectMaster objTask)
        {
            try
            {
                string msg = "";
                SqlParameter[] Para = new SqlParameter[17];
                Para[0] = new SqlParameter("@Id", clsMain.MyInt(objTask.ID));
                Para[1] = new SqlParameter("@ProjectName", clsMain.MyString(objTask.ProjectName));
                Para[2] = new SqlParameter("@ProjectLeader", clsMain.MyInt(objTask.ProjectLeader));
                Para[3] = new SqlParameter("@ContactPerson", clsMain.MyString(objTask.ContactPerson));
                Para[4] = new SqlParameter("@PhoneNo", clsMain.MyString(objTask.PhoneNo));
                Para[5] = new SqlParameter("@EstimatedTargetDate", Convert.ToDateTime(objTask.EstimatedTargetDate).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
                Para[6] = new SqlParameter("@OrgId", 1);
                Para[7] = new SqlParameter("@Priority", clsMain.MyInt(objTask.Priority));
                Para[8] = new SqlParameter("@EstimatedStartDate", Convert.ToDateTime(objTask.EstimatedStartDate).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
                Para[9] = new SqlParameter("@ProjectManager", clsMain.MyInt(objTask.ProjectManager));
                Para[10] = new SqlParameter("@ProjectDescription", clsMain.MyString(objTask.ProjectDescription));
                Para[11] = new SqlParameter("@AssignTo", clsMain.MyInt(objTask.AssignTo));
                Para[12] = new SqlParameter("@ProjectValue", clsMain.MyInt(objTask.ProjectValue));
                Para[13] = new SqlParameter("@ProjectAdvance", clsMain.MyInt(objTask.ProjectAdvance));
                Para[14] = new SqlParameter("@ReferenceName", clsMain.MyInt(objTask.ReferenceId));
                Para[15] = new SqlParameter("@ClientName", clsMain.MyInt(objTask.Client));
                //  Para[16] = new SqlParameter("@CompletionDate", Convert.ToDateTime(objTask.ProjectCompletionDate).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
                //Para[17] = new SqlParameter("@ProjectPoint", clsMain.MyInt(objTask.ProjectPoint));
                Para[16] = new SqlParameter("@projectAList", GetXml(objTask.AsslignList));

                DataTable dt = new ESCommon.SQLHELPER(ConnectionString).getTable("SP_SaveProjectMaster", Para);

                int ID = 0;
                if (dt != null && dt.Rows.Count > 0)
                {

                    ID = clsMain.MyInt(dt.Rows[0][0]);
                    if (dt != null && clsMain.MyInt(dt.Rows[0][1]) == 1)
                    {
                        msg = "Saved";
                    }

                    else if (dt != null && clsMain.MyInt(dt.Rows[0][1]) == 2)
                    {
                        msg = "Update Successfully";
                    }
                    else if (dt != null && clsMain.MyInt(dt.Rows[0][1]) == -4)
                    {

                        msg = "Project Name Already exists";
                    }
                }
                return ID + "," + msg;

            }
            catch (Exception Ex)
            {
                return Ex.Message;
            }

        }

        string GetXml(object obj1)
        {
            string ReturnVal = "";
            try
            {

                XmlSerializer xs = new XmlSerializer(obj1.GetType());
                StringWriter textWriter = new StringWriter();
                xs.Serialize(textWriter, obj1);
                string DoubleQ = @"""";
                string Remove2 = @"""xmlns:xsi=""" + "http://www.w3.org/2001/XMLSchema-instance" + DoubleQ;
                string Encoding = "<?xml version=" + DoubleQ + "1.0" + DoubleQ + " encoding=" + DoubleQ + "utf-16" + DoubleQ + "?>";
                string InnerChild1 = "xmlns:xsi=" + DoubleQ + "http://www.w3.org/2001/XMLSchema-instance" + DoubleQ;
                string InnerChild2 = "xmlns:xsd=" + DoubleQ + "http://www.w3.org/2001/XMLSchema" + DoubleQ;


                string XML = textWriter.ToString().Replace(Encoding, "").Replace(InnerChild1, "").Replace(InnerChild2, "");
                ReturnVal = XML;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ReturnVal;
        }

        [HttpPost]
        public string SavePriority(SavePriority objItem)
        {
            try
            {
                string Msg = "";
                SqlParameter[] Para = new SqlParameter[1];
                Para[0] = new SqlParameter("@ID", clsMain.MyString(objItem.ID));
                Para[0] = new SqlParameter("@PriorityID", clsMain.MyString(objItem.PriorityID));

                DataTable dt = new ESCommon.SQLHELPER(ConnectionString).getTable("SP_SavePriority", Para);
                if (dt != null && dt.Rows[0][0].ToString() == "1")
                {
                    Msg = "Updated";
                }
                else if (dt != null && dt.Rows[0][0].ToString() == "2")
                {
                    Msg = "Plz Enter Valid Password";
                }
                else
                    if (dt != null && dt.Rows[0][0].ToString() == "-4")
                {
                    Msg = " UseR Name Already exists";
                }

                return Msg;
            }
            catch (Exception Ex)
            {
                return Ex.Message;
            }
        }

        [HttpGet]
        public IEnumerable<Summarytable> GetSummaryTable()
        {
            List<Summarytable> objResult = new List<Summarytable>();
            try
            {
                //SqlParameter[] Para = new SqlParameter[2];
                //Para[0] = new SqlParameter("@VID", clsMain.MyInt(VID));
                //Para[1] = new SqlParameter("@PID", clsMain.MyInt(PID));
                DataTable dt = new ESCommon.SQLHELPER(ConnectionString).getTable("dbo.[sp_SummaryTable]");


                if (dt != null && dt.Rows.Count > 0)
                {

                    foreach (DataRow item in dt.Rows)
                    {
                        Summarytable row = new Summarytable();

                        row.Task = clsMain.MyInt(item["totaltask"]);
                        row.Progress = clsMain.MyInt(item["per"]);
                        row.Project = clsMain.MyString(item["Projectname"]);

                        row.completed = clsMain.MyInt(item["completed"]);
                        row.remaining = clsMain.MyInt(item["remaining"]);
                        row.overdue = clsMain.MyInt(item["overdue"]);

                        objResult.Add(row);
                    }

                }
                else
                {

                }
                return objResult;
            }
            catch (Exception Ex)
            {
                return objResult;
            }

        }

        [HttpGet]
        public IEnumerable<TaskList> GetTaskList(string UserId)
        {
            List<TaskList> objResult = new List<TaskList>();
            try
            {
                SqlParameter[] Para = new SqlParameter[1];
                Para[0] = new SqlParameter("@AsignTo", clsMain.MyInt(UserId)); //ESCommon.clsMain.MyPrintDate(ESCommon.clsMain.MyDate(objTask.VchDate), "yyyy-MM-dd"));//clsMain.MyDate(objPaymentEntry.VchDate));

                DataTable dt = new ESCommon.SQLHELPER(ConnectionString).getTable("dbo.[SP_GetTaskHistory]", Para);


                if (dt != null && dt.Rows.Count > 0)
                {

                    foreach (DataRow item in dt.Rows)
                    {
                        TaskList row = new TaskList();
                        row.Taskid = clsMain.MyString(item["TaskId"]);
                        row.ID = clsMain.MyInt(item["id"]);
                        row.ItemTypen = clsMain.MyString(item["ItemTypen"]);
                        row.Project = clsMain.MyString(item["ProjectName"]);
                        row.Client = clsMain.MyString(item["clientName"]);
                        row.Task = clsMain.MyString(item["TaskName"]);
                        row.AssignDate = Convert.ToDateTime(item["AssignDate"]).ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);
                        row.AssignTo = clsMain.MyString(item["UserName"]);
                        row.EstDate = Convert.ToDateTime(item["EstimatedDate"]).ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);
                        row.CompletionDate = Convert.ToDateTime(item["CompeletionDate"]).ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);
                        row.Statusn = clsMain.MyString(item["statusn"]);
                        row.check_in = clsMain.MyInt(item["check_in"]);
                        row.Priorityn = clsMain.MyString(item["priorityn"]);
                        row.TaskDesc = clsMain.MyString(item["TaskDesc"]);
                        objResult.Add(row);
                    }
                    //objResult = dt.AsEnumerable()
                    //        .Select(row => new TaskList()
                    //        {
                    //            Taskid = clsMain.MyString(row.Field<string>("TaskId")),
                    //            ID = clsMain.MyInt(row.Field<int>("id")),
                    //            ItemType = clsMain.MyInt(row.Field<int>("ItemType")),
                    //            Project = clsMain.MyString(row.Field<string>("ProjectName")),
                    //            Client = clsMain.MyString(row.Field<string>("clientName")),
                    //            Task = clsMain.MyString(row.Field<string>("TaskName")),
                    //            AssignDate = Convert.ToDateTime(row.Field<DateTime?>("AssignDate")),
                    //            AssignTo = clsMain.MyString(row.Field<string>("UserName")),
                    //            EstDate = Convert.ToDateTime(row.Field<DateTime?>("EstimateDate")),
                    //            CompletionDate = Convert.ToDateTime(row.Field<DateTime?>("CompDate")),
                    //            Status = clsMain.MyInt(row.Field<int>("Status")),
                    //            check_in = clsMain.MyInt(row.Field<int>("check_in")),
                    //        }).ToList();
                }
                else
                {


                }
                return objResult;
            }
            catch (Exception Ex)
            {
                return objResult;
            }

        }

        [HttpGet]
        public IEnumerable<TaskList> GetTaskList1(string UserId, int Project, int ItemTypeID, int? StatusID, int PriorityID)
        {
            List<TaskList> objResult = new List<TaskList>();
            try
            {
                SqlParameter[] Para = new SqlParameter[5];
                Para[0] = new SqlParameter("@AsignTo", clsMain.MyInt(UserId));

                if (Project == 0)
                {
                    Para[1] = new SqlParameter("@Project", DBNull.Value);
                }
                else
                {
                    Para[1] = new SqlParameter("@Project", clsMain.MyInt(Project));
                }
                if (ItemTypeID == 0)
                {
                    Para[2] = new SqlParameter("@ItemTypeID", DBNull.Value);
                }
                else
                {
                    Para[2] = new SqlParameter("@ItemTypeID", clsMain.MyInt(ItemTypeID));
                }
                if (StatusID == 0 || StatusID == null)
                {
                    Para[3] = new SqlParameter("@StatusID", DBNull.Value);
                }
                else
                {
                    Para[3] = new SqlParameter("@StatusID", clsMain.MyInt(StatusID));
                }
                if (PriorityID == 0)
                {
                    Para[4] = new SqlParameter("@PriorityID", DBNull.Value);
                }
                else
                {
                    Para[4] = new SqlParameter("@PriorityID", clsMain.MyInt(PriorityID));
                }


                DataTable dt = new ESCommon.SQLHELPER(ConnectionString).getTable("dbo.[SP_GetTaskHistory1]", Para);


                if (dt != null && dt.Rows.Count > 0)
                {

                    foreach (DataRow item in dt.Rows)
                    {
                        TaskList row = new TaskList();
                        row.Taskid = clsMain.MyString(item["TaskId"]);
                        row.ID = clsMain.MyInt(item["id"]);
                        row.ItemTypen = clsMain.MyString(item["ItemTypen"]);
                        row.Project = clsMain.MyString(item["ProjectName"]);
                        row.Client = clsMain.MyString(item["clientName"]);
                        row.Task = clsMain.MyString(item["TaskName"]);
                        row.AssignDate = Convert.ToDateTime(item["AssignDate"]).ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);
                        row.AssignTo = clsMain.MyString(item["UserName"]);
                        row.EstDate = Convert.ToDateTime(item["EstimatedDate"]).ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);
                        row.CompletionDate = Convert.ToDateTime(item["CompeletionDate"]).ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);
                        row.Statusn = clsMain.MyString(item["statusn"]);
                        row.check_in = clsMain.MyInt(item["check_in"]);
                        row.Priorityn = clsMain.MyString(item["priorityn"]);
                        row.TaskDesc = clsMain.MyString(item["TaskDesc"]);
                        objResult.Add(row);
                    }

                }
                else
                {


                }
                return objResult;
            }
            catch (Exception Ex)
            {
                return objResult;
            }

        }


        [HttpGet]
        public IEnumerable<TaskList> CheckInList(string UserId)
        {
            List<TaskList> objResult = new List<TaskList>();
            try
            {
                SqlParameter[] Para = new SqlParameter[1];
                Para[0] = new SqlParameter("@AsignTo", clsMain.MyInt(UserId)); //ESCommon.clsMain.MyPrintDate(ESCommon.clsMain.MyDate(objTask.VchDate), "yyyy-MM-dd"));//clsMain.MyDate(objPaymentEntry.VchDate));

                DataTable dt = new ESCommon.SQLHELPER(ConnectionString).getTable("dbo.[SP_CheckInList]", Para);


                if (dt != null && dt.Rows.Count > 0)
                {

                    foreach (DataRow item in dt.Rows)
                    {
                        TaskList row = new TaskList();
                        row.Taskid = clsMain.MyString(item["TaskId"]);
                        row.ID = clsMain.MyInt(item["id"]);
                        row.ItemTypen = clsMain.MyString(item["ItemTypen"]);
                        row.Project = clsMain.MyString(item["ProjectName"]);
                        row.Client = clsMain.MyString(item["clientName"]);
                        row.Task = clsMain.MyString(item["TaskName"]);
                        row.AssignDate = Convert.ToDateTime(item["AssignDate"]).ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);
                        row.AssignTo = clsMain.MyString(item["UserName"]);
                        row.EstDate = Convert.ToDateTime(item["EstimatedDate"]).ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);
                        row.CompletionDate = Convert.ToDateTime(item["CompeletionDate"]).ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);
                        row.Statusn = clsMain.MyString(item["statusn"]);
                        row.check_in = clsMain.MyInt(item["check_in"]);
                        row.Priorityn = clsMain.MyString(item["priorityn"]);
                        row.TaskDesc = clsMain.MyString(item["TaskDesc"]);
                        objResult.Add(row);
                    }

                }
                else
                {


                }
                return objResult;
            }
            catch (Exception Ex)
            {
                return objResult;
            }

        }

        [HttpGet]
        public IEnumerable<TaskList> TodayUserTask(string UserId)
        {
            List<TaskList> objResult = new List<TaskList>();
            try
            {
                SqlParameter[] Para = new SqlParameter[1];
                Para[0] = new SqlParameter("@AsignTo", clsMain.MyInt(UserId)); //ESCommon.clsMain.MyPrintDate(ESCommon.clsMain.MyDate(objTask.VchDate), "yyyy-MM-dd"));//clsMain.MyDate(objPaymentEntry.VchDate));

                DataTable dt = new ESCommon.SQLHELPER(ConnectionString).getTable("dbo.[SP_TodayUserTask]", Para);


                if (dt != null && dt.Rows.Count > 0)
                {

                    foreach (DataRow item in dt.Rows)
                    {
                        TaskList row = new TaskList();
                        row.Taskid = clsMain.MyString(item["TaskId"]);
                        row.ID = clsMain.MyInt(item["id"]);
                        row.ItemTypen = clsMain.MyString(item["ItemTypen"]);
                        row.Project = clsMain.MyString(item["ProjectName"]);
                        row.Client = clsMain.MyString(item["clientName"]);
                        row.Task = clsMain.MyString(item["TaskName"]);
                        row.AssignDate = Convert.ToDateTime(item["AssignDate"]).ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);
                        row.AssignTo = clsMain.MyString(item["UserName"]);
                        row.EstDate = Convert.ToDateTime(item["EstimatedDate"]).ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);
                        row.CompletionDate = Convert.ToDateTime(item["CompeletionDate"]).ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);
                        row.Statusn = clsMain.MyString(item["statusn"]);
                        row.check_in = clsMain.MyInt(item["check_in"]);
                        row.Priorityn = clsMain.MyString(item["priorityn"]);
                        row.TaskDesc = clsMain.MyString(item["TaskDesc"]);
                        objResult.Add(row);
                    }

                }
                else
                {


                }
                return objResult;
            }
            catch (Exception Ex)
            {
                return objResult;
            }

        }


        [HttpGet]
        public IEnumerable<totalCall> DashBoardPendinglist(string UserId)
        {
            List<totalCall> objResult = new List<totalCall>();
            try
            {
                SqlParameter[] Para = new SqlParameter[1];
                Para[0] = new SqlParameter("@AssignTo", clsMain.MyInt(UserId)); //ESCommon.clsMain.MyPrintDate(ESCommon.clsMain.MyDate(objTask.VchDate), "yyyy-MM-dd"));//clsMain.MyDate(objPaymentEntry.VchDate));

                DataTable dt = new ESCommon.SQLHELPER(ConnectionString).getTable("dbo.[Get_DashboardPendingList]", Para);


                if (dt != null && dt.Rows.Count > 0)
                {
                    objResult = dt.AsEnumerable()
                            .Select(row => new totalCall()
                            {

                                TotalCalls = clsMain.MyInt(row.Field<int>("TotalCall")),
                                PendingCall = clsMain.MyInt(row.Field<int>("PendingCall")),
                                TotalTask = clsMain.MyInt(row.Field<int>("TotalTask")),
                                PendingTask = clsMain.MyInt(row.Field<int>("PendingTask")),

                            }).ToList();
                }
                else
                {


                }
                return objResult;
            }
            catch (Exception Ex)
            {
                return objResult;
            }

        }



        [HttpGet]
        public IEnumerable<TaskList> GetTotalTaskList(string EmpCode)
        {
            List<TaskList> objResult = new List<TaskList>();
            try
            {
                DataTable dt = new ESCommon.SQLHELPER(ConnectionString).getTable("Exec sp_GetTotalTask @AssignTo=" + EmpCode + "");

                if (dt != null && dt.Rows.Count > 0)
                {


                    objResult = dt.AsEnumerable()
                            .Select(row => new TaskList()
                            {
                                Taskid = clsMain.MyString(row.Field<string>("TaskId")),
                                ID = clsMain.MyInt(row.Field<int>("id")),
                                Project = clsMain.MyString(row.Field<string>("ProjectName")),
                                Client = clsMain.MyString(row.Field<string>("clientName")),
                                Task = clsMain.MyString(row.Field<string>("TaskName")),
                                AssignDate = Convert.ToDateTime(row.Field<DateTime?>("AssignDate")).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                                AssignTo = clsMain.MyString(row.Field<string>("UserName")),
                                Teamleader = clsMain.MyString(row.Field<string>("Teamleader")),
                                EstDate = Convert.ToDateTime(row.Field<DateTime?>("EstimateDate")).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                                CompletionDate = Convert.ToDateTime(row.Field<DateTime?>("CompDate")).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                                Status = clsMain.MyInt(row.Field<int>("Status")),
                            }).ToList();
                }
                else
                {


                }

            }
            catch
            {

            }
            return objResult;

        }

        [HttpGet]
        public DashBoard GetAllHeaderTotal(string EmpCode)
        {
            DashBoard objResult = new DashBoard();
            try
            {
                string strqry = "select (select count(*) from ProjectMaster) as Project , " + Environment.NewLine +
                    "  (select COUNT(*) from ClientMaster) as client, " + Environment.NewLine +
                " (select COUNT(*) from TaskMaster) as task, " + Environment.NewLine +
                " (select COUNT(*) from usermaster  WHERE  id!=1) as usercount, " + Environment.NewLine +
                " (select COUNT(*) from TaskMaster Where ItemType=3) as Bugcount, " + Environment.NewLine +
                " (select COUNT(*) from TaskMaster Where StatusType=4) as taskcount, " + Environment.NewLine +
                " (select COUNT(*) from TaskMaster Where StatusType=4) as taskcount ";

                DataTable dt = new ESCommon.SQLHELPER(ConnectionString).getTable(strqry);

                if (dt != null && dt.Rows.Count > 0)
                {


                    objResult = dt.AsEnumerable()
                            .Select(row => new DashBoard()
                            {
                                TotalProject = clsMain.MyInt(row.Field<int>("Project")),
                                TotalClient = clsMain.MyInt(row.Field<int>("client")),
                                TotalTask = clsMain.MyInt(row.Field<int>("task")),
                                TotalUser = clsMain.MyInt(row.Field<int>("usercount")),

                                //TotalCompletedProject = clsMain.MyInt(row.Field<int>("ProjectCount")),
                                TotalCompletedTask = clsMain.MyInt(row.Field<int>("taskcount")),
                                TotalBug = clsMain.MyInt(row.Field<int>("Bugcount")),
                                //TotalCompletedBug = clsMain.MyInt(row.Field<int>("Bug")),

                            }).FirstOrDefault();
                }
                else
                {


                }
                return objResult;
            }
            catch (Exception Ex)
            {
                return objResult;
            }

        }

        [HttpGet]
        public IEnumerable<TaskList> GetPendingTaskList(string EmpCode)
        {
            List<TaskList> objResult = new List<TaskList>();
            try
            {
                DataTable dt = new ESCommon.SQLHELPER(ConnectionString).getTable("Exec sp_GetPendingTask @AssignTo=" + EmpCode + "");

                if (dt != null && dt.Rows.Count > 0)
                {


                    objResult = dt.AsEnumerable()
                            .Select(row => new TaskList()
                            {
                                Taskid = clsMain.MyString(row.Field<string>("TaskId")),
                                ID = clsMain.MyInt(row.Field<int>("id")),
                                Project = clsMain.MyString(row.Field<string>("ProjectName")),
                                Client = clsMain.MyString(row.Field<string>("clientName")),
                                Task = clsMain.MyString(row.Field<string>("TaskName")),
                                AssignDate = Convert.ToDateTime(row.Field<DateTime?>("AssignDate")).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                                AssignTo = clsMain.MyString(row.Field<string>("UserName")),
                                Teamleader = clsMain.MyString(row.Field<string>("Teamleader")),
                                EstDate = Convert.ToDateTime(row.Field<DateTime?>("EstimateDate")).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                                CompletionDate = Convert.ToDateTime(row.Field<DateTime?>("CompDate")).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                                Status = clsMain.MyInt(row.Field<int>("Status")),
                            }).ToList();
                }
                else
                {


                }

            }
            catch
            {

            }
            return objResult;

        }


        [HttpGet]
        public IEnumerable<ItemTypelist> GetItemTypeRecord()
        {
            List<ItemTypelist> objResult = new List<ItemTypelist>();
            SQLHELPER obj = new SQLHELPER(ConnectionString);

            string str = " select* from itemtype ";

            try
            {
                DataTable dt = obj.getTable(str);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        ItemTypelist row = new ItemTypelist();

                        row.ID = clsMain.MyInt(item["Id"]);
                        row.ItemType = clsMain.MyString(item["ItemType"]);
                        objResult.Add(row);
                    }

                }


            }
            catch (Exception ex)
            {
                return objResult;
            }


            return objResult;
        }


        [HttpGet]
        public IEnumerable<taskstatus> GettaskRecord()
        {
            List<taskstatus> objResult = new List<taskstatus>();
            SQLHELPER obj = new SQLHELPER(ConnectionString);

            string str = " select* from TStatus ";

            try
            {
                DataTable dt = obj.getTable(str);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        taskstatus row = new taskstatus();

                        row.ID = clsMain.MyInt(item["Id"]);
                        row.tstatus = clsMain.MyString(item["status"]);
                        objResult.Add(row);
                    }

                }


            }
            catch (Exception ex)
            {
                return objResult;
            }


            return objResult;
        }


        [HttpGet]
        public IEnumerable<ProjectName> GetProjectName()
        {
            List<ProjectName> objResult = new List<ProjectName>();
            SQLHELPER obj = new SQLHELPER(ConnectionString);

            string str = " select Id,ProjectName from ProjectMaster ";

            try
            {
                DataTable dt = obj.getTable(str);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        ProjectName row = new ProjectName();

                        row.Code = clsMain.MyInt(item["Id"]);
                        row.Desc = clsMain.MyString(item["ProjectName"]);
                        objResult.Add(row);
                    }

                }


            }
            catch (Exception ex)
            {
                return objResult;
            }
            return objResult;
        }

        [HttpGet]
        public IEnumerable<TaskList> GetTask(int TaskID)
        {
            List<TaskList> objResult = new List<TaskList>();
            try
            {
                SqlParameter[] Para = new SqlParameter[1];
                Para[0] = new SqlParameter("@TasId", clsMain.MyInt(TaskID)); //ESCommon.clsMain.MyPrintDate(ESCommon.clsMain.MyDate(objTask.VchDate), "yyyy-MM-dd"));//clsMain.MyDate(objPaymentEntry.VchDate));

                DataTable dt = new ESCommon.SQLHELPER(ConnectionString).getTable("dbo.[SP_GetTask]", Para);


                if (dt != null && dt.Rows.Count > 0)
                {

                    foreach (DataRow item in dt.Rows)
                    {
                        TaskList row = new TaskList();
                        row.Taskid = clsMain.MyString(item["TaskId"]);
                        row.Projectid = clsMain.MyInt(item["ProjectId"]);
                        row.ID = clsMain.MyInt(item["Id"]);
                        row.Clientid = clsMain.MyInt(item["ClientId"]);
                        row.Task = clsMain.MyString(item["TaskName"]);
                        row.AssignDate = Convert.ToDateTime(item["AssignDate"]).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        row.AssignToId = clsMain.MyInt(item["AssignTo"]);
                        row.EstDate = Convert.ToDateTime(item["EstimatedDate"]).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        row.CompletionDate = Convert.ToDateTime(item["CompeletionDate"]).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
                        row.ActualTime = clsMain.MyDouble(item["ActualTime"]);
                        row.EstTime = clsMain.MyDouble(item["EstimatedTime"]);
                        row.Status = clsMain.MyInt(item["StatusType"]);
                        row.Priority = clsMain.MyInt(item["Priority"]);
                        row.ItemType = clsMain.MyInt(item["ItemType"]);
                        row.TaskDesc = clsMain.MyString(item["TaskDesc"]);
                        row.TaskPoint = clsMain.MyInt(item["TaskPoint"]);

                        row.ImpStatus = clsMain.MyInt(item["Status"]);
                        row.Checkedby = clsMain.MyInt(item["CheckBy"]);
                        row.Unimple = clsMain.MyString(item["Unimple"]);
                        row.Setting = clsMain.MyString(item["Setting"]);
                        row.Closing = clsMain.MyString(item["Closing"]);
                        objResult.Add(row);
                    }

                }
                else
                {

                }
                return objResult;
            }
            catch (Exception Ex)
            {
                return objResult;
            }

        }

        [HttpPost]
        public string SaveTask(TaskEntry objTask)
        {

            try
            {
                string Msg = "";

                //objTask.EstDate = DateTime.ParseExact(objTask.EstDate,"dd/MM/yyyy, HH:mm:ss", null).ToString("MM/dd/yyyy");
                //objTask.AssignDate = DateTime.ParseExact(objTask.AssignDate, "dd/MM/yyyy, HH:mm:ss", null).ToString("MM/dd/yyyy");
                //objTask.CompletionDate = DateTime.ParseExact(objTask.CompletionDate, "dd/MM/yyyy, HH:mm:ss", null).ToString("MM/dd/yyyy");

                SqlParameter[] Para = new SqlParameter[24];
                Para[0] = new SqlParameter("@Project", clsMain.MyInt(objTask.Project)); //ESCommon.clsMain.MyPrintDate(ESCommon.clsMain.MyDate(objTask.VchDate), "yyyy-MM-dd"));//clsMain.MyDate(objPaymentEntry.VchDate));
                Para[1] = new SqlParameter("@Client", clsMain.MyInt(objTask.Client));
                Para[2] = new SqlParameter("@Task", clsMain.MyString(objTask.Task));
                Para[3] = new SqlParameter("@AssignDate", objTask.AssignDate);//ESCommon.clsMain.MyPrintDate(Convert.ToDateTime( objTask.AssignDate), "yyyy-MM-dd"));// clsMain.MyString(objTask.AssignDate));
                Para[4] = new SqlParameter("@AssignTo", clsMain.MyInt(objTask.AssignTo));
                Para[5] = new SqlParameter("@ActualTime", clsMain.MyDouble(objTask.ActualTime));
                Para[6] = new SqlParameter("@EstTime", clsMain.MyDouble(objTask.EstTime));
                Para[7] = new SqlParameter("@Status", clsMain.MyInt(objTask.Status));
                Para[8] = new SqlParameter("@ImpStatus", clsMain.MyInt(objTask.ImpStatus));
                Para[9] = new SqlParameter("@Checkedby", clsMain.MyInt(objTask.Checkedby));

                Para[10] = new SqlParameter("@EstDate", objTask.EstDate);//clsMain.MyString(objTask.EstDate));
                Para[11] = new SqlParameter("@CompletionDate", objTask.CompletionDate);//clsMain.MyString(objTask.CompletionDate));
                Para[12] = new SqlParameter("@CompletedVer", clsMain.MyString(objTask.CompletedVer));
                Para[13] = new SqlParameter("@Unimple", clsMain.MyString(objTask.Unimple));
                Para[14] = new SqlParameter("@Setting", clsMain.MyString(objTask.Setting));
                Para[15] = new SqlParameter("@Closing", clsMain.MyString(objTask.Closing));
                Para[16] = new SqlParameter("@EmpCode", clsMain.MyInt(objTask.EmpCode));
                Para[17] = new SqlParameter("@TasId", clsMain.MyInt(objTask.TaskID));
                Para[18] = new SqlParameter("@CallID", clsMain.MyInt(objTask.CallID));
                Para[19] = new SqlParameter("@ItemType", clsMain.MyInt(objTask.ItemType));
                Para[20] = new SqlParameter("@Priority", clsMain.MyInt(objTask.Priority));
                Para[21] = new SqlParameter("@TaskDesc", clsMain.MyString(objTask.TaskDesc));
                Para[22] = new SqlParameter("@TaskPoint", clsMain.MyString(objTask.TaskPoint));
                Para[23] = new SqlParameter("@TaskAList", GetXml(objTask.AsslignList));

                //System.IO.File.AppendAllText(@"D:\Error1.txt", objLeavEntry.ToDate.ToString());
                DataTable dt = new ESCommon.SQLHELPER(ConnectionString).getTable("dbo.[SP_SaveTask]", Para);
                int ID = 0;
                if (dt != null && dt.Rows.Count > 0)
                {
                    ID = clsMain.MyInt(dt.Rows[0][0]);
                    if (dt != null && clsMain.MyInt(dt.Rows[0][1]) == 1)
                    {
                        Msg = "Saved";
                    }
                    else if (dt != null && clsMain.MyInt(dt.Rows[0][1]) == 2)
                    {
                        Msg = "Update Successfully";
                    }
                    else if (dt != null && clsMain.MyInt(dt.Rows[0][1]) == -5)
                    {
                        Msg = "Task Name Already Exists";
                    }
                }
                return ID + "," + Msg;

            }
            catch (Exception Ex)
            {
                return Ex.Message;
            }

        }

        [HttpGet]
        public IEnumerable<UserTypeController> GetUserType()
        {
            List<UserTypeController> userlist = new List<UserTypeController>();
            userlist.Add(new UserTypeController { UserCode = 1, UserType = "Developer" });
            //userlist.Add(new UserTypeController { UserCode = 2, UserType = "Admin" });
            userlist.Add(new UserTypeController { UserCode = 2, UserType = "ProjectManager" });
            userlist.Add(new UserTypeController { UserCode = 3, UserType = "TeamLeader" });



            return userlist;
        }

        [HttpGet]
        public IEnumerable<UserEntry> GetUserListById(int VisitorUID)
        {
            List<UserEntry> objResult = new List<UserEntry>();
            SQLHELPER obj = new SQLHELPER(ConnectionString);
            //string str = "Exec SP_UserMaster @UserId='" + userid + "' ";
            string str = "select * from UserMaster u where u.id = '" + VisitorUID + "'";

            try
            {
                DataTable dt = obj.getTable(str);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {

                        UserEntry row = new UserEntry();

                        row.ID = clsMain.MyInt(item["Id"]);
                        row.UserName = clsMain.MyString(item["UserName"]);
                        row.Mobile = clsMain.MyString(item["PhoneNo"]);
                        row.MailID = clsMain.MyString(item["EmailId"]);
                        row.UserType = clsMain.MyInt(item["UserType"]);
                        row.Password = clsMain.MyString(item["Password"]);
                        objResult.Add(row);

                    }
                }
            }
            catch (Exception ex)
            {
                return objResult;
            }


            return objResult;
        }

        [HttpGet]
        public List<TaskInfo> TaskInfo()
        {
            string str = "Exec SP_GetTaskInfo";
            List<TaskInfo> objResult = new List<TaskInfo>();


            List<MasterInfo> objClient = new List<MasterInfo>();
            List<MasterInfo> objAssignTo = new List<MasterInfo>();

            SQLHELPER obj1 = new SQLHELPER(ConnectionString);
            DataSet TaskInfo = obj1.getDataSet(str);
            if (TaskInfo.Tables.Count > 0)
            {
                DataTable dtProj = TaskInfo.Tables[0];
                DataTable dtClient = TaskInfo.Tables[1];
                DataTable dtAssignTo = TaskInfo.Tables[2];


                if (dtClient.Rows.Count > 0)
                {
                    objClient = dtClient.AsEnumerable()
                                .Select(row => new MasterInfo()
                                {
                                    select = row.Field<string>("ngSelect"),
                                    Code = row.Field<int>("Id"),
                                    Desc = row.Field<string>("ClientName"),
                                }).ToList();
                }

                if (dtAssignTo.Rows.Count > 0)
                {

                    objAssignTo = dtAssignTo.AsEnumerable()
                                .Select(row => new MasterInfo()
                                {

                                    Code = row.Field<int>("Id"),
                                    Desc = row.Field<string>("UserName"),
                                }).ToList();
                }

                if (dtProj.Rows.Count > 0)
                {
                    objResult = dtProj.AsEnumerable()
                        .Select(row => new TaskInfo()
                        {

                            Code = row.Field<int>("Id"),
                            Desc = row.Field<string>("ProjectName"),
                            select = row.Field<string>("ngSelect"),
                            Client = objClient,
                            AssignTo = objAssignTo

                        }).ToList();
                }
                else
                {
                    TaskInfo row = new TaskInfo();

                    row.Client = objClient;
                    row.AssignTo = objAssignTo;
                    objResult.Add(row);
                }


            }


            return objResult;
        }


        [HttpGet]
        public IEnumerable<UserEntry> GetUserList(string userid)
        {
            List<UserEntry> objResult = new List<UserEntry>();
            SQLHELPER obj = new SQLHELPER(ConnectionString);
            //string str = "Exec SP_UserMaster @UserId='" + userid + "' ";
            //string str = "select * from UserMaster";
            string str = "select u.*,ut.UserType Role,ISNULL((select TaskName  from TaskMaster where AssignTo = u.id " + Environment.NewLine +
                " and id = (select MAX(id) from TaskMaster where AssignTo=u.id)),'') TaskName," + Environment.NewLine +
                " ISNULL((select CONVERT(VARCHAR(11), AssignDate, 106 ) as AssignDate  from TaskMaster where AssignTo = u.id and id = (select MAX(id) from " + Environment.NewLine +
                " TaskMaster where AssignTo=u.id)),'')LastDate from UserMaster u left outer join UserTypeMast ut" + Environment.NewLine +
                " on u.UserType=ut.ID where U.Id !=1";
            //AND T.AssignTo='"+userid+ "'

            try
            {
                DataTable dt = obj.getTable(str);

                if (dt.Rows.Count > 0)
                {

                    foreach (DataRow item in dt.Rows)
                    {
                        UserEntry row = new UserEntry();

                        row.ID = clsMain.MyInt(item["Id"]);
                        row.UserName = clsMain.MyString(item["UserName"]);
                        row.Mobile = clsMain.MyString(item["PhoneNo"]);
                        row.MailID = clsMain.MyString(item["EmailId"]);
                        row.Password = clsMain.MyString(item["Password"]);
                        row.IsAdmin = clsMain.MyInt(item["UserType"]);
                        row.Role = clsMain.MyString(item["Role"]);
                        row.ECPassword = clsMain.MyString(item["Password"]);
                        row.Deactivate = clsMain.MyInt(item["deactivate"]);
                        row.TeamLeader = clsMain.MyInt(item["TeamLeader"]);
                        row.TaskName = clsMain.MyString(item["TaskName"]);
                        // row.TaskDate = Convert.ToDateTime(item["LastDate"]).ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);
                        row.TaskDate = clsMain.MyString(item["LastDate"]);
                        objResult.Add(row);
                    }

                }
            }
            catch (Exception ex)
            {
                return objResult;
            }


            return objResult;
        }

        [HttpPost]
        public string UserEntry(SaveUserEntry objTask)
        {
            try
            {
                string Msg = "";
                SqlParameter[] Para = new SqlParameter[10];
                Para[0] = new SqlParameter("@ID", clsMain.MyInt(objTask.Id));
                Para[1] = new SqlParameter("@UserName", clsMain.MyString(objTask.UserName));
                Para[2] = new SqlParameter("@PhoneNo", clsMain.MyString(objTask.Mobile));
                Para[3] = new SqlParameter("@Password", clsMain.MyString(objTask.Password));
                Para[4] = new SqlParameter("@OrgId", clsMain.MyInt(objTask.OrgId));
                Para[5] = new SqlParameter("@EmailId", clsMain.MyString(objTask.MailID));
                Para[6] = new SqlParameter("@PassID", clsMain.MyInt(objTask.PassID));
                Para[7] = new SqlParameter("@UserType", clsMain.MyInt(objTask.UserType));
                Para[8] = new SqlParameter("@Deactivate", clsMain.MyInt(objTask.Deactivate));
                Para[9] = new SqlParameter("@TeamLeader", clsMain.MyInt(objTask.TeamLeader));

                DataTable dt = new ESCommon.SQLHELPER(ConnectionString).getTable("SP_SaveUserEnty", Para);
                if (dt != null && dt.Rows[0][0].ToString() == "1")
                {
                    Msg = "Saved";
                }
                else
                    if (dt != null && dt.Rows[0][0].ToString() == "2")
                {
                    Msg = "Update Successfully";
                }
                else
                        if (dt != null && dt.Rows[0][0].ToString() == "-4")
                {
                    Msg = "  Email ID Already exists";
                }
                return Msg;

            }
            catch (Exception Ex)
            {
                return Ex.Message;
            }

        }


        [HttpGet]
        public IEnumerable<ClientEntry> GetClientRecordById(int VisitorCID)
        {
            List<ClientEntry> objResult = new List<ClientEntry>();
            SQLHELPER obj = new SQLHELPER(ConnectionString);
            //string str = "select * from clientmaster where clientmaster.id = '" + VisitorCID + "' where isnull(PhoneNo,'') <> '' Order by ClientName ";
            string str = "select * from clientmaster where clientmaster.id = '" + VisitorCID + "'";
            // string str = "select c.*,r.ReferenceName ReferenceName1 from clientmaster c,ReferenceMaster r select c.*,r.ReferenceName from clientmaster c,ReferenceMaster r where  c.ReferenceName=r.ID and c.id = '" + VisitorCID + "'";
            try
            {
                DataTable dt = obj.getTable(str);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        ClientEntry row = new ClientEntry();

                        row.ID = clsMain.MyInt(item["Id"]);
                        row.ClientName = clsMain.MyString(item["ClientName"]);
                        row.PhoneNo = clsMain.MyString(item["PhoneNo"]);
                        row.ContactPerson = clsMain.MyString(item["ContactPerson"]);
                        row.EmailId = clsMain.MyString(item["EmailId"]);
                        row.ReferenceID = clsMain.MyString(item["ReferenceName"]);

                        objResult.Add(row);
                    }

                }


            }
            catch (Exception ex)
            {
                return objResult;
            }


            return objResult;
        }

        [HttpPost]
        public string SaveClientMaster(ClientEntry objTask)
        {
            try
            {
                string msg = "";
                SqlParameter[] Para = new SqlParameter[8];
                Para[0] = new SqlParameter("@Id", clsMain.MyInt(objTask.ID));
                Para[1] = new SqlParameter("@ClientName", clsMain.MyString(objTask.ClientName));
                Para[2] = new SqlParameter("@PhoneNo", clsMain.MyString(objTask.PhoneNo));
                Para[3] = new SqlParameter("@ContactPerson", clsMain.MyString(objTask.ContactPerson));
                Para[4] = new SqlParameter("@EmailId", clsMain.MyString(objTask.EmailId));
                Para[5] = new SqlParameter("@Password", clsMain.MyString(objTask.Password));
                Para[6] = new SqlParameter("@ReferenceName", clsMain.MyString(objTask.ReferenceID));

                Para[7] = new SqlParameter("@OrgId", 1);
                DataTable dt = new ESCommon.SQLHELPER(ConnectionString).getTable("dbo.SP_ClientEntry", Para);


                if (dt != null && dt.Rows[0][0].ToString() == "1")
                {
                    msg = "Saved";
                }
                else
                    if (dt != null && dt.Rows[0][0].ToString() == "2")
                {
                    msg = "Update Successfully";
                }
                else
                        if (dt != null && dt.Rows[0][0].ToString() == "-4")
                {
                    msg = " Email ID Already exists";
                }
                return msg;

            }
            catch (Exception Ex)
            {
                return Ex.Message;
            }

        }

        [HttpPost]
        public string SaveReferenceMaster(ReferenceEntry objTask)
        {
            try
            {
                string msg = "";
                SqlParameter[] Para = new SqlParameter[5];
                Para[0] = new SqlParameter("@Id", clsMain.MyInt(objTask.ID));
                Para[1] = new SqlParameter("@ReferenceName", clsMain.MyString(objTask.ReferenceName));
                Para[2] = new SqlParameter("@PhoneNo", clsMain.MyString(objTask.PhoneNo));
                Para[3] = new SqlParameter("@ContactPerson", clsMain.MyString(objTask.ContactPerson));
                Para[4] = new SqlParameter("@EmailId", clsMain.MyString(objTask.EmailId));
                //Para[5] = new SqlParameter("@Password", clsMain.MyString(objTask.ClientName));
                //Para[5] = new SqlParameter("@OrgId", 1);
                DataTable dt = new ESCommon.SQLHELPER(ConnectionString).getTable("dbo.SP_ReferenceEntry", Para);


                if (dt != null && dt.Rows[0][0].ToString() == "1")
                {
                    msg = "Saved";
                }
                else
                    if (dt != null && dt.Rows[0][0].ToString() == "2")
                {
                    msg = "Update Successfully";
                }
                else
                        if (dt != null && dt.Rows[0][0].ToString() == "-4")
                {
                    msg = "Email ID Already exists";
                }
                return msg;

            }
            catch (Exception Ex)
            {
                return Ex.Message;
            }

        }

        [HttpGet]
        public IEnumerable<ReferenceEntry> GetReferenceRecordById(int VisitorRID)
        {
            List<ReferenceEntry> objResult = new List<ReferenceEntry>();
            SQLHELPER obj = new SQLHELPER(ConnectionString);
            string str = "select * from ReferenceMaster R   where R.id = '" + VisitorRID + "'  Order by ReferenceName  ";
            try
            {
                DataTable dt = obj.getTable(str);

                if (dt.Rows.Count > 0)
                {
                    objResult = dt.AsEnumerable()
                                            .Select(row => new ReferenceEntry()
                                            {
                                                ID = row.Field<int>("Id"),
                                                ReferenceName = row.Field<string>("ReferenceName"),
                                                PhoneNo = row.Field<string>("PhoneNo"),
                                                ContactPerson = row.Field<string>("ContactPerson"),
                                                EmailId = row.Field<string>("EmailId"),

                                            }).ToList();

                }
            }
            catch (Exception ex)
            {
                return objResult;
            }


            return objResult;
        }

        [HttpGet]
        public IEnumerable<ReportType> GetReportType()
        {
            List<ReportType> objResult = new List<ReportType>();
            SQLHELPER obj = new SQLHELPER(ConnectionString);

            string str = " select* from ReportType ";

            try
            {
                DataTable dt = obj.getTable(str);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow item in dt.Rows)
                    {
                        ReportType row = new ReportType();

                        row.Code = clsMain.MyInt(item["Id"]);
                        row.Desc = clsMain.MyString(item["ReportType"]);
                        objResult.Add(row);
                    }

                }


            }
            catch (Exception ex)
            {
                return objResult;
            }


            return objResult;
        }


        [HttpGet]
        public IEnumerable<ManageTaskNew> UserTaskStatusnew(DateTime FromDate, DateTime ToDate, int AssignTo)
        {

            List<ManageTaskNew> objResult = new List<ManageTaskNew>();
            try
            {
                SqlParameter[] Para = new SqlParameter[3];
                Para[0] = new SqlParameter("@AsignTo", clsMain.MyInt(AssignTo)); //ESCommon.clsMain.MyPrintDate(ESCommon.clsMain.MyDate(objTask.VchDate), "yyyy-MM-dd"));//clsMain.MyDate(objPaymentEntry.VchDate));
                Para[1] = new SqlParameter("@FromDate", FromDate.ToString("yyyy/MM/dd "));//clsMain.MyString(FromDate));
                Para[2] = new SqlParameter("@ToDate", ToDate.ToString("yyyy/MM/dd "));




                DataTable dt = new ESCommon.SQLHELPER(ConnectionString).getTable("SP_UserTaskDetailNewupdated", Para);


                if (dt != null && dt.Rows.Count > 0)
                {
                    objResult = dt.AsEnumerable()
                            .Select(row => new ManageTaskNew()
                            {
                                //TaskId = clsMain.MyString(row.Field<string>("TaskId")),


                                TotalProject = clsMain.MyInt(row.Field<int>("TotalProject")),
                                TotalTask = clsMain.MyInt(row.Field<int>("TotalTask")),
                                UserName = clsMain.MyString(row.Field<string>("UserName")),

                                Progress = clsMain.MyInt(row.Field<int>("Progress")),
                                FailTask = clsMain.MyInt(row.Field<int>("Fail")),
                                SuccessTask = clsMain.MyInt(row.Field<int>("Success")),
                                TaskDate = Convert.ToDateTime(row.Field<DateTime?>("LastDate")),

                                //EstTime = clsMain.MyDouble(row.Field<double>("ExpHours")),
                                //TotalTimeTaken = clsMain.MyString(row.Field<string>("finalTimeTaken")),


                            }).ToList();
                }
                else
                {


                }
                return objResult;
            }
            catch (Exception Ex)
            {
                return objResult;
            }

        }

        [HttpGet]
        public IEnumerable<ManageTaskNew> UserTaskStatusnew1()
        {

            List<ManageTaskNew> objResult = new List<ManageTaskNew>();

            string str = "select x.UserName,x.TotalTask,x.TotalProject,x.Fail,x.Success,CASE WHEN X.TotalTask < 1  THEN 	CASE WHEN X.Success<1 THEN 0	ELSE 0 	END	WHEN X.TotalTask>0 THEN	CASE WHEN X.Success<1 THEN 0	else (X.Success*100/X.TotalTask)	END 	 END Progress, ISNULL((select TaskName  from TaskMaster  where AssignTo = u.id and id = (select MAX(id) from TaskMaster where AssignTo=u.id)),'') TaskName, ISNULL((select AssignDate  from TaskMaster where AssignTo = u.id and id = (select MAX(id) from TaskMaster where AssignTo=u.id)),'')LastDate  from (select   um.UserName,count(distinct tm.projectid)as TotalProject,count(*) As TotalTask ,(select count(IsSuccess) as Fail from Taskmaster where IsSuccess=-1 and AssignTo= tm.AssignTo)as Fail ,(select count(IsSuccess) as Success from Taskmaster where IsSuccess=1 and AssignTo= tm.AssignTo)as Success from TaskMaster tm left outer join UserMaster um on tm.AssignTo=um.Id where tm.AssignTo not in (1,0)and tm.AssignTo is not null   group by UserName,tm.AssignTo) x left outer join UserMaster u on x.UserName=u.username";
            try
            {
                DataTable dt = new ESCommon.SQLHELPER(ConnectionString).getTable(str);


                if (dt != null && dt.Rows.Count > 0)
                {
                    objResult = dt.AsEnumerable()
                            .Select(row => new ManageTaskNew()
                            {
                                //TaskId = clsMain.MyString(row.Field<string>("TaskId")),


                                TotalProject = clsMain.MyInt(row.Field<int>("TotalProject")),
                                TotalTask = clsMain.MyInt(row.Field<int>("TotalTask")),
                                UserName = clsMain.MyString(row.Field<string>("UserName")),

                                Progress = clsMain.MyInt(row.Field<int>("Progress")),
                                FailTask = clsMain.MyInt(row.Field<int>("Fail")),
                                SuccessTask = clsMain.MyInt(row.Field<int>("Success")),
                                TaskDate = Convert.ToDateTime(row.Field<DateTime?>("LastDate")),

                                //EstTime = clsMain.MyDouble(row.Field<double>("ExpHours")),
                                //TotalTimeTaken = clsMain.MyString(row.Field<string>("finalTimeTaken")),


                            }).ToList();
                }
                else
                {


                }
                return objResult;
            }
            catch (Exception Ex)
            {
                return objResult;
            }

        }


        [HttpGet]
        public IEnumerable<ManageTaskNew1> ProjectTaskStatusnew(DateTime FromDate, DateTime ToDate, int ProjectId, int ReportTypeId)
        {
            List<ManageTaskNew1> objResult = new List<ManageTaskNew1>();
            try
            {
                SqlParameter[] Para = new SqlParameter[4];
                Para[0] = new SqlParameter("@ProjectId", clsMain.MyInt(ProjectId)); //ESCommon.clsMain.MyPrintDate(ESCommon.clsMain.MyDate(objTask.VchDate), "yyyy-MM-dd"));//clsMain.MyDate(objPaymentEntry.VchDate));
                Para[1] = new SqlParameter("@FromDate", ESCommon.clsMain.MyPrintDate(ESCommon.clsMain.MyDate(FromDate), "yyyy-MM-dd"));//clsMain.MyString(FromDate));
                Para[2] = new SqlParameter("@ToDate", ESCommon.clsMain.MyPrintDate(ESCommon.clsMain.MyDate(ToDate), "yyyy-MM-dd"));//clsMain.MyString(ToDate));
                Para[3] = new SqlParameter("@ReportTypeId", clsMain.MyInt(ReportTypeId));


                DataTable dt = new ESCommon.SQLHELPER(ConnectionString).getTable("SP_ProjectTaskDetailNew", Para);


                if (dt != null && dt.Rows.Count > 0)
                {
                    objResult = dt.AsEnumerable()
                            .Select(row => new ManageTaskNew1()
                            {
                                //TaskId = clsMain.MyString(row.Field<string>("TaskId")),


                                TotalTask = clsMain.MyInt(row.Field<int>("TotalTask")),

                                ProjectName = clsMain.MyString(row.Field<string>("ProjectName")),
                                Progress = clsMain.MyInt(row.Field<int>("Progress")),

                                FailTask = clsMain.MyInt(row.Field<int>("Fail")),
                                SuccessTask = clsMain.MyInt(row.Field<int>("Success")),
                                //EstTime = clsMain.MyDouble(row.Field<double>("ExpHours")),
                                //TotalTimeTaken = clsMain.MyString(row.Field<string>("finalTimeTaken")),


                            }).ToList();
                }
                else
                {


                }
                return objResult;
            }
            catch (Exception Ex)
            {
                return objResult;
            }

        }

        [HttpGet]
        public IEnumerable<ManageTaskNew1> ProjectTaskStatusnew1()
        {
            List<ManageTaskNew1> objResult = new List<ManageTaskNew1>();
            string str = "select x.ProjectName,x.TotalTask,x.Fail,x.Success,CASE WHEN X.TotalTask < 1  THEN 	CASE WHEN X.Success<1 THEN 0	ELSE 0 	END	WHEN X.TotalTask>0 THEN	CASE WHEN X.Success<1 THEN 0	else (X.Success*100/X.TotalTask)	END END Progress from (select   pm.ProjectName,count(*) As TotalTask, (select count(IsSuccess) as Fail from Taskmaster where IsSuccess=-1 and Projectid= pm.Id)as Fail,(select count(IsSuccess) as Success from Taskmaster where IsSuccess=1 and Projectid= pm.Id)as Success from TaskMaster tm left outer join ProjectMaster pm on tm.ProjectId=pm.Id where tm.AssignTo not in (1,0) and tm.AssignTo is not null  group by ProjectName, pm.Id) x";
            try
            {

                DataTable dt = new ESCommon.SQLHELPER(ConnectionString).getTable(str);


                if (dt != null && dt.Rows.Count > 0)
                {
                    objResult = dt.AsEnumerable()
                            .Select(row => new ManageTaskNew1()
                            {
                                //TaskId = clsMain.MyString(row.Field<string>("TaskId")),


                                TotalTask = clsMain.MyInt(row.Field<int>("TotalTask")),

                                ProjectName = clsMain.MyString(row.Field<string>("ProjectName")),
                                Progress = clsMain.MyInt(row.Field<int>("Progress")),

                                FailTask = clsMain.MyInt(row.Field<int>("Fail")),
                                SuccessTask = clsMain.MyInt(row.Field<int>("Success")),
                                //EstTime = clsMain.MyDouble(row.Field<double>("ExpHours")),
                                //TotalTimeTaken = clsMain.MyString(row.Field<string>("finalTimeTaken")),


                            }).ToList();
                }
                else
                {


                }
                return objResult;
            }
            catch (Exception Ex)
            {
                return objResult;
            }

        }


        [HttpPost]
        public string CheckInStatus(CheckInStatus objTask)
        {
            try
            {
                string Msg = "";
                SqlParameter[] Para = new SqlParameter[4];
                Para[0] = new SqlParameter("@id", clsMain.MyInt(objTask.TaskID));
                Para[1] = new SqlParameter("@CheckIn", clsMain.MyInt(objTask.CheckIn));
                Para[2] = new SqlParameter("@CheckOut", clsMain.MyInt(objTask.CheckOut));
                Para[3] = new SqlParameter("@CreatedBy", clsMain.MyInt(objTask.EmpCode));

                DataTable dt = new ESCommon.SQLHELPER(ConnectionString).getTable("dbo.[SP_CheckIn]", Para);
                if (dt != null && dt.Rows[0][0].ToString() == "1")
                {
                    Msg = "Saved";  //Saved
                }
                if (dt != null && dt.Rows[0][0].ToString() == "-2")
                {
                    Msg = "Task Check out..!"; // Check out
                }
                if (dt != null && dt.Rows[0][0].ToString() == "-4")
                {
                    Msg = "One Task Already Check-In plz CheckOut First..!";
                }

                if (dt != null && dt.Rows[0][0].ToString() == "-3")
                {
                    Msg = ""; //Not CheckIn
                }


                return Msg;



            }
            catch (Exception Ex)
            {
                return Ex.Message;
            }

        }



        [HttpGet]
        public IEnumerable<TaskList> ConvertCallToTask(int CallID)
        {
            List<TaskList> objResult = new List<TaskList>();
            try
            {
                SqlParameter[] Para = new SqlParameter[1];
                Para[0] = new SqlParameter("@CallID", clsMain.MyInt(CallID)); //ESCommon.clsMain.MyPrintDate(ESCommon.clsMain.MyDate(objTask.VchDate), "yyyy-MM-dd"));//clsMain.MyDate(objPaymentEntry.VchDate));

                DataTable dt = new ESCommon.SQLHELPER(ConnectionString).getTable("dbo.[SP_CallConvertToTask]", Para);


                if (dt != null && dt.Rows.Count > 0)
                {
                    objResult = dt.AsEnumerable()
                            .Select(row => new TaskList()
                            {
                                Projectid = clsMain.MyInt(row.Field<int>("ProjectId")),
                                Clientid = clsMain.MyInt(row.Field<int>("Client")),
                                AssignToId = clsMain.MyInt(row.Field<int>("AssignID")),
                                Task = clsMain.MyString(row.Field<string>("discription")),
                                AssignDate = Convert.ToDateTime(row.Field<DateTime?>("CreatedDate")).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),

                            }).ToList();
                }
                else
                {


                }
                return objResult;
            }
            catch (Exception Ex)
            {
                return objResult;
            }

        }

        [HttpPost]
        public string Checkoutstatus(ReasonStatus objTask)
        {
            try
            {
                CheckInStatus objstatus = new Models.CheckInStatus();
                objstatus.CheckOut = 2;
                objstatus.CheckIn = 1;
                objstatus.EmpCode = objTask.EmpId;
                objstatus.TaskID = objTask.TaskId;
                CheckInStatus(objstatus);
                string Msg = "";
                SqlParameter[] Para = new SqlParameter[3];
                Para[0] = new SqlParameter("@StatusID", clsMain.MyInt(objTask.StatusID));
                Para[1] = new SqlParameter("@Reason", clsMain.MyString(objTask.Reason));
                Para[2] = new SqlParameter("@ID", clsMain.MyString(objTask.TaskId));

                DataTable dt = new ESCommon.SQLHELPER(ConnectionString).getTable("SP_Savecheckoutreason", Para);

                if (dt != null && dt.Rows[0][0].ToString() == "1")
                {
                    Msg = "Saved";  //Saved
                }
                if (dt != null && dt.Rows[0][0].ToString() == "2")
                {
                    Msg = "Task Check out..!"; // Check out
                }
                if (dt != null && dt.Rows[0][0].ToString() == "-4")
                {
                    Msg = "One Task Already Check-In plz CheckOut First..!";
                }

                if (dt != null && dt.Rows[0][0].ToString() == "-3")
                {
                    Msg = ""; //Not CheckIn
                }

                //if (dt != null && dt.Rows[0][0].ToString() == "1")
                //{
                //    Msg = "Saved";
                //}
                //else
                //    if (dt != null && dt.Rows[0][0].ToString() == "2")
                //    {
                //        Msg = "Update Successfully";
                //    }
                //    else
                //        if (dt != null && dt.Rows[0][0].ToString() == "-4")
                //        {
                //            Msg = " User Name Already exists";
                //        }
                return Msg;

            }
            catch (Exception Ex)
            {
                return Ex.Message;
            }

        }

        [HttpGet]
        public IEnumerable<TaskList> GetTaskList2(string UserId, int project)
        {
            List<TaskList> objResult = new List<TaskList>();
            try
            {
                SqlParameter[] Para = new SqlParameter[2];
                Para[0] = new SqlParameter("@AsignTo", clsMain.MyInt(UserId));
                Para[1] = new SqlParameter("@projct", clsMain.MyInt(project));
                DataTable dt = new ESCommon.SQLHELPER(ConnectionString).getTable("dbo.[SP_GetTaskHistory2]", Para);


                if (dt != null && dt.Rows.Count > 0)
                {

                    foreach (DataRow item in dt.Rows)
                    {
                        TaskList row = new TaskList();
                        row.Taskid = clsMain.MyString(item["TaskId"]);
                        row.ID = clsMain.MyInt(item["id"]);
                        row.ItemTypen = clsMain.MyString(item["ItemTypen"]);
                        row.Project = clsMain.MyString(item["ProjectName"]);
                        row.Client = clsMain.MyString(item["clientName"]);
                        row.Task = clsMain.MyString(item["TaskName"]);
                        row.AssignDate = Convert.ToDateTime(item["AssignDate"]).ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);
                        row.AssignTo = clsMain.MyString(item["UserName"]);
                        row.EstDate = Convert.ToDateTime(item["EstimatedDate"]).ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);
                        row.CompletionDate = Convert.ToDateTime(item["CompeletionDate"]).ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);
                        row.Statusn = clsMain.MyString(item["statusn"]);
                        row.check_in = clsMain.MyInt(item["check_in"]);
                        row.Priorityn = clsMain.MyString(item["priorityn"]);
                        row.TaskDesc = clsMain.MyString(item["TaskDesc"]);
                        row.Test = clsMain.MyDouble(item["Test"]);
                        objResult.Add(row);
                    }
                }
                else
                {


                }
                return objResult;
            }
            catch (Exception Ex)
            {
                return objResult;
            }

        }

        [HttpGet]
        public IEnumerable<ManageTask> UserTaskStatus(DateTime FromDate, DateTime ToDate, int AssignTo)
        {
            List<ManageTask> objResult = new List<ManageTask>();
            try
            {
                SqlParameter[] Para = new SqlParameter[3];
                Para[0] = new SqlParameter("@AsignTo", clsMain.MyInt(AssignTo)); //ESCommon.clsMain.MyPrintDate(ESCommon.clsMain.MyDate(objTask.VchDate), "yyyy-MM-dd"));//clsMain.MyDate(objPaymentEntry.VchDate));
                Para[1] = new SqlParameter("@FromDate", ESCommon.clsMain.MyPrintDate(ESCommon.clsMain.MyDate(FromDate), "yyyy-MM-dd"));//clsMain.MyString(FromDate));
                Para[2] = new SqlParameter("@ToDate", ESCommon.clsMain.MyPrintDate(ESCommon.clsMain.MyDate(ToDate), "yyyy-MM-dd"));//clsMain.MyString(ToDate));



                DataTable dt = new ESCommon.SQLHELPER(ConnectionString).getTable("SP_UserTaskDetail", Para);


                if (dt != null && dt.Rows.Count > 0)
                {
                    objResult = dt.AsEnumerable()
                            .Select(row => new ManageTask()
                            {
                                //TaskId = clsMain.MyString(row.Field<string>("TaskId")),

                                TaskId = clsMain.MyString(row.Field<string>("TaskId")),
                                ProjectName = clsMain.MyString(row.Field<string>("ProjectName")),
                                ClientName = clsMain.MyString(row.Field<string>("ClientName")),
                                UserName = clsMain.MyString(row.Field<string>("UserName")),
                                Discription = clsMain.MyString(row.Field<string>("taskdesc")),
                                EstTime = clsMain.MyDouble(row.Field<double>("ExpHours")),
                                TotalTimeTaken = clsMain.MyString(row.Field<string>("finalTimeTaken")),


                            }).ToList();
                }
                else
                {


                }
                return objResult;
            }
            catch (Exception Ex)
            {
                return objResult;
            }

        }






















    }
}
