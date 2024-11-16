using UnityEngine;
using UnityEngine.AI;

public class AIEnemy : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float chaseDistance = 10f;
    public float attackDistance = 2f;
    public float attackCooldown = 1.5f;
    public float patrolSpeed = 3;
    public float chaseSpeed = 5;

    public Transform player;
    private NavMeshAgent agent;
    private int currentPatrolIndex;
    private float lastAttackTime;

    public Collider collider;
    private Animator animator;
    private enum State { Patrolling, Chasing, Attacking }
    private State currentState;

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        currentState = State.Patrolling;
        GoToNextPatrolPoint();
        collider.enabled = false;
        agent.speed = patrolSpeed;
    }

    private void Update()
    {
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

    private void Patrol()
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

    private void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;
        agent.destination = patrolPoints[currentPatrolIndex].position;
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    private void StartChasing()
    {
        currentState = State.Chasing;
    }

    private void ChasePlayer()
    {
        animator.SetTrigger("Chase");
        agent.speed = chaseSpeed;
        animator.ResetTrigger("Patrol");
        animator.ResetTrigger("Attack");
        agent.SetDestination(player.position);
    }

    private void StartAttacking()
    {
        currentState = State.Attacking;
    }

    private void AttackPlayer()
    {
        agent.speed = 0;
        animator.SetTrigger("Attack");
        animator.ResetTrigger("Chase");
    }

    private void StartPatrolling()
    {
        currentState = State.Patrolling;
        GoToNextPatrolPoint();
    }

    public void SetgoAI()
    {
        agent.isStopped = false;
    }

    public void SetstopAI()
    {
        agent.isStopped = true;
    }

    public void EndCollider()
    {
        collider.enabled = false;
    }

    public void OnCollider()
    {
        collider.enabled = true;
    }
}
