using System;
using JokerGames.AssetManagement;
using JokerGames.Managers;
using TMPro;
using UnityEngine;


namespace JokerGames.UI.Popups
{
    public class DicePopup : BasePopup
    {
        private DicePopupDefinition _dicePopupDefinitionDefinition;

        private int _diceOneValue =1;
        private int _diceTwoValue = 1;
        
        [SerializeField] private TextMeshProUGUI diceOne;
        [SerializeField] private TextMeshProUGUI diceTwo;
        
        public class DicePopupDefinition : PopupDefinition
        {
            public Action<int, int> Callback;
            
            public DicePopupDefinition(Action<int,int> callback) : base(ViewName.DicePopup)
            {
                Callback = callback;
            }
        }

        /// <summary>
        /// Init firs time
        /// </summary>
        /// <param name="givenUIManager"></param>
        /// <param name="panelDefinition"></param>
        public override void Initialize(UIManager givenUIManager, PopupDefinition panelDefinition)
        {
            
            base.Initialize(givenUIManager, panelDefinition);
            _dicePopupDefinitionDefinition = panelDefinition as DicePopupDefinition;
            transform.localPosition = Vector3.zero;


        }
    
        /// <summary>
        /// Load Existing Popup And Update
        /// </summary>
        /// <param name="panelDefinition"></param>
        public override void InitFromDefinition(PopupDefinition panelDefinition)
        {
            base.InitFromDefinition(panelDefinition);
            _dicePopupDefinitionDefinition = panelDefinition as DicePopupDefinition;
            transform.localPosition = Vector3.zero;
        }

        public void IncreaseOne()
        {
            if (_diceOneValue < 6)
            {
                _diceOneValue++;
                diceOne.text = _diceOneValue.ToString();
            }
          
        }

        public void DecreaseOne()
        {
            if (_diceOneValue > 1)
            {
                
                _diceOneValue--;
                diceOne.text = _diceOneValue.ToString();
            }
           
        }

        public void IncreaseTwo()
        {
            if (_diceTwoValue < 6)
            {
                _diceTwoValue++;
                diceTwo.text = _diceTwoValue.ToString();
            }
            
        }

        public void DecreaseTwo()
        {
            if (_diceTwoValue > 1)
            {
                _diceTwoValue--;
                diceTwo.text = _diceTwoValue.ToString();
            }
            
        }

        public override void Confirm()
        {
            _dicePopupDefinitionDefinition.Callback?.Invoke(_diceOneValue,_diceTwoValue);
            base.Confirm();
        }
    }
}
