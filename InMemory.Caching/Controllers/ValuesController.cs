using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Net;

namespace InMemory.Caching.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        readonly IMemoryCache _memoryCache;

        public ValuesController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpGet("set/{name}")]
        public void SetName(string name)
        {
            _memoryCache.Set("name", name);
        }

        [HttpGet("get")]
        public string GetName()
        {
            if (_memoryCache.TryGetValue<string>("name", out string name))
                return name;
            else
                return String.Empty;
        }

        [HttpGet("setdate")]
        public void SetDate()
        {
            _memoryCache.Set<DateTime>("date", DateTime.UtcNow, options: new()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                SlidingExpiration = TimeSpan.FromSeconds(5)
            });
        }

        [HttpGet("getdate")]
        public DateTime GetDate()
        {
            if (_memoryCache.TryGetValue<DateTime>("date", out DateTime date))
                return date;
            else
                return DateTime.MinValue;
        }
    }
}
