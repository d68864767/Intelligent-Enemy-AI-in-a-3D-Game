```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IEnemyState
{
    private Enemy enemy;
    private float attackTimer;
    private float attackCoolDown = 3.0f;
    private bool canAttack = true;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        Attack();
        if (enemy.Target != null && !WithinAttackRange())
        {
            enemy.ChangeState(new ChaseState());
        }
        else if (enemy.Target == null)
        {
            enemy.ChangeState(new PatrolState());
        }
    }

    public void Exit()
    {
    }

    public void OnTriggerEnter(Collider other)
    {
    }

    private void Attack()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackCoolDown)
        {
            canAttack = true;
            attackTimer = 0;
        }

        if (canAttack)
        {
            canAttack = false;
            enemy.MyAnimator.SetTrigger("attack");
        }
    }

    private bool WithinAttackRange()
    {
        if (Vector3.Distance(enemy.transform.position, enemy.Target.transform.position) <= enemy.attackRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
```
