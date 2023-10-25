using UnityEngine;

namespace Sources.Controllers.Scenes
{
    public class MainMenuScene : IScene
    {
        public void Update(float deltaTime)
        {
        }

        public void UpdateFixed(float fixedDeltaTime)
        {
        }

        public void UpdateLate(float deltaTime)
        {
        }

        public void Enter(object payload)
        {
            Debug.Log("Scene is Running");
        }

        public void Exit()
        {
        }
    }
}