namespace Localization
{
    public class AdSuggestionLabel : TranslatingLabel
    {
        private const string AdSuggestion = nameof(AdSuggestion);

        protected override string GetPhraseCode() => AdSuggestion;
    }
}