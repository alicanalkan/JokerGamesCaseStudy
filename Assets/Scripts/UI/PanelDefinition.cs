using System;
using JokerGames.AssetManagement;

namespace JokerGames.UI
{
    public class PanelDefinition
    {
        public ViewName viewName;

        public Action ConfirmCallback;

        public static PanelDefinition Create(ViewName givenViewName)
        {
            return new PanelDefinition(givenViewName);
        }
        protected PanelDefinition(ViewName givenViewName)
        {
            viewName = givenViewName;
        }

        public void SetCallbacks(Action confirmCallback, Action closeCallback = null)
        {
            ConfirmCallback = confirmCallback;
        }
    }
}