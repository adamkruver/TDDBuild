using System.Collections;
using Cysharp.Threading.Tasks;
using Sources.Infrastructure.Services.Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sources.App.Core
{
    public class AppCore : MonoBehaviour
    {
        private SceneService _sceneService;

        private void Awake() =>
            DontDestroyOnLoad(this);

        private IEnumerator Start() =>
            _sceneService.ChangeStateAsync(SceneManager.GetActiveScene().name).ToCoroutine();

        private void Update() =>
            _sceneService?.Update(Time.deltaTime);

        private void FixedUpdate() =>
            _sceneService?.UpdateFixed(Time.fixedDeltaTime);

        private void LateUpdate() =>
            _sceneService?.UpdateLate(Time.deltaTime);

        public void Construct(SceneService sceneService) =>
            _sceneService = sceneService;
    }
}