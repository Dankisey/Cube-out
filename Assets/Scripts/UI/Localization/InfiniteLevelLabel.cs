public class InfiniteLevelLabel : TranslatingLabel
{
    private const string Infinite = nameof(Infinite);
    private const string Level = nameof(Level);

    protected override string GetText()
    {
        string infinite = GetTranslation(Infinite);
        string level = GetTranslation(Level);

        return $"{infinite} {level}";
    }
}
