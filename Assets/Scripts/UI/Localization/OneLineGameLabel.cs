namespace Localization
{
    public class OneLineGameLabel : TranslatingLabel
    {
        private const string GameNameOneLine = nameof(GameNameOneLine);

        protected override string GetPhraseCode() => GameNameOneLine;
    }
}