namespace studentCourse.AppMetaData.BaseRouter

{
    public partial class Router
    {
        public class CourseRouter : Router
        {
            private const string Prefix = Rule + "Course";
            public const string Add = Prefix + "/";
            public const string Delete = Prefix + "/{id}";
            public const string Update = Prefix + "/{id}";
            public const string GetAll = Prefix + "/";
            public const string GetById = Prefix + "/{id}";
        }
    }
}