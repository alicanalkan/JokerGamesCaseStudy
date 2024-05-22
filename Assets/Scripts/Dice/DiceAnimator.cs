using System;
using System.Collections.Generic;
using UnityEngine;

namespace JokerGames.Dice
{
    public class DiceAnimator : MonoBehaviour
    {
        /// <summary>
        /// Animation Names
        /// </summary>
        [SerializeField] private string _oneFaillingAnimationTriggerName;
        [SerializeField] private string _twoFaillingAnimationTriggerName;
        [SerializeField] private string _threeFaillingAnimationTriggerName;
        [SerializeField] private string _fourFaillingAnimationTriggerName;
        [SerializeField] private string _fiveFaillingAnimationTriggerName;
        [SerializeField] private string _sixFaillingAnimationTriggerName;

        [SerializeField] private Animator _animator;

        private Dictionary<uint, string> _diceAimationsTriggersNames;


        public event Action<int> DiceAnimationEnded;
        
        public void Awake()
        {
            _diceAimationsTriggersNames = new Dictionary<uint, string>()
            {
                {1, _oneFaillingAnimationTriggerName},
                {2, _twoFaillingAnimationTriggerName},
                {3, _threeFaillingAnimationTriggerName},
                {4, _fourFaillingAnimationTriggerName},
                {5, _fiveFaillingAnimationTriggerName},
                {6, _sixFaillingAnimationTriggerName},
            };
        }

        /// <summary>
        /// Calling from animation clip
        /// </summary>
        /// <param name="dice"></param>
        public void AnimationEnd(int dice)
        {
            DiceAnimationEnded?.Invoke(dice);
            
            // Reset Dice
            _animator.SetTrigger("Idle");
        }
        public void StartDiceAnimation(uint PlayersThrowedNumber)
        {
            _diceAimationsTriggersNames.TryGetValue(PlayersThrowedNumber, out string animationTriggerName);

            if(animationTriggerName is not null)
                _animator.SetTrigger(animationTriggerName);
        }
        
    }
}