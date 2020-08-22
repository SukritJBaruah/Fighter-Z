using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IWalkState : IEnemyStates
{
    DifficultyUtils difficultyUtils = GameObject.FindGameObjectWithTag("DifficultyUtils").GetComponent<DifficultyUtils>();
    private Enemy1 enemy;
    private float walkTimer;

    private float locatorTimer;

    public void Enter(Enemy1 enemy1)
    {
        this.enemy = enemy1;
    }

    public void Execute()
    {
        Walk();

        locatorTimer += Time.deltaTime;
        if (locatorTimer> difficultyUtils.locateAfter)
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
        //goes to ideal after walk duration
        walkTimer += Time.deltaTime;

        if (walkTimer > difficultyUtils.walkDuration)
        {
            enemy.ChangeState(new IIdealState());
        }

        //check for
        if(enemy.InMeleeRange())
        {
            enemy.ChangeState(new IAttackState());
        }
    }
}
