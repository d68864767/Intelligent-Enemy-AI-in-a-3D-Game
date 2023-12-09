```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnState : IState
{
    private Enemy enemy;
    private EnemyAIController controller;

    public ReturnState(Enemy enemy, EnemyAIController controller)
    {
        this.enemy = enemy;
        this.controller = controller;
    }

    public void Enter()
    {
        // Reset chase timer
        enemy.chaseTimer = 0f;
    }

    public void Execute()
    {
        // Get the current patrol point
        Transform targetPatrolPoint = enemy.patrolPoints[enemy.currentPatrolIndex];
        float distanceToPatrolPoint = Vector3.Distance(enemy.transform.position, targetPatrolPoint.position);

        // If the enemy is close enough to the patrol point, switch to the patrol state
        if (distanceToPatrolPoint <= 1.0f)
        {
            controller.state = EnemyAIController.State.Patrolling;
        }
        else
        {
            // Move towards the patrol point
            Vector3 direction = (targetPatrolPoint.position - enemy.transform.position).normalized;
            Vector3 movement = direction * enemy.speed * Time.deltaTime;
            enemy.rb.MovePosition(enemy.transform.position + movement);
        }

        // If the player is in sight, switch to the chase state
        if (controller.fieldOfView.visibleTargets.Contains(enemy.player.transform))
        {
            controller.state = EnemyAIController.State.Chasing;
        }
    }

    public void Exit()
    {
        // Nothing to do here
    }
}
```
