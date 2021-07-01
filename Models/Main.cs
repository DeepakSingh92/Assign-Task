using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Assign_Task.Models
{
    public class Main
    {
    }

    public class Login
    {
        public int UserCode;
        public string UserName;
        public int UserType;

    }

    public class User
    {

        public string Name = "";
        public string Code = "";
        public Int32 UserType = 0;
        public string Pwd = "";

    }
    public class UserName
    {
        public int Code { get; set; }
        public String Desc { get; set; }
    }

    public class ReferenceEntry
    {
        public int ID { get; set; }
        public string ReferenceName { get; set; }
        public string PhoneNo { get; set; }
        public string ContactPerson { get; set; }
        public string EmailId { get; set; }
        //public string Password { get; set; }
    }

    public class Prioritylist
    {
        public int ID { get; set; }
        public string priority1 { get; set; }
    }
    public class ClientEntry
    {
        public int ID { get; set; }
        public string ClientName { get; set; }
        public string PhoneNo { get; set; }
        public string ContactPerson { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string ReferenceID { get; set; }
        public string ReferenceName { get; set; }


    }

    public class ProjectEntry
    {
        public int ID { get; set; }
        public string ProjectName { get; set; }
        public string ProjectLeader { get; set; }

        public int Client { get; set; }
        public string ClientName { get; set; }
        public string Status { get; set; }
        public string ContactPerson { get; set; }
        public string PhoneNo { get; set; }

        public int ProjectValue { get; set; }
        public int Progress { get; set; }
        public int ProjectAdvance { get; set; }
        public int AssignTo { get; set; }
        public string ProjectDescription { get; set; }
        public string ProjectManagerName { get; set; }

        public int ProjectManager { get; set; }
        public string Priorityn { get; set; }
        public string ReferenceName { get; set; }
        public int Priority { get; set; }
        public string EstimatedTargetDate { get; set; }
        public string EstimatedStartDate { get; set; }
        public string ProjectCompletionDate { get; set; }
        public string TaskName { get; set; }
        public string TaskDate { get; set; }
    }

    public class SaveProjectMaster
    {
        public int ID { get; set; }
        public string ProjectName { get; set; }
        public int ProjectLeader { get; set; }
        public DateTime EstimatedTargetDate { get; set; }
        public int Priority { get; set; }
        public int ReferenceId { get; set; }
        public string ContactPerson { get; set; }
        public string PhoneNo { get; set; }
        public DateTime EstimatedStartDate { get; set; }
        public int ProjectManager { get; set; }
        public string ProjectDescription { get; set; }
        public int AssignTo { get; set; }
        public int ProjectValue { get; set; }
        public int ProjectAdvance { get; set; }
        public int Client { get; set; }
        public DateTime ProjectCompletionDate { get; set; }
        public List<assignmaster> AsslignList { get; set; }

    }

    public class assignmaster
    {

        public int Code { get; set; }
        public string Name { get; set; }

    }

    public class SavePriority
    {
        public int ID { get; set; }

        public int PriorityID { get; set; }

    }

    public class Summarytable
    {
        public string Project { get; set; }
        public int Task { get; set; }
        public int Progress { get; set; }
        public int completed { get; set; }
        public int remaining { get; set; }
        public int overdue { get; set; }

    }

    public class TaskList
    {
        public int ID { get; set; }
        public string Taskid { get; set; }
        public string Project { get; set; }
        public int Projectid { get; set; }
        public string Client { get; set; }
        public int Clientid { get; set; }
        public string Task { get; set; }
        public string AssignDate { get; set; }
        public string AssignTo { get; set; }
        public int AssignToId { get; set; }
        public double ActualTime { get; set; }
        public double EstTime { get; set; }
        public int Status { get; set; }
        public string Statusn { get; set; }

        public int ImpStatus { get; set; }
        public int Checkedby { get; set; }
        public string EstDate { get; set; }
        public string CompletionDate { get; set; }
        public string CompletedVer { get; set; }
        public string Unimple { get; set; }
        public string Setting { get; set; }
        public string Closing { get; set; }
        public string Teamleader { get; set; }
        public int check_in { get; set; }
        public int ItemType { get; set; }
        public string ItemTypen { get; set; }

        public int Priority { get; set; }
        public string Priorityn { get; set; }

        public string TaskDesc { get; set; }
        public int TaskPoint { get; set; }
        public double Test { get; set; }


    }

    public class totalCall
    {

        public int TotalCalls { get; set; }
        public int PendingCall { get; set; }
        public int TotalTask { get; set; }
        public int PendingTask { get; set; }
    }

    public class DashBoard
    {
        public string ProjectName { get; set; }
        public int ProId { get; set; }
        public int TotalProject { get; set; }
        public int TotalClient { get; set; }
        public int TotalTask { get; set; }
        public int TotalCompletedProject { get; set; }
        public int TotalCompletedTask { get; set; }
        public int TotalUser { get; set; }
        public int TotalBug { get; set; }
        public int TotalCompletedBug { get; set; }
        public int OpenTask { get; set; }
        public int UnassignedTask { get; set; }
        public int ProjmanId { get; set; }
        public int ProjleadId { get; set; }
    }

    public class ItemTypelist
    {
        public int ID { get; set; }
        public string ItemType { get; set; }
    }

    public class taskstatus
    {
        public int ID { get; set; }
        public string tstatus { get; set; }
    }

    public class ProjectName
    {
        public int Code { get; set; }
        public String Desc { get; set; }
    }


    public class TaskEntry
    {
        public int Project { get; set; }
        public int Client { get; set; }
        public string ProjectName { get; set; }
        public string ClientName { get; set; }
        public string Task { get; set; }
        public string AssignDate { get; set; }
        public int AssignTo { get; set; }
        public string ActualTime { get; set; }
        public string EstTime { get; set; }
        public int Status { get; set; }
        public int ItemType { get; set; }

        public int ImpStatus { get; set; }
        public int Checkedby { get; set; }
        public string EstDate { get; set; }
        public string CompletionDate { get; set; }
        public string CompletedVer { get; set; }
        public string Unimple { get; set; }
        public string Setting { get; set; }
        public string Closing { get; set; }
        public string username { get; set; }
        public string TeamLeader { get; set; }
        public int EmpCode { get; set; }
        public int TaskID { get; set; }
        public int ID { get; set; }
        public int TaskId { get; set; }
        public int CallID { get; set; }
        public DateTime checkinTime { get; set; }
        public string Createdby { get; set; }
        public string TotalTimeTaken { get; set; }
        public DateTime CreatedDate { get; set; }
        public double ExpectTime { get; set; }
        public double StatusPerc { get; set; }
        public int Priority { get; set; }
        public string TaskDesc { get; set; }
        public int TaskPoint { get; set; }
        public List<assignmaster> AsslignList { get; set; }

    }

    public class UserTypeController
    {
        public int UserCode { get; set; }
        public string UserType { get; set; }
    }


    public class UserEntry
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Mobile { get; set; }
        public string MailID { get; set; }
        public string Password { get; set; }
        public string ECPassword { get; set; }
        public int IsAdmin { get; set; }
        public int UserType { get; set; }
        public string Role { get; set; }
        public int Deactivate { get; set; }
        public int TeamLeader { get; set; }
        public string TaskName { get; set; }
        public string TaskDate { get; set; }
    }

    public class TaskInfo
    {
        public int Code { get; set; }
        public string Desc { get; set; }
        public string select { get; set; }
        public List<MasterInfo> Client { get; set; }
        public List<MasterInfo> AssignTo { get; set; }

    }

    public class MasterInfo
    {
        public int Code { get; set; }
        public string select { get; set; }
        public string Desc { get; set; }
    }

    public class SaveUserEntry
    {
        public string UserName { get; set; }
        public int Id { get; set; }
        public string Mobile { get; set; }
        public int UserType { get; set; }
        public string Password { get; set; }
        public string OldPassWord { get; set; }
        public int OrgId { get; set; }
        public string MailID { get; set; }
        public int PassID { get; set; }
        public int Deactivate { get; set; }
        public int TeamLeader { get; set; }


    }

    public class ReportType
    {
        public int Code { get; set; }
        public string Desc { get; set; }
    }

    public class ManageTaskNew
    {
        public string UserName { get; set; }
        public string ProjectName { get; set; }
        public int TotalProject { get; set; }
        public int TotalTask { get; set; }
        public int Progress { get; set; }
        public int FailTask { get; set; }
        public int SuccessTask { get; set; }
        public DateTime TaskDate { get; set; }

    }

    public class ManageTaskNew1
    {
        public string UserName { get; set; }
        public string ProjectName { get; set; }
        public int TotalProject { get; set; }
        public int TotalTask { get; set; }
        public int Progress { get; set; }
        public int FailTask { get; set; }
        public int SuccessTask { get; set; }

    }

    public class CheckInStatus
    {
        public int id { get; set; }
        public int EmpCode { get; set; }
        public int AssignTo { get; set; }
        public int CheckIn { get; set; }
        public int CheckOut { get; set; }
        public int TaskID { get; set; }
        public string CheckInTime { get; set; }
        public string CheckOutTime { get; set; }
    }

    public class ReasonStatus
    {
        public int StatusID { get; set; }
        public string Reason { get; set; }
        public int TaskId { get; set; }
        public int EmpId { get; set; }

    }



    public class ManageTask
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string ID { get; set; }
        public string Project { get; set; }
        public string ProjectName { get; set; }
        public string Discription { get; set; }
        public string UserName { get; set; }
        public string ClientName { get; set; }
        public int ProjectID { get; set; }
        public string Client { get; set; }
        public string Attachment { get; set; }
        public int ClientId { get; set; }
        public int check_in { get; set; }
        public string TaskId { get; set; }
        public DateTime AssignDate { get; set; }
        public string AssignTo { get; set; }
        public string ActualTime { get; set; }
        public double EstTime { get; set; }
        public string TotalTimeTaken { get; set; }
        public string Status { get; set; }
        public string ImpStatus { get; set; }
        public string Checkedby { get; set; }
        public DateTime EstDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime CompletionDate { get; set; }
        public string CompletedVer { get; set; }
        public string Unimple { get; set; }
        public string Setting { get; set; }
        public string Closing { get; set; }
    }




























}