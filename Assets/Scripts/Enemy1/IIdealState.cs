﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class IIdealState : IEnemyStates
{
    DifficultyUtils difficultyUtils = GameObject.FindGameObjectWithTag("DifficultyUtils").GetComponent<DifficultyUtils>();
    private Enemy1 enemy;

    private float idealTimer=0f;

    public void Enter(Enemy1 enemy1)
    {
        this.enemy = enemy1;
    }

    public void Execute()
    {
        Ideal();
        enemy.Animator.SetBool("Attack1", false);

    }

    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D other)
    {

    }

    private void Ideal()
    {
        //goes to walking after ideal duration
        enemy.Animator.SetFloat("Velocity", 0);

        idealTimer += Time.deltaTime;

        if(idealTimer > difficultyUtils.idealDuration)
        {
            enemy.ChangeState(new IWalkState());
        }
    }
}
