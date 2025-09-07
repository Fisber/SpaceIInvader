using System.Threading.Tasks;
using Controllers;
using SO;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using VContainer;
using VContainer.Unity;

namespace EntryPoints
{
    public class GameLoop : IStartable
    {
        [Inject] private ObjectPool ObjectPool;
        [Inject] private GunFireController GunFireController;
        [Inject] private EnemyRowsManager EnemyRowsManager;
        [Inject] private DataHolder.Data DataHolder;
        [Inject] private UiController UiController;
        [Inject] private GunMoverController GunMoverController;
        [Inject] private LevelHolder.Level LevelIdHolder;

        public void Start()
        {
            InitializeWithDelay();
        }

        public async void InitializeWithDelay()
        {
            await Task.Delay(Constants.Delay);
            ObjectPool.Initialize();
            GunFireController.OnRequestFireEvent += GunFireControllerOnOnRequestFireEvent;
            GunFireController.OnEnemyReachPlayer += EndGame;
            GunFireController.Initialize();
            GunMoverController.Initialized(true);
            UiController.OnClick += OnOnClickOnPopups;
            UiController.OnRestart += Restart;
            UiController.OnNext += Next;
            
            UiController.Initialize();
  
            EnemyRowsManager.SubscribeToFireEvent(EnemyFireRequested);
            EnemyRowsManager.InitializeRowManager();
        }

        private void Next()
        {
            if (LevelIdHolder.LevelId == 0)
            {
                SceneManager.LoadScene(Constants.GamePlayScene2);
            }
            
            if (LevelIdHolder.LevelId == 1)
            {
                SceneManager.LoadScene(Constants.GamePlayScene3);
            }
        }

        private void OnOnClickOnPopups()
        {
            SceneManager.LoadScene(Constants.MenuScene);
        }


        private void EnemyFireRequested(EnemyController enemy)
        {
            var bulit = ObjectPool.GetBuilit();
            bulit.transform.position = enemy.transform.position;
            bulit.IsEnemy = true;
            bulit.OnHitPlayerAction = () => ObjectPool.ReturnToPool(bulit);
            bulit.OnTimeOutAction = () => ObjectPool.ReturnToPool(bulit);
            bulit.OnHitPlayerAction += EndGame;
            bulit.tag = Constants.TagEnemy;
            bulit.Initialize();
        }

        private void Restart()
        {
            if (LevelIdHolder.LevelId == 0)
                DataHolder.Score = 0;

            if (LevelIdHolder.LevelId == 1)
                DataHolder.Score = 50;

            if (LevelIdHolder.LevelId == 2)
                DataHolder.Score = 100;
            
            var name = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(name);
        }

        private void EndGame()
        {
            UiController.ShowLosePopup();
            EnemyRowsManager.OnGameEnd();
            GunFireController.CancelInvoke();
            GunMoverController.Initialized(false);
            DataHolder.Score = 0;
            DataHolder.LevelPaseed = 0;

        }

        private void GunFireControllerOnOnRequestFireEvent(Vector2 pos)
        {
            var bulit = ObjectPool.GetBuilit();
            bulit.transform.position = pos;
            bulit.tag = Constants.TagUntagged;
            bulit.IsEnemy = false;
            bulit.OnHitEnemyAction = () => ObjectPool.ReturnToPool(bulit);
            bulit.OnTimeOutAction = () => ObjectPool.ReturnToPool(bulit);
            bulit.OnHitEnemyAction += IncreaseScore;
            bulit.Initialize();
        }

        private void IncreaseScore()
        {
            DataHolder.Score++;
            UiController.SetScore( DataHolder.Score);
            CheckWin();
        }

        private void CheckWin()
        {
            int winTarget = Constants.WinAmount;
            
            if (LevelIdHolder.LevelId == 1)
            {
                winTarget = Constants.WinAmount2;
            }
            
            if (LevelIdHolder.LevelId == 2)
            {
                winTarget = Constants.WinAmount3;
            }
     
            
            if (DataHolder.Score >= winTarget)
            {
                UiController.ShowWinPopup();
                UiController.SetYouWonText(LevelIdHolder.LevelId);
                EnemyRowsManager.OnGameEnd();
                GunFireController.CancelInvoke();
                GunMoverController.Initialized(false);
                DataHolder.LevelPaseed++;
            }
        }
    }
}