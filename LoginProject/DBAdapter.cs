using System.Collections.Generic;

namespace LoginProject
{
    public static class DbAdapter
    {
        public static List<User> Users { get; set; }

        static DbAdapter()
        {
            Users = new List<User>
            {
                new User("sergiy", "password"),
                new User("sergiy2", "password2")
            };
        }
    }
}
