using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IWalkState : IEnemyStates
{
    private Enemy1 enemy;
    private float walkTimer;
    private float walkDuration = 7f;

    private float locatorTimer;
    private float locateAfter = 0.7f;

    public void Enter(Enemy1 enemy1)
    {
        this.enemy = enemy1;
    }

    public void Execute()
    {
        Walk();

        locatorTimer += Time.deltaTime;
        if (locatorTimer>locateAfter)
        {
            enemy.LocatePlayer();
            locatorTimer = 0;
        }
        enemy.Move();
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D other)
    {

    }

    private void Walk()
    {

        walkTimer += Time.deltaTime;

        if (walkTimer > walkDuration)
        {
            enemy.ChangeState(new IIdealState());
        }
    }
}
