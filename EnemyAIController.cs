```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIController : MonoBehaviour
{
    private Enemy enemy;
    private FieldOfView fieldOfView;

    private enum State
    {
        Patrolling,
        Chasing,
        Attacking,
        Returning
    }

    private State state;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        fieldOfView = GetComponent<FieldOfView>();
        state = State.Patrolling;
    }

    void Update()
    {
        switch (state)
        {
            case State.Patrolling:
                if (fieldOfView.visibleTargets.Contains(enemy.player.transform))
                {
                    state = State.Chasing;
                }
                break;
            case State.Chasing:
                if (!fieldOfView.visibleTargets.Contains(enemy.player.transform))
                {
                    state = State.Returning;
                }
                else if (Vector3.Distance(transform.position, enemy.player.transform.position) <= enemy.attackRange)
                {
                    state = State.Attacking;
                }
                break;
            case State.Attacking:
                if (Vector3.Distance(transform.position, enemy.player.transform.position) > enemy.attackRange)
                {
                    state = State.Chasing;
                }
                break;
            case State.Returning:
                if (fieldOfView.visibleTargets.Contains(enemy.player.transform))
                {
                    state = State.Chasing;
                }
                else if (Vector3.Distance(transform.position, enemy.patrolPoints[enemy.currentPatrolIndex].position) <= 1.0f)
                {
                    state = State.Patrolling;
                }
                break;
        }
    }
}
```
