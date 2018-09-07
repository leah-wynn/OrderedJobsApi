using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderedJobs.Domain
{
    public class Job
    {
        private readonly string _detail;

        public Job(string detail)
        {
            _detail = detail;
            Name = detail[0].ToString();
            if (HasDependancy())
            {
                Dependancy = detail[3].ToString();
            }
        }

        public string Name { get; }
        public string Dependancy { get; }

        public bool HasNoDependancy()
        {
            return _detail.Length == 3;
        }

        public bool HasDependancy()
        {
            return _detail.Length == 4;
        }

        public bool JobNotPresent(List<string> orderedJobs)
        {
            return orderedJobs.None(x => x == Name);
        }

        public bool DependancyIsPresent(List<string> orderedJobs)
        {
            return orderedJobs.Contains(Dependancy);
        }
    }

    public static class EnumerableExtentions
    {
        public static bool None<TSource>(this IEnumerable<TSource> enumerable, Func<TSource, bool> predicate )
        {
            return !enumerable.Any(predicate);
        }

    }
}