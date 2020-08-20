using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class IIdealState : IEnemyStates
{
    private Enemy1 enemy;

    private float idealTimer;

    private float idealDuration = 0.5f;
    public void Enter(Enemy1 enemy1)
    {
        this.enemy = enemy1;
    }

    public void Execute()
    {
        Debug.Log("Ideal");
        Ideal();

    }

    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D other)
    {

    }

    private void Ideal()
    {
        enemy.Animator.SetFloat("Velocity", 0);

        idealTimer += Time.deltaTime;

        if(idealTimer > idealDuration)
        {
            enemy.ChangeState(new IWalkState());
        }
    }
}
