using Sources.Infrastructure.StateMachines;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sources.App.Core
{
    public class AppCore : MonoBehaviour
    {
        public StateMachine StateMachine { get; private set; }

        private void Awake() =>
            DontDestroyOnLoad(this);

        private void Start() =>
            StateMachine.ChangeStateAsync(SceneManager.GetActiveScene().name);

        private void Update() =>
            StateMachine?.Update(Time.deltaTime);

        private void FixedUpdate() =>
            StateMachine?.UpdateFixed(Time.fixedDeltaTime);

        private void LateUpdate() =>
            StateMachine?.UpdateLate(Time.deltaTime);

        public void Construct(StateMachine stateMachine) =>
            StateMachine = stateMachine;
    }
}