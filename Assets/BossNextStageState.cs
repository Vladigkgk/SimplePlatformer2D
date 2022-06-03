using System.Collections;
using System.Collections.Generic;
using PixelCrew.Components.GoBased;
using PixelCrew.Creatures.Mobs.Boss;
using UnityEngine;

public class BossNextStageState : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var spawner = animator.GetComponent<CircularProjectileSpawner>();
        spawner.Stage++;

        var changeColorLight = animator.GetComponent<ModifyLights>();
        changeColorLight.SetColor();

        var bombSpawn = animator.GetComponent<BombSpawnerComponent>();
        bombSpawn.SetSpawn();
    }
}