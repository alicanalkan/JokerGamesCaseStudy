using JokerGames.AssetManagement;
using UnityEngine;

namespace JokerGames.Components
{
    /// <summary>
    /// Mono Entities
    /// </summary>
    public abstract class Entity : MonoBehaviour , IAddressableTag
    {
        public Vector3 Position { get; set; }
        public string Label { get; set; }
    }
}
