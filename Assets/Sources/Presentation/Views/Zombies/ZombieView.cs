﻿using Sources.Controllers.Zombies;
using Sources.Presentation.Views.Enemies;
using Sources.PresentationInterfaces.Views.Zombies;
using UnityEngine;
using UnityEngine.AI;

namespace Sources.Presentation.Views.Zombies
{
    public class ZombieView : EnemyView<ZombieStateMachine>, IZombieView
    {
        [SerializeField] private NavMeshAgent _navMeshAgent;

        public NavMeshAgent NavMeshAgent => _navMeshAgent;
        
        public void Update()
        {
            Presenter?.Update(Time.deltaTime);
        }
        
        public void Die()
        {
            gameObject.SetActive(false);
            Debug.Log("Zombie Died");
        }
    }
}