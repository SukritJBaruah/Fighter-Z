using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class IIdeal3State : IEnemy3States
{
    DifficultyUtils difficultyUtils = GameObject.FindGameObjectWithTag("DifficultyUtils").GetComponent<DifficultyUtils>();
    private Enemy3 enemy;

    private float idealTimer=0f;

    public void Enter(Enemy3 enemy1)
    {
        this.enemy = enemy1;
    }

    public void Execute()
    {
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
        //goes to walking after ideal duration
        enemy.Animator.SetFloat("Velocity", 0);


        idealTimer += Time.deltaTime;

        if(idealTimer > difficultyUtils.idealDuration)
        {
            enemy.ChangeState(new IWalk3State());
        }
    }
}
