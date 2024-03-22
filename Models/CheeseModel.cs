namespace Toast_and_Taste.Models
{
    public class CheeseModel
    {
        public int Id { get; set; }
        public string Kind { get; set; }
        public string WinePair { get; set; }
    }

    public class CheeseFavoriteModel
    {
        public int Id { get; set; }
        public string Kind { get; set; }
        public string WinePair { get; set; }

        public Boolean IsFavorite { get; set; }
    }
}
