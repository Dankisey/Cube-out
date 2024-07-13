namespace Localization
{
    public class AuthorizeInstructionsLabel : TranslatingLabel
    {
        private const string AuthorizeInstructions = nameof(AuthorizeInstructions);

        protected override string GetText() => GetTranslation(AuthorizeInstructions);
    }
}