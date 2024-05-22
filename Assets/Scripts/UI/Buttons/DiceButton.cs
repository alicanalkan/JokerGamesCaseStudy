using System;
using DG.Tweening;
using JokerGames.AssetManagement;
using JokerGames.Managers;
using UnityEngine;

namespace JokerGames.UI.Buttons
{
    public class DiceButton : BaseButton
    {
        private DiceButtonDefinition _diceButtonDefinition;

        public Action Callback;
        public class DiceButtonDefinition : ButtonDefinition
        {
            public Action _callback;
            public  DiceButtonDefinition(Action callback) : base(ViewName.DiceButton)
            {
                _callback = callback;
            }
        }
        
        /// <summary>
        /// Init firs time
        /// </summary>
        /// <param name="givenUIManager"></param>
        /// <param name="buttonDefinition"></param>
        public override void Initialize(UIManager givenUIManager, ButtonDefinition buttonDefinition)
        {
            base.Initialize(givenUIManager, buttonDefinition);
            _diceButtonDefinition = buttonDefinition as DiceButtonDefinition;

            Callback = _diceButtonDefinition._callback;
        }
        
        /// <summary>
        /// Load Existing Button And Update
        /// </summary>
        /// <param name="buttonDefinition"></param>
        public override void InitFromDefinition(ButtonDefinition buttonDefinition)
        {
            base.InitFromDefinition(buttonDefinition);
            _diceButtonDefinition = buttonDefinition as DiceButtonDefinition;
            Callback = _diceButtonDefinition._callback;
        }
       
        /// <summary>
        /// Dice Button
        /// </summary>
        public override void Confirm()
        {
            Callback?.Invoke();
            if(!DOTween.IsTweening(transform))
                transform.DOShakeRotation(0.5f, 10,10,50,false,ShakeRandomnessMode.Harmonic);
            // base.Confirm();
        }
    }
}
