using JokerGames.Data;
using UnityEditor;
using UnityEngine;

namespace JokerGames.Editor
{
    [CustomEditor(typeof(BoardData))]
    public class TileDataEditor : UnityEditor.Editor
    {
        private BoardData _boardData;
        
        private void OnEnable()
        {
            if (_boardData == null)
            {
                _boardData = target as BoardData;
            }
        }

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Create Board"))
            {
                _boardData.GenerateBoardData();
            }
            
            base.OnInspectorGUI();
        }
    }
}
