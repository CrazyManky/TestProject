using _Project.Scripts.GameStateMachine;
using _Project.Scripts.GameStateMachine.States;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Scripts.UI
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private Button _restartLevel;
        [SerializeField] private Button _exitApp;
        [SerializeField] private RectTransform _rectTransform;

        private GameFSM _gameFsm;

        [Inject]
        public void Construct(GameFSM gameFsm)
        {
            _gameFsm = gameFsm;
            _restartLevel.onClick.AddListener(Restart);
            _exitApp.onClick.AddListener(CloseApp);
        }

        public void Open()
        {
            _restartLevel.interactable = false;
            _exitApp.interactable = false;
            _rectTransform.localScale = new Vector3(0, 0, 1);
            _rectTransform.DOScale(new Vector3(1, 1, 1), 0.5f).OnComplete(() =>
            {
                _restartLevel.interactable = true;
                _exitApp.interactable = true;
            });
        }

        public void Restart()
        {
            _gameFsm.Enter<GameOverState>();
            _restartLevel.onClick.RemoveListener(Restart);
            _exitApp.onClick.RemoveListener(CloseApp);
            Destroy(gameObject);
        }

        private void CloseApp()
        {
#if UNITY_EDITOR

            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}