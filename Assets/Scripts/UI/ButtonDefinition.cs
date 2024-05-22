using System;
using JokerGames.AssetManagement;

namespace JokerGames.UI
{
    public class ButtonDefinition
    {
        public ViewName viewName;

        public Action ConfirmCallback;

        public static ButtonDefinition Create(ViewName givenViewName)
        {
            return new ButtonDefinition(givenViewName);
        }
        protected ButtonDefinition(ViewName givenViewName)
        {
            viewName = givenViewName;
        }

        public void SetCallbacks(Action confirmCallback, Action closeCallback = null)
        {
            ConfirmCallback = confirmCallback;
        }
    }
}
