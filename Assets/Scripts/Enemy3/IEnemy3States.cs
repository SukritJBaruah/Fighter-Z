using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy3States
{
    void Execute();
    void Enter(Enemy3 enemy);
    void Exit();
    void OnTriggerEnter(Collider2D other);

}
