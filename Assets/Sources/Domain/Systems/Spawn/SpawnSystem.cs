using System;

namespace Sources.Domain.Systems.Spawn
{
    public class SpawnSystem
    {
        public event Action SpawnStarted;  
        public event Action SpawnFinished;  
        
        public void Start() => 
            SpawnStarted?.Invoke();

        public void Finish() => 
            SpawnFinished?.Invoke();
    }
}