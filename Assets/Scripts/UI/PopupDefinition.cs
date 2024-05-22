using System;
using JokerGames.AssetManagement;

namespace JokerGames.UI
{
    public class PopupDefinition
    {
        public ViewName viewName;

        public Action ConfirmCallback;
        public Action CloseCallback;

        public static PopupDefinition Create(ViewName givenViewName)
        {
            return new PopupDefinition(givenViewName);
        }
        protected PopupDefinition(ViewName givenViewName)
        {
            viewName = givenViewName;
        }

        public void SetCallbacks(Action confirmCallback, Action closeCallback = null)
        {
            ConfirmCallback = confirmCallback;
            CloseCallback = closeCallback;
        }
    }
}