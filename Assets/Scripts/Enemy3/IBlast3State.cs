using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.Collections;
using UnityEngine;

public class IBlast3State : IEnemy3States
{

    private Enemy3 enemy;
    protected Animator animator;
    DifficultyUtils difficultyUtils = GameObject.FindGameObjectWithTag("DifficultyUtils").GetComponent<DifficultyUtils>();

    float blastTimer = 0;
    bool canattacc;
    float blastendtimer = 0f;

    public void Enter(Enemy3 enemy1)
    {
        this.enemy = enemy1;
        animator = enemy.GetComponent<Animator>();
        canattacc = false;
    }

    public void Execute()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy1_fall") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Enemy1_damage"))
        {
            if(enemy.ifELeft())
            {
                animator.SetFloat("Velocity", 0);
                animator.SetBool("isblastanim", true);
                enemy.blast();
            }
        }
        enemy.ChangeState(new IIdeal3State());
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D other)
    {

    }

}
