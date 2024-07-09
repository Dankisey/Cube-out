namespace LeaderBoard
{
    public record LeaderboardPlayer(string ImageURL, string Name, int Score, int Rank)
    {
        public string ImageURL { get; private set; } = ImageURL;
        public string Name { get; private set; } = Name;
        public int Score { get; private set; } = Score;
        public int Rank { get; private set; } = Rank;
    }
}