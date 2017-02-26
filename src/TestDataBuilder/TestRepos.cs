using System.Collections.Generic;

namespace TestDataBuilder
{
    public class TestRepos
    {
        private int _numOfRepos;

        public TestRepos(int numOfRepos)
        {
            _numOfRepos = numOfRepos;
        }

        public IEnumerable<Models.Repo> Build()
        {
            var repos = new List<Models.Repo>();

            for (int i = 0; i < _numOfRepos; i++)
            {
                var repo = new Models.Repo()
                {
                    Name = $"Name{i}",
                    StarGazers_Count = i
                };
                repos.Add(repo);
            }

            return repos;
        }
    }
}
