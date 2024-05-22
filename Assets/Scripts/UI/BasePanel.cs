using DG.Tweening;
using JokerGames.AssetManagement;
using JokerGames.Managers;
using UnityEngine;

namespace JokerGames.UI
{
    public class BasePanel : MonoView, IPanel
    {
        private PanelDefinition _panelDefinition;
        public UIManager uiManager;
        public virtual void Initialize(UIManager givenUIManager, PanelDefinition panelDefinition)
        {
            uiManager = givenUIManager;
            _panelDefinition = panelDefinition;

            Show();
        }

        public virtual void InitFromDefinition(PanelDefinition panelDefinition)
        {
            _panelDefinition = panelDefinition;

            Show();
        }

        public virtual void Show()
        {
            transform.localScale = Vector3.zero;
            gameObject.SetActive(true);
            transform.DOScale(Vector2.one, .25f);
        }

        public virtual void Confirm()
        {
            transform.DOScale(Vector2.zero, .15f).OnComplete(() =>
            {
                uiManager.ClosePanel(this);
                _panelDefinition.ConfirmCallback?.Invoke();
            });
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}