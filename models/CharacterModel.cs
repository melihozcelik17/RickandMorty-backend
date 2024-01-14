public class CharacterModel
{
    public int id { get; set; }
    public string name { get; set; }
    public StatusEnum status { get; set; }
    public SpeciesEnum species { get; set; }
    public string type { get; set; }
    public GenderEnum gender { get; set; }
    public Location origin { get; set; }
    public Location location { get; set; }
    public string image { get; set; }
    public List<string> episode { get; set; }
    public string url { get; set; }
    public string created { get; set; }

}

