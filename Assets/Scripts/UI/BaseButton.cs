using DG.Tweening;
using JokerGames.AssetManagement;
using JokerGames.Managers;
using UnityEngine;

namespace JokerGames.UI
{
    public class BaseButton : MonoView, IButton
    {
        private ButtonDefinition _buttonDefinition;
        public UIManager uiManager;
        public virtual void Initialize(UIManager givenUIManager, ButtonDefinition buttonDefinition)
        {
            uiManager = givenUIManager;
            _buttonDefinition = buttonDefinition;

            Show();
        }

        public virtual void InitFromDefinition(ButtonDefinition buttonDefinition)
        {
            _buttonDefinition = buttonDefinition;

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
                uiManager.CloseButton(this);
                _buttonDefinition.ConfirmCallback?.Invoke();
            });
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
