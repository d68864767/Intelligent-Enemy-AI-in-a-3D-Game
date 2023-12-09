```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    private Enemy enemy;
    private EnemyAIController controller;

    public PatrolState(Enemy enemy, EnemyAIController controller)
    {
        this.enemy = enemy;
        this.controller = controller;
    }

    public void Enter()
    {
        // Reset patrol index to the first point
        enemy.currentPatrolIndex = 0;
    }

    public void Execute()
    {
        // If there are no patrol points, return
        if (enemy.patrolPoints.Length == 0)
            return;

        // Get the current patrol point
        Transform targetPatrolPoint = enemy.patrolPoints[enemy.currentPatrolIndex];
        float distanceToPatrolPoint = Vector3.Distance(enemy.transform.position, targetPatrolPoint.position);

        // If the enemy is close enough to the patrol point, move to the next one
        if (distanceToPatrolPoint <= 1.0f)
        {
            enemy.currentPatrolIndex++;
            if (enemy.currentPatrolIndex >= enemy.patrolPoints.Length)
            {
                enemy.currentPatrolIndex = 0;
            }
        }

        // Move towards the patrol point
        Vector3 direction = (targetPatrolPoint.position - enemy.transform.position).normalized;
        Vector3 movement = direction * enemy.speed * Time.deltaTime;
        enemy.rb.MovePosition(enemy.transform.position + movement);

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
