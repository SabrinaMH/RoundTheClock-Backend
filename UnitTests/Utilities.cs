using RoundTheClock.Core.Model;

namespace RoundTheClock.UnitTests
{
    class Utilities
    {
        public static bool AreEqualByJson(object obj1, object obj2)
        {
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return serializer.Serialize(obj1) == serializer.Serialize(obj2);
        }

        public static bool AreProjectsEqual(Project p1, Project p2)
        {
            return p1.Name == p2.Name && p1.Customer.Name == p2.Customer.Name;
        }

        public static bool AreTasksEqual(Task t1, Task t2)
        {
            return t1.Name == t2.Name && t1.ProjectId == t2.ProjectId;
        }
    }
}
