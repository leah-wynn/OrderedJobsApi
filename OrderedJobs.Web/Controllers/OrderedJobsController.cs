using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OrderedJobs.Web.Controllers
{
    [Route("orderedjobs")]
    public class OrderedJobsController : ControllerBase
    {
        [HttpGet("{dependencies}")]
        public async Task<string> OrderTheJobsAsync(string dependencies)
        {
            return await Domain.OrderedJobs.OrderTheJobs(dependencies);
        }
    }
}