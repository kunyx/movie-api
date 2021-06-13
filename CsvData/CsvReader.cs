using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using ChoETL;
using MovieApi.Models;

namespace MovieApi.CsvData
{
    public class CsvReader : ICsvReader
    {
        public CsvReader()
        { }

        public List<MetadataVm> GetMetadata()
        {
            string dirName = "CsvData";
            string fileName = "metadata.csv";
            List<MetadataVm> items = new List<MetadataVm>();
            try
            {
                string path2csv = Path.Combine(Environment.CurrentDirectory, dirName, fileName);
                var csvMetadata = new ChoCSVReader<Metadata>(path2csv).WithFirstLineHeader();
                foreach (var metadata in csvMetadata)
                {
                    MetadataVm vm = new MetadataVm()
                    {
                        MovieId = metadata.MovieId,
                        Title = metadata.Title,
                        Language = metadata.Language,
                        Duration = metadata.Duration,
                        ReleaseYear = metadata.ReleaseYear,
                    };
                    items.Add(vm);
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            return items;
        }

        public List<Stats> GetStats()
        {
            string dirName = "CsvData";
            string fileName = "stats.csv";
            List<Stats> items = new List<Stats>();
            try
            {
                string path2csv = Path.Combine(Environment.CurrentDirectory, dirName, fileName);
                var csvStats = new ChoCSVReader<Stats>(path2csv).WithFirstLineHeader();
                foreach (var e in csvStats)
                    items.Add(e);
            }
            catch (Exception ex)
            {
                _ = ex.Message;
            }
            return items;
        }
    }
}
