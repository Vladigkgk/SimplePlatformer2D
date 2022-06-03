using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PixelCrew.Creatures.Mobs.Boss
{
    public class BossFloodState : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var floodController = animator.GetComponent<FloodControlller>();
            floodController.StartFlooding();
        }
    }
}
