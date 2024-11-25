using UnityEngine;
using UnityEngine.AI;

public class AIEnemyLongAttack : AIEnemy
{
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        currentState = State.Patrolling;
        GoToNextPatrolPoint();
        agent.speed = patrolSpeed;
        maxHP = GetComponent<EnemyInfor>().hp;
    }

    void Update()
    {
        curent = GetComponent<EnemyInfor>().hpcurrent;
        if (player == null)
        {
            player = GameObject.FindWithTag("Player")?.transform;
            if (player == null) return;
        }
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (gameObject.CompareTag("MinionEnemy"))
        {
            switch (currentState)
            {
                case State.Patrolling:
                    Patrol();
                    if (distanceToPlayer < chaseDistance)
                    {
                        StartChasing();
                    }
                    break;

                case State.Chasing:
                    ChasePlayer();
                    if (distanceToPlayer < attackDistance)
                    {
                        StartAttacking();
                    }
                    else if (distanceToPlayer > chaseDistance)
                    {
                        StartPatrolling();
                    }
                    break;

                case State.Attacking:
                    if (Time.time > lastAttackTime + attackCooldown)
                    {
                        AttackPlayer();
                        lastAttackTime = Time.time;
                    }
                    if (distanceToPlayer > attackDistance)
                    {

                        currentState = distanceToPlayer < chaseDistance ? State.Chasing : State.Patrolling;
                    }
                    break;
            }

        }
    }
    protected override void Patrol()
    {
        animator.SetTrigger("Patrol");
        agent.speed = patrolSpeed;
        animator.ResetTrigger("Chase");
        animator.ResetTrigger("Attack");
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextPatrolPoint();
        }
    }
}
