using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Models;
using MovieApi.CsvData;

namespace MovieApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MetadataController : ControllerBase
    {
        private readonly CsvReader _csvReader;
        private static List<Metadata> _database;

        public MetadataController(CsvReader csvReader)
        {
            _csvReader = csvReader;
            if (_database == null)
                _database = new List<Metadata>();
        }

        // GET: metadata/3
        [HttpGet("{movieId}")]
        public ActionResult<IEnumerable<MetadataVm>> Get(long movieId)
        {
            var items = _csvReader.GetMetadata();
            var metadataList = items.FindAll(x => x.MovieId == movieId);
            List<MetadataVm> sortedList = metadataList.OrderBy(x => x.Language).ToList();
            return sortedList;
        }

        // POST: metadata
        [HttpPost]
        public ActionResult<MetadataVm> Post(Metadata metadata)
        {
            var count = _database.Count;
            metadata.Id = count + 1;
            _database.Add(metadata);
            MetadataVm metadataVm = new MetadataVm()
            {
                MovieId = metadata.MovieId,
                Title = metadata.Title,
                Language = metadata.Language,
                Duration = metadata.Duration,
                ReleaseYear = metadata.ReleaseYear,
            };
            return metadataVm;
        }
    }
}
