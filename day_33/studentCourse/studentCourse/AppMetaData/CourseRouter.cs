namespace studentCourse.AppMetaData.BaseRouter

{
    public partial class Router
    {
        public class CourseRouter : Router
        {
            private const string Prefix = Rule + "Course";
            public const string Main = Prefix + "/";
            public const string MainId = Prefix + "/{id}";
        }
    }
}