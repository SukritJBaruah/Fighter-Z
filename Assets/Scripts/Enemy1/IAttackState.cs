using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class IAttackState : IEnemyStates
{
    private Enemy1 enemy;
    protected Animator animator;
    DifficultyUtils difficultyUtils = GameObject.FindGameObjectWithTag("DifficultyUtils").GetComponent<DifficultyUtils>();

    float attaccTimer = 0;
    bool canattacc;
    float punchendtimer= 0f;

    public void Enter(Enemy1 enemy1)
    {
        this.enemy = enemy1;
        animator = enemy.GetComponent<Animator>();
        canattacc = false;
    }

    public void Execute()
    {
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy1_fall") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy1_damage"))
        {
            Attacc();
        }
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
        animator.SetFloat("Velocity", 0);
        if (!canattacc)
        {
            enemy.Animator.SetBool("Attack1", false);

            punchendtimer += Time.smoothDeltaTime;
            if (punchendtimer > 0.05f)
            {
                enemy.enemy_punch.enabled = false;
            }
        }

        attaccTimer += Time.deltaTime;
        if(attaccTimer >= difficultyUtils.attaccCooldown)
        {
            canattacc = true;
            attaccTimer = 0;
        }

        if(canattacc)
        {
            animator.SetFloat("Velocity", 0);
            canattacc = false;
            enemy.Animator.SetBool("Attack1", true);
            //attack code punch
            enemy.enemy_punch.enabled = true;
            punchendtimer = 0f;

            //move a lil forward, remove if not necessary
            enemy.transform.Translate(enemy.GetDirection() * Enemy1.MoveUnitsPerSecond * Time.deltaTime * 0.1f);
        }
    }

}
