namespace TestDataBuilder
{
    public class TestUser
    {
        public Models.User Build()
        {
            Models.User user = new Models.User()
            {
                Name = "Name1"
            };
            return user;
        }
    }
}
