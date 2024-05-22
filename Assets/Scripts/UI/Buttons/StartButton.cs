using JokerGames.AssetManagement;
using JokerGames.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JokerGames.UI.Buttons
{

    public class StartButton : BaseButton
    {
        private StartButtonDefinition _startButtonDefinition;

        public class StartButtonDefinition : ButtonDefinition
        {
            public StartButtonDefinition() : base(ViewName.StartGameButton)
            {
            }
        }
       /// <summary>
       /// Init firs time
       /// </summary>
       /// <param name="givenUIManager"></param>
       /// <param name="buttonDefinition"></param>
       public override void Initialize(UIManager givenUIManager, ButtonDefinition buttonDefinition)
       {
           base.Initialize(givenUIManager, buttonDefinition);
           _startButtonDefinition = buttonDefinition as StartButtonDefinition;
       }
        
       /// <summary>
       /// Load Existing Popup And Update
       /// </summary>
       /// <param name="buttonDefinition"></param>
       public override void InitFromDefinition(ButtonDefinition buttonDefinition)
       {
           base.InitFromDefinition(buttonDefinition);
           _startButtonDefinition = buttonDefinition as StartButtonDefinition;
       }
       
       /// <summary>
       /// Confirm Button
       /// </summary>
       public override void Confirm()
       {

           SceneManager.LoadScene("BoardScene");
           base.Confirm();
       }

    }
}
