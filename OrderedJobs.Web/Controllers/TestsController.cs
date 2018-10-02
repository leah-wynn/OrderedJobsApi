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
        public async Task<List<string>> GetAllTestsAsync()
        {
            return await Domain.OrderedJobs.GetAllTests();
        }
            
        [HttpPost]
        public void AddTestAsync([FromBody] string test)
        {
             Domain.OrderedJobs.AddTest(test);
        }
        
        [HttpDelete]
        public async Task<bool> DeleteTestsAsync()
        {
            return await Domain.OrderedJobs.DeleteTest();
        }
    }
}