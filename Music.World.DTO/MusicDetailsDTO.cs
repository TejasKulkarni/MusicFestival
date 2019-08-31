namespace Music.World.DTO
{
    public class MusicDetailsDTO
    {
        public string Name { get; set; }

        public BandDetails[] Bands { get; set; }
    }

    public class BandDetails
    {
        public string Name { get; set; }

        public string RecordLabel { get; set; }
    }
}
