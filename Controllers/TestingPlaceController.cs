
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebTalkApi.Entities;

namespace WebTalkApi.Controllers
{
    [Route("test")]
    [ApiController]
    public class TestingPlaceController: ControllerBase
    {
        private readonly WebTalkDbContext _dbContext;

        public TestingPlaceController(WebTalkDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        
    }
}
