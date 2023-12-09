```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3.0f;
    public float sightRange = 10.0f;
    public float attackRange = 2.0f;
    public float chaseTime = 10.0f;
    public Transform[] patrolPoints;
    public Player player;

    private int currentPatrolIndex;
    private float chaseTimer;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        currentPatrolIndex = 0;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer <= sightRange)
        {
            ChasePlayer();
        }
        else if (distanceToPlayer <= attackRange)
        {
            AttackPlayer();
        }
        else
        {
            Patrol();
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    void Patrol()
    {
        if (patrolPoints.Length == 0)
            return;

        Transform targetPatrolPoint = patrolPoints[currentPatrolIndex];
        float distanceToPatrolPoint = Vector3.Distance(transform.position, targetPatrolPoint.position);

        if (distanceToPatrolPoint <= 1.0f)
        {
            currentPatrolIndex++;
            if (currentPatrolIndex >= patrolPoints.Length)
            {
                currentPatrolIndex = 0;
            }
        }
    }

    void ChasePlayer()
    {
        chaseTimer += Time.deltaTime;
        if (chaseTimer > chaseTime)
        {
            chaseTimer = 0f;
            ReturnToPatrol();
        }
    }

    void AttackPlayer()
    {
        // Implement your attack logic here
    }

    void ReturnToPatrol()
    {
        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }

    void Move()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Vector3 movement = direction * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
    }
}
```
