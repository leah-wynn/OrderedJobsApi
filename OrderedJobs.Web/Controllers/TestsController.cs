using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;


namespace OrderedJobs.Web.Controllers
{
    [Route("tests")]
    public class TestsController : ControllerBase
    {

        [HttpGet]
        public async Task<string> GetAllTestsAsync()
        {
            return await Domain.OrderedJobs.GetAllTests();
        }
        
        [HttpPost("{dependencies}")]
        public async Task<string> AddTestAsync(string test)
        {
            return await Domain.OrderedJobs.AddTest(test);
        }
        [HttpDelete]
        public async Task<string> DeleteTestsAsync()
        {
            return await Domain.OrderedJobs.DeleteTest();
        }
    }
}