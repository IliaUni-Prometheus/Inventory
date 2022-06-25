using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Shared.Extensions;

namespace Inventory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
        private const string weatherListCacheKey = "weatherList";
        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);
        private readonly ILogger<WeatherForecastController> _logger;
        private IMemoryCache _cache;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMemoryCache cache)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache)); ;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        [ProducesResponseType(200, Type = typeof(WeatherForecast))]
        [ProducesResponseType(404)]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.Log(LogLevel.Information, "Trying to fetch the list of weather from cache.");

            if (_cache.TryGetValue(weatherListCacheKey, out IEnumerable<WeatherForecast> weatherlist))
            {
                _logger.Log(LogLevel.Information, "Employee list found in cache.");
            }
            else
            {
                _logger.Log(LogLevel.Information, "Employee list not found in cache. Fetching from database.");
                weatherlist = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                })
             .ToList();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                        .SetPriority(CacheItemPriority.Normal)
                        .SetSize(1024);
                _cache.Set(weatherListCacheKey, weatherlist, cacheEntryOptions);
            }

            return weatherlist;
        }
        [HttpGet("concurent")]
        public async Task<IEnumerable<WeatherForecast>> GetWeatherForecastConcurrent()
        {
            _logger.Log(LogLevel.Information, "Trying to fetch the list of weather from cache.");

            if (_cache.TryGetValue(weatherListCacheKey, out IEnumerable<WeatherForecast> weatherlist))
            {
                _logger.Log(LogLevel.Information, "Employee list found in cache.");
            }
            else
            {
                try
                {
                    await semaphore.WaitAsync();
                    if (_cache.TryGetValue(weatherListCacheKey, out weatherlist))
                    {
                        _logger.Log(LogLevel.Information, "Employee list found in cache.");
                    }
                    else
                    {
                        _logger.Log(LogLevel.Information, "Employee list not found in cache. Fetching from database.");
                        weatherlist = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                        {
                            Date = DateTime.Now.AddDays(index),
                            TemperatureC = Random.Shared.Next(-20, 55),
                            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                        })
                     .ToList();

                        var cacheEntryOptions = new MemoryCacheEntryOptions()
                                .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                                .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                                .SetPriority(CacheItemPriority.Normal)
                                .SetSize(1024);
                        _cache.Set(weatherListCacheKey, weatherlist, cacheEntryOptions);
                    }
                }
                finally
                {
                    semaphore.Release();
                }
            }
            return weatherlist;
        }

        [HttpGet("GetCache")]
        [ResponseCache(CacheProfileName = "Cache2Mins")]
        public IActionResult GetCache()
        {
            return Ok($"Response was generated at {DateTime.Now}");
        }
    }
}