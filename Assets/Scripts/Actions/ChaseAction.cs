﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BotLight
{
    [CreateAssetMenu(menuName = "BotLight/Actions/Chase")]
    public class ChaseAction : Action
    {
        public override void Act(StateController controller)
        {
            Chase(controller);
        }

        private void Chase(StateController controller)
        {
            controller.botMovement.navMeshAgent.destination = controller.chaseTarget.position;
            controller.botMovement.navMeshAgent.isStopped = false;
        }
    }
}