namespace Localization
{
    public class NotAuthorizedLabel : TranslatingLabel
    {
        private const string NotAuthorized = nameof(NotAuthorized);

        protected override string GetPhraseCode() => NotAuthorized;
    }
}