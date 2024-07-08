public class GameLabel : TranslatingLabel
{
    private const string GameName = nameof(GameName);

    protected override string GetText() => GetTranslation(GameName);
}