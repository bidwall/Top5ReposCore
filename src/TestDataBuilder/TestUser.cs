using Models;

namespace TestDataBuilder
{
    public class TestUser
    {
        public User Build()
        {
            var user = new User()
            {
                Name = "Name1"
            };

            return user;
        }
    }
}
