using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using OrderedJobs.Data;

namespace OrderedJobs.Domain
{
    public class OrderedJobs
    {

        public static Task<List<string>> GetAllTests()
        {
            return new DataFunctions().GetTests();
        }

        public static void AddTest(string test)
        {
            new DataFunctions().AddTest(test);
        }

        public static Task<bool> DeleteTest()
        {
            return Task.FromResult(new DataFunctions().DeleteTests());
        }

        public static Task<string> OrderTheJobs(string dependencies)
        {
            var orderedJobs = OrderJobs(dependencies);
            return Task.FromResult(orderedJobs);
        }
        
        public static string OrderJobs(string input)
        {
            if (input == "") return "";
            
            var jobInstructions = input.Replace(" ", "").Split("|").Select(x => new Job(x)).ToList();
            var jobSchedule = new List<string>();

            var wereJobsAdded = true;
            while (wereJobsAdded)
            {
                wereJobsAdded = false;
                foreach (var job in jobInstructions)
                {
                    if (job.HasNoDependancy() && job.JobNotPresent(jobSchedule))
                    {
                        jobSchedule.Add(job.Name);
                        wereJobsAdded = true;
                    }
                    
                    if (job.HasDependancy() && job.DependancyIsPresent(jobSchedule) && job.JobNotPresent(jobSchedule))
                    {
                        jobSchedule.Add(job.Name);
                        wereJobsAdded = true;
                    }
                }

            }

            var jobList = GetJobList(input);
            var orderedJobs = string.Join("", jobSchedule);
            return jobList.Length != orderedJobs.Length ? "circular dependancy not allowed" : orderedJobs;
        }

        private static string GetJobList(string input)
        {
            var jobs = input.Where(char.IsLetter);
            var jobList = string.Join("", jobs.Distinct());
            return jobList;
        }

        public static Task<string> TestJobsOrdering(string url)
        {
            var client = new HttpClient();
            var allTests = client.GetAsync(url).Result.Content.ReadAsStringAsync().Result;
            return Task.FromResult(allTests);
        }
    }
}