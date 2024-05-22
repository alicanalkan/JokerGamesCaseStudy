using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JokerGames
{
    public interface IGamePanel
    {
        void UpdateApple(int count);
        void UpdatePear(int count);
        void UpdateStrawberry(int count);
    }
}
