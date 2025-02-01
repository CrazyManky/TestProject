using _Project._Screpts.GameItems.GameLevels;
using _Project._Screpts.Interfaces;
using _Project._Screpts.LoadSystem;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Zenject;

namespace _Project._Screpts.GameStateMashine.States
{
    public class LoadingState : IGameState
    {
        private GameFSM _gameFsm;
        private LoadingService _loadingService;

        [Inject]
        public void Constructor(GameFSM gameFsm, ConteinerLevels conteinerLevelsPrefab, LoadingService loadingService)
        {
            _gameFsm = gameFsm;
            _loadingService = loadingService;
        }

        public void EnterState()
        {
            LoadNextSceneAsync();
        }

        private async UniTask LoadNextSceneAsync()
        {
            var asyncOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            await _loadingService.LoadFromFileAsync();

            while (!asyncOperation.isDone)
            {
                await UniTask.Yield();
            }

            _gameFsm.Enter<GamePlayState>();
        }


        public void ExitState()
        {
        }
    }
}