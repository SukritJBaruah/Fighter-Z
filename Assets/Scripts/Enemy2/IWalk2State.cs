using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IWalk2State : IEnemy2States
{
    DifficultyUtils difficultyUtils = GameObject.FindGameObjectWithTag("DifficultyUtils").GetComponent<DifficultyUtils>();
    private Enemy2 enemy;
    private float walkTimer;
    private float blastTimer;
    protected Animator animator;

    private float locatorTimer;

    public void Enter(Enemy2 enemy1)
    {
        this.enemy = enemy1;
        animator = enemy.GetComponent<Animator>();
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
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy2_blast"))
        {
            enemy.Move();
        }
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
        blastTimer += Time.deltaTime;

        if (blastTimer > difficultyUtils.blastCooldown)
        {
            enemy.ChangeState(new IBlast2State());
        }

        if (walkTimer > difficultyUtils.walkDuration)
        {
            enemy.ChangeState(new IIdeal2State());
        }

        //check for
        if(enemy.InMeleeRange())
        {
            enemy.ChangeState(new IAttack2State());
        }
    }
}
