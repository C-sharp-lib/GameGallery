namespace GameGallery.Models
{
    public class GameGenres : IHelperFunctions
    {
        public int GenreId { get; set; }
        public Genres Genres { get; set; }
        public int GameId { get; set; }
        public Games Games { get; set; }

        public string TruncateWords(string text, int wordCount = 20)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            var words = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (words.Length <= wordCount) return text;
            return string.Join(" ", words.Take(wordCount)) + "...";
        }
    }

}
