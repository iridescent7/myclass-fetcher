using System;

namespace MyClass
{
    public static class MyClassApi
    {
        public static string BaseAddress { get; } = "https://myclass.apps.binus.ac.id";
        public static string Login { get; } = BaseAddress + "/Auth/Login";
        public static string GetViconSchedule { get; } = BaseAddress + "/Home/GetViconSchedule";
    }

    public class MyClassLoginData
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string URL { get; set; }
    }

    public class MyClassViconData
    {
        public string StartDate { get; set; }
        public string DisplayStartDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string SsrComponentDescription { get; set; }
        public string ClassCode { get; set; }
        public string Room { get; set; }
        public string Campus { get; set; }
        public string DeliveryMode { get; set; }
        public string CourseCode { get; set; }
        public string CourseTitleEn { get; set; }
        public string ClassType { get; set; }
        public int WeekSession { get; set; }
        public int CourseSessionNumber { get; set; }
        public string MeetingId { get; set; }
        public string MeetingPassword { get; set; }
        public string MeetingUrl { get; set; }
        public string UserFlag { get; set; }
        public string BinusianId { get; set; }
        public string PersonCode { get; set; }
        public string FullName { get; set; }
        public string AcademicCareer { get; set; }
        public string ClassMeetingId { get; set; }
        public string Location { get; set; }
        public string MeetingStartDate { get; set; }
        public string Id { get; set; }
    }
}
