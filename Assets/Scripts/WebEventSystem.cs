using UnityEngine.EventSystems;

namespace Game
{
    public class WebEventSystem : EventSystem
    {
        protected override void OnApplicationFocus(bool hasFocus) => base.OnApplicationFocus(true);
    }
}