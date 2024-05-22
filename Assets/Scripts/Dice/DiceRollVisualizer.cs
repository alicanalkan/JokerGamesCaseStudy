using System;
using System.Threading;
using System.Threading.Tasks;
using JokerGames.Components;
using UnityEngine;

namespace JokerGames.Dice
{
    public class DiceRollVisualizer : Entity
    {
        [SerializeField] private DiceAnimator _firstDice;
        [SerializeField] private DiceAnimator _secondDice;

        private int _diceRollAnimationEndedCount = 0;
        private int _diceValue = 0;
        



        private void DiceAnimationEndedHandler(int dice)
        {
            _diceRollAnimationEndedCount++;
            _diceValue += dice;
        }

        public async Task<int> VisualizeDiceRoll(uint firstDiceNumber, uint secondDiceNumber)
        {
            _diceValue = 0;
            
            _firstDice.StartDiceAnimation(firstDiceNumber);
            _secondDice.StartDiceAnimation(secondDiceNumber);
            
            await Utils.Utils.WaitUntil(IsDiceComplete);
            _diceRollAnimationEndedCount = 0;
            return _diceValue;
        }
        
        private bool IsDiceComplete()
        {
            return _diceRollAnimationEndedCount == 2;
        }
        
        /// <summary>
        /// Subscribe events
        /// </summary>
        private void OnEnable()
        {
            _firstDice.DiceAnimationEnded += DiceAnimationEndedHandler;
            _secondDice.DiceAnimationEnded += DiceAnimationEndedHandler;
        }
        
        /// <summary>
        /// UnSubscribe events
        /// </summary>
        private void OnDisable()
        {
            _firstDice.DiceAnimationEnded -= DiceAnimationEndedHandler;
            _secondDice.DiceAnimationEnded -= DiceAnimationEndedHandler;
        }
    }
}