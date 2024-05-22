using UnityEngine;

namespace JokerGames.AssetManagement
{
    public class MonoView : MonoBehaviour
    {
        private Transform _tm;

        public virtual void Initialize()
        {
            _tm = GetComponent<Transform>();
        }

        public Transform Transform => _tm;

        public Vector3 Position
        {
            set => _tm.position = value;
            get => _tm.position;
        }

        public Vector3 LocalPosition
        {
            set => _tm.localPosition = value;
            get => _tm.localPosition;
        }

        public Vector3 LocalScale
        {
            set => _tm.localScale = value;
            get => _tm.localScale;
        }
    }
}