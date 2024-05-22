using JokerGames.Managers;

namespace JokerGames.UI
{
    internal interface IButton
    {
        /// <summary>
        /// Initialize Popup
        /// </summary>
        /// <param name="uiManager">Main UI Manager</param>
        /// <param name="buttonDefinition">Panel Definition</param>
        public void Initialize(UIManager uiManager, ButtonDefinition buttonDefinition);
        
        
        /// <summary>
        /// Inits Popup from Given Definition
        /// </summary>
        /// <param name="buttonDefinition">Panel Definition</param>
        public void InitFromDefinition(ButtonDefinition buttonDefinition);

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