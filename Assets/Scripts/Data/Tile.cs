using JokerGames.Components;
using TMPro;
using UnityEngine;

namespace JokerGames.Data
{
    public class Tile : Entity
    {
        [SerializeField] private TextMeshProUGUI apple;
        [SerializeField] private TextMeshProUGUI pear;
        [SerializeField] private TextMeshProUGUI strawberrie;

        public int Apple;
        public int Pear;
        public int Strawberrie;

        public void SetApple(int count)
        {
            Apple = count;
            apple.text = Apple.ToString();
        }

        public void SetPear(int count)
        {
            Pear = count;
            pear.text = Pear.ToString();
        }

        public void SetStrawberrie(int count)
        {
            Strawberrie = count;
            strawberrie.text = Strawberrie.ToString();
        }
    }
}
