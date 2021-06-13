using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieApi.CsvData;
using MovieApi.Models;

namespace MovieApi.Controllers
{
    [Route("movies/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        private readonly ICsvReader _csvReader;

        public StatsController(ICsvReader csvReader)
        {
            _csvReader = csvReader;
        }

        // GET: movies/stats
        [HttpGet]
        public ActionResult<IEnumerable<MovieStats>> Get()
        {
            var stats =_csvReader.GetStats();
            var metadataList = _csvReader.GetMetadata();
            var dWatches = new Dictionary<int, long>();
            var dAverageDurationS = new Dictionary<int, long>();
            foreach (var item in stats)
            {
                if (!dWatches.ContainsKey(item.MovieId))
                    dWatches.Add(item.MovieId, 1);
                else
                    dWatches[item.MovieId] += 1;
            }
            foreach (var item in stats)
            {
                var seconds = item.WatchDurationMs / 100;
                if (!dAverageDurationS.ContainsKey(item.MovieId))
                    dAverageDurationS.Add(item.MovieId, seconds);
                else
                    dAverageDurationS[item.MovieId] += seconds;
            }
            foreach (var item in dWatches)
            {
                dAverageDurationS[item.Key] = dAverageDurationS[item.Key] / dWatches[item.Key];
            }
            List<MovieStats> movieStatsList = new List<MovieStats>();
            foreach (var metadata in metadataList)
            {
                var movieStats = new MovieStats()
                {
                    MovieId = metadata.MovieId,
                    Title = metadata.Title,
                    AverageWatchDurationS = dAverageDurationS[metadata.MovieId],
                    Watches = dWatches[metadata.MovieId],
                    ReleaseYear = metadata.ReleaseYear
                };
                if (!movieStatsList.Any(x => x.MovieId == metadata.MovieId))
                {
                    movieStatsList.Add(movieStats);
                }
            }
            List<MovieStats> sortedList = movieStatsList.OrderByDescending(x => x.Watches).ThenByDescending(x => x.ReleaseYear).ToList();
            return sortedList;
        }
    }
}
