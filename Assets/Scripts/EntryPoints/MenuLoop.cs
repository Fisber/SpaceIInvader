using Controllers;
using SO;
using UnityEngine.SceneManagement;
using Utils;
using VContainer;
using VContainer.Unity;

namespace EntryPoints
{
    public class MenuLoop : IStartable
    {
        [Inject] private UiMenuController UiMenuController;
        [Inject] private DataHolder.Data UserData;
        [Inject] private LevelHolder.Level Level;
        
        
        public void Start()
        {
            UiMenuController.OnClickPlayLevel1 += OnClicklevel1;
            UiMenuController.OnClickPlayLevel2 += OnClicklevel2;
            UiMenuController.OnClickPlayLevel3 += OnClicklevel3;

            if (UserData.LevelPaseed > 0)
            {
                UiMenuController.EnableLevel2();
            }
            
            if (UserData.LevelPaseed > 1)
            {
                UiMenuController.EnableLevel3();
            }
            

            UiMenuController.SubScribe();
            UiMenuController.SetScore(UserData.Score);
        }

        private void OnClicklevel1()
        {
            SceneManager.LoadScene(Constants.GamePlayScene);
        }
        private void OnClicklevel2()
        {
            if (UserData.LevelPaseed > 0)
            {
                SceneManager.LoadScene(Constants.GamePlayScene2);
            }
        }
        private void OnClicklevel3()
        {
            if (UserData.LevelPaseed > 1)
            {
                SceneManager.LoadScene(Constants.GamePlayScene3);
            }
        }
    }
}