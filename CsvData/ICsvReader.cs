using System;
using System.Collections.Generic;
using MovieApi.Models;

namespace MovieApi.CsvData
{
    public interface ICsvReader
    {
        public List<MetadataVm> GetMetadata();
        public List<Stats> GetStats();
    }
}
