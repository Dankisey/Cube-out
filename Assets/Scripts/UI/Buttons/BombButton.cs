﻿using UnityEngine;
using Game.Level;

namespace Game.UI.Buttons
{
    public class BombButton : CustomButton
    {
        [SerializeField] private InputHandler _input;
        
        protected override void OnButtonClick()
        {
            _input.ActivateBombNavigating();
        }
    }
}