namespace GameGallery.Models
{
    public class GameReviews
    {
        public int GameId { get; set; }
        public int UserId { get; set; }
        public int ReviewId { get; set; }
        public Users Users { get; set; }
        public Games Games { get; set; }

        public Reviews Reviews { get; set; }

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
