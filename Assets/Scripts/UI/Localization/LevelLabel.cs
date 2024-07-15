using UnityEngine.SceneManagement;

namespace Localization
{
    public class LevelLabel : TranslatingLabel
    {
        private const string Level = nameof(Level);

        protected override string GetPhraseCode() => Level;

        protected override void OnTextSetted() => AddText($" {GetLevelNumber()}");

        private int GetLevelNumber()
        {
            int levelNumber = 0;

            string sceneName = SceneManager.GetActiveScene().name;
            string[] sceneNameParts = sceneName.Split(' ');

            foreach (string sceneNamePart in sceneNameParts)
            {
                if (int.TryParse(sceneNamePart, out levelNumber))
                    return levelNumber;
            }

            return levelNumber;
        }
    }
}