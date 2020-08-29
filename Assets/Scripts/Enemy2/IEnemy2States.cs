using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy2States
{
    void Execute();
    void Enter(Enemy2 enemy);
    void Exit();
    void OnTriggerEnter(Collider2D other);

}
