using JokerGames.Managers;

namespace JokerGames.UI
{
    internal interface IPanel
    {
        /// <summary>
        /// Initialize Popup
        /// </summary>
        /// <param name="uiManager">Main UI Manager</param>
        /// <param name="panelDefinition">Panel Definition</param>
        public void Initialize(UIManager uiManager, PanelDefinition panelDefinition);
        
        
        /// <summary>
        /// Inits Popup from Given Definition
        /// </summary>
        /// <param name="panelDefinition">Panel Definition</param>
        public void InitFromDefinition(PanelDefinition panelDefinition);

        /// <summary>
        /// Shows Popup
        /// </summary>
        public void Show();
        
        /// <summary>
        /// Confirms Popup
        /// </summary>
        public void Confirm();
        
        /// <summary>
        /// Hides Popup
        /// </summary>
        public void Hide();
    }
}