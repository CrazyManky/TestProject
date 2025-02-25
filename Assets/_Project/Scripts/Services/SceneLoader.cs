using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.Services
{
    public class SceneLoader
    {
        public async UniTask LoadSceneAsync(int sceneIndex) => await SceneManager.LoadSceneAsync(sceneIndex);
    }
}