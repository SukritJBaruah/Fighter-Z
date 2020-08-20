using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyStates
{
    void Execute();
    void Enter(Enemy1 enemy);
    void Exit();
    void OnTriggerEnter(Collider2D other);

}
