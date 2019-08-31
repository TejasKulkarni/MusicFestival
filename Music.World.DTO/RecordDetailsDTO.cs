using System;
using System.Collections.Generic;
using System.Text;

namespace Music.World.DTO
{
    public class RecordDetailsDTO
    {
        public string RecordName { get; set; }

        public List<Band> Bands { get; set; }
    }

    public class Band
    {
        public string Name { get; set; }

        public List<string> Festivals { get; set; }
    }
}
