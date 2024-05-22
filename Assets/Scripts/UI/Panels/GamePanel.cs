using DG.Tweening;
using JokerGames.AssetManagement;
using JokerGames.Managers;
using TMPro;
using UnityEngine;


namespace JokerGames.UI.Panels
{

    public class GamePanel : BasePanel, IGamePanel
    {
        public class InventoryPanel : PanelDefinition
        {
            public int Apples;
            public int Pears;
            public int Strawberries;
    
            public InventoryPanel(int apples, int pears, int strawberries) : base(ViewName.GamePanel)
            {
                Apples = apples;
                Pears = pears;
                Strawberries = strawberries;
            }
        }

        private InventoryPanel _startButtonDefinition;
    
        [SerializeField] private TextMeshProUGUI _appleGUI;
        [SerializeField] private TextMeshProUGUI _pearsGUI;
        [SerializeField] private TextMeshProUGUI _strawberriesGUI;
    
        /// <summary>
        /// Init firs time
        /// </summary>
        /// <param name="givenUIManager"></param>
        /// <param name="panelDefinition"></param>
        public override void Initialize(UIManager givenUIManager, PanelDefinition panelDefinition)
        {
            base.Initialize(givenUIManager, panelDefinition);
            _startButtonDefinition = panelDefinition as InventoryPanel;
    
            _appleGUI.text = _startButtonDefinition.Apples.ToString();
            _pearsGUI.text = _startButtonDefinition.Pears.ToString();
            _strawberriesGUI.text = _startButtonDefinition.Strawberries.ToString();
        }
    
        /// <summary>
        /// Load Existing Popup And Update
        /// </summary>
        /// <param name="panelDefinition"></param>
        public override void InitFromDefinition(PanelDefinition panelDefinition)
        {
            base.InitFromDefinition(panelDefinition);
            _startButtonDefinition = panelDefinition as InventoryPanel;
            
            _appleGUI.text = _startButtonDefinition.Apples.ToString();
            _pearsGUI.text = _startButtonDefinition.Pears.ToString();
            _strawberriesGUI.text = _startButtonDefinition.Strawberries.ToString();
        }

        public void UpdateApple(int count)
        {
            _appleGUI.transform.DOShakeScale(1.5f, 1.3f);
            _appleGUI.transform.DOShakeRotation(1, 10);
            _appleGUI.text = count.ToString();
        }

        public void UpdatePear(int count)
        {
            _pearsGUI.transform.DOShakeScale(1.5f, 1.3f);
            _pearsGUI.transform.DOShakeRotation(1, 10);
            _pearsGUI.text = count.ToString();
        }

        public void UpdateStrawberry(int count)
        {
            _strawberriesGUI.transform.DOShakeScale(1.5f, 1.3f);
            _strawberriesGUI.transform.DOShakeRotation(1, 10);
            _strawberriesGUI.text = count.ToString();
        }
    }

}
