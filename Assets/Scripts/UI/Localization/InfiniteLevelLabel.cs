namespace Localization
{
    public class InfiniteLevelLabel : TranslatingLabel
    {
        private const string InfiniteLevel = nameof(InfiniteLevel);

        protected override string GetPhraseCode() => InfiniteLevel;
    }
}