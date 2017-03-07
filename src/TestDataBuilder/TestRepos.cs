using System.Collections.Generic;
using Models;

namespace TestDataBuilder
{
    public class TestRepos
    {
        private readonly int _numOfRepos;

        public TestRepos(int numOfRepos)
        {
            _numOfRepos = numOfRepos;
        }

        public IEnumerable<Repo> Build()
        {
            var repos = new List<Repo>();

            for (var i = 0; i < _numOfRepos; i++)
            {
                var repo = new Repo()
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
