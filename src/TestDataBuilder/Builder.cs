namespace TestDataBuilder
{
    public static class Builder
    {
        public static TestUser CreateUser()
        {
            return new TestUser();
        }

        public static TestRepos GetListOfRepos(int numOfRepos)
        {
            return new TestRepos(numOfRepos);
        }
    }
}
