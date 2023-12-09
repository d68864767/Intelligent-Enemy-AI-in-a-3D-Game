```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : MonoBehaviour
{
    private EnemyAIController aiController;
    private Enemy enemy;
    private FieldOfView fieldOfView;

    void Start()
    {
        aiController = GetComponent<EnemyAIController>();
        enemy = GetComponent<Enemy>();
        fieldOfView = GetComponent<FieldOfView>();
    }

    void Update()
    {
        if (aiController.state == EnemyAIController.State.Chasing)
        {
            ChasePlayer();
        }
    }

    void ChasePlayer()
    {
        enemy.chaseTimer += Time.deltaTime;
        if (enemy.chaseTimer > enemy.chaseTime)
        {
            enemy.chaseTimer = 0f;
            aiController.state = EnemyAIController.State.Returning;
        }
        else
        {
            enemy.Move();
        }
    }
}
```
