using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;
using JokerGames.AssetManagement;
using JokerGames.Engine;
using JokerGames.UI;
using UnityEngine.UI;

namespace JokerGames.Managers
{
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private Image popupCanvasImage;
        [SerializeField] private CanvasGroup popupCanvasGroup;
        [SerializeField] private Canvas uiCanvas;

        private readonly Dictionary<ViewName, IPopup> _popups = new Dictionary<ViewName, IPopup>();
        private readonly Dictionary<ViewName, IButton> _buttons = new Dictionary<ViewName, IButton>();
        private readonly Dictionary<ViewName, IPanel> _panels = new Dictionary<ViewName, IPanel>();

        public IGamePanel GamePanel;
        
        /// <summary>
        /// Creates Panel by the Given Panel Definition
        /// </summary>
        /// <param name="panelDefinition"></param>
        public async Task CreatePanel(PanelDefinition panelDefinition)
        {   
            var loadedPanel = await UILoader.Instance.LoadMonoView<BasePanel>(panelDefinition.viewName,uiCanvas.transform);
            
            loadedPanel.gameObject.SetActive(false);

            var Ipanel = loadedPanel.GetComponent<IPanel>();
        
            Ipanel.Initialize(this, panelDefinition);

            _panels.Add(panelDefinition.viewName ,Ipanel);
            
            GamePanel = loadedPanel.GetComponent<IGamePanel>();
        }

        /// <summary>
        /// Shows Popup by the Given Panel Definition.Loads Popups from Addressable or Loads from Panels Dictionary
        /// </summary>
        /// <param name="panelDefinition"></param>
        public async Task ShowPanel(PanelDefinition panelDefinition)
        {
            if (_panels.TryGetValue(panelDefinition.viewName, out var panel))
            {
                panel.InitFromDefinition(panelDefinition);
            }
            else
            {
                var loadedButton = await UILoader.Instance.LoadMonoView<BasePanel>(panelDefinition.viewName,uiCanvas.transform);
                
                var Ipanel = loadedButton.GetComponent<IPanel>();
                
                Ipanel.Initialize(this, panelDefinition);

                _panels.Add(panelDefinition.viewName ,Ipanel);
            }
        }
        /// <summary>
        /// Creates Button by the Given Button Definition
        /// </summary>
        /// <param name="buttonDefinition"></param>
        public async Task CreateButton(ButtonDefinition buttonDefinition)
        {   
            var loadedButton = await UILoader.Instance.LoadMonoView<BaseButton>(buttonDefinition.viewName,uiCanvas.transform);
            
            loadedButton.gameObject.SetActive(false);

            var Ibutton = loadedButton.GetComponent<IButton>();
        
            Ibutton.Initialize(this, buttonDefinition);

            _buttons.Add(buttonDefinition.viewName ,Ibutton);
        }

        /// <summary>
        /// Shows Popup by the Given Button Definition.Loads Popups from Addressable or Loads from Buttons Dictionary
        /// </summary>
        /// <param name="buttonDefinition"></param>
        public async Task ShowButton(ButtonDefinition buttonDefinition)
        {
            if (_buttons.TryGetValue(buttonDefinition.viewName, out var button))
            {
                button.InitFromDefinition(buttonDefinition);
            }
            else
            {
                
                var loadedButton = await UILoader.Instance.LoadMonoView<BaseButton>(buttonDefinition.viewName,uiCanvas.transform);

                var Ibutton = loadedButton.GetComponent<IButton>();
                
                Ibutton.Initialize(this, buttonDefinition);

                _buttons.Add(buttonDefinition.viewName ,Ibutton);
            }
        }
        

        /// <summary>
        /// Creates Popup by the Given Popup Definition
        /// </summary>
        /// <param name="popupDefinition"></param>
        public async Task CreatePopup(PopupDefinition popupDefinition)
        {   
            var loadedPopup = await UILoader.Instance.LoadMonoView<BasePopup>(popupDefinition.viewName,popupCanvasGroup.transform);
            
            var iPopup = loadedPopup.GetComponent<IPopup>();
        
            iPopup.Initialize(this, popupDefinition);

            _popups.Add(popupDefinition.viewName ,iPopup);
            
            iPopup.Close();
        }

        /// <summary>
        /// Shows Popup by the Given Popup Definition.Loads Popups from Resources or Loads from Popups Dictionary
        /// </summary>
        /// <param name="popupDefinition"></param>
        /// <param name="raycastBlocker"></param>
        public async Task ShowPopup(PopupDefinition popupDefinition, bool raycastBlocker = true)
        {
            if (_popups.TryGetValue(popupDefinition.viewName, out var popup))
            {
                popup.InitFromDefinition(popupDefinition);
            }
            else
            {
                var loadedPopup = await UILoader.Instance.LoadMonoView<BasePopup>(popupDefinition.viewName,popupCanvasGroup.transform);

                var iPopup = loadedPopup.GetComponent<IPopup>();
                
                iPopup.Initialize(this, popupDefinition);

                _popups.Add(popupDefinition.viewName ,iPopup);
            }
            
            popupCanvasGroup.alpha = 1;
            popupCanvasGroup.interactable = true;
            popupCanvasGroup.blocksRaycasts = true;
            popupCanvasImage.enabled = raycastBlocker;
        }
        
        /// <summary>
        /// Closes Open Popup
        /// </summary>
        public void ClosePopup(BasePopup popup)
        {
            popup.Hide();
            
            popupCanvasGroup.alpha = 0;
            popupCanvasGroup.interactable = false;
            popupCanvasGroup.blocksRaycasts = false;
        }
        
           
        /// <summary>
        /// Closes Open Popup
        /// </summary>
        public void CloseButton(BaseButton button)
        {
            button.Hide();
            
            popupCanvasGroup.alpha = 0;
            popupCanvasGroup.interactable = false;
            popupCanvasGroup.blocksRaycasts = false;
        }
        
        /// <summary>
        /// Closes Open Popup
        /// </summary>
        public void ClosePanel(BasePanel panel)
        {
            panel.Hide();
            
            popupCanvasGroup.alpha = 0;
            popupCanvasGroup.interactable = false;
            popupCanvasGroup.blocksRaycasts = false;
        }
        
    }
}
