using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAttackState : IEnemyStates
{
    private Enemy1 enemy;
    protected Animator animator;
    DifficultyUtils difficultyUtils = GameObject.FindGameObjectWithTag("DifficultyUtils").GetComponent<DifficultyUtils>();

    float attaccTimer = 0;
    bool canattacc;

    public void Enter(Enemy1 enemy1)
    {
        this.enemy = enemy1;
        animator = enemy.GetComponent<Animator>();
        canattacc = false;
    }

    public void Execute()
    {
        Attacc();
        if(!enemy.InMeleeRange())
        {
            enemy.ChangeState(new IIdealState());
        }

    }

    public void Exit()
    {
    }

    public void OnTriggerEnter(Collider2D other)
    {
    }

    private void Attacc()
    {
        if (!canattacc)
        {
            enemy.Animator.SetBool("Attack1", false);
        }

        attaccTimer += Time.deltaTime;
        if(attaccTimer >= difficultyUtils.attaccCooldown)
        {
            canattacc = true;
            attaccTimer = 0;
        }

        if(canattacc)
        {
            canattacc = false;
            enemy.Animator.SetBool("Attack1", true);
            animator.SetFloat("Velocity", 0);
        }
    }
}
