using Unity.VisualScripting;
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

    public float maxHP;
    public float curent;

    public float throwFroce = 10f;
    public float spawnRadius = 1f;
    public Transform player;
    protected NavMeshAgent agent;
    protected int currentPatrolIndex;
    protected float lastAttackTime;
    public Collider attackcoli;
    protected Animator animator;
    protected bool isReloading = false;
    bool oke = true;

    public GameObject arrowprefabs;
    public Transform arrowSpawn;
    public float bulletspeed = 10f;
    protected enum State { Patrolling, Chasing, Attacking, Reload }
    protected State currentState;

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        currentState = State.Patrolling;
        GoToNextPatrolPoint();
        if (!gameObject.CompareTag("RogueEnemy"))
        {
            attackcoli.enabled = false;
        }
        agent.speed = patrolSpeed;
        maxHP = GetComponent<EnemyInfor>().hp;
    }

    private void Update()
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
        else if (gameObject.CompareTag("RogueEnemy"))
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

                case State.Reload:
                    if (!isReloading)
                    {
                        agent.speed = 0;
                        animator.SetTrigger("Reload");
                        isReloading = true;
                    }
                    if (oke)
                    {
                        if (distanceToPlayer > attackDistance)
                        {
                            currentState = distanceToPlayer < chaseDistance ? State.Chasing : State.Patrolling;
                        }
                        else
                        {
                            StartAttacking();
                        }
                    }
                    break;

                case State.Attacking:

                    if (Time.time > lastAttackTime + attackCooldown)
                    {
                        if (distanceToPlayer <= 3f)
                        {
                            NormalAttacking();
                            lastAttackTime = Time.time;
                        }
                        else if (distanceToPlayer > 3)
                        {
                            AttackPlayer();
                            lastAttackTime = Time.time;
                            currentState = State.Reload;
                        }
                    }
                    break;
            }
        }
        
        else if (gameObject.CompareTag("WariorBoss"))
        {
            switch (currentState)
            {
                case State.Patrolling:
                    GoToPlayer();
                    if (distanceToPlayer < attackDistance)
                    {
                        StartAttacking();
                    }
                    break;

                case State.Attacking:
                    if (Time.time > lastAttackTime + attackCooldown)
                    {
                        if (curent > maxHP * 0.5f)
                        {
                            attackDistance = 4f;
                            NormalAttack();
                        }
                        else if (curent <= maxHP * 0.5f)
                        {
                            patrolSpeed = 4.5f;
                            FastAttack();
                        }
                        
                        lastAttackTime = Time.time;
                    }
                    if (distanceToPlayer > attackDistance)
                    {
                        currentState = State.Patrolling;
                    }
                    break;
            }
        }

    }
    protected void Fire()
    {
        gameObject.transform.LookAt(player);
        Vector3 dicrection = (player.position - arrowSpawn.position).normalized;
        float angleY = Mathf.Atan2(dicrection.x, dicrection.z) * Mathf.Rad2Deg;
        GameObject bullet = Instantiate(arrowprefabs, arrowSpawn.position,Quaternion.Euler(-90,angleY,0));
        Rigidbody rid = bullet.GetComponent<Rigidbody>();
        if (rid != null)
        {
            rid.velocity = dicrection * bulletspeed;
        }
        Destroy(bullet, 5f);
    }
    protected virtual void Patrol()
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

    protected void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;
        agent.destination = patrolPoints[currentPatrolIndex].position;
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }
    protected void GoToPlayer()
    {
        animator.SetTrigger("Patrol");
        agent.speed = patrolSpeed;
        agent.SetDestination(player.position);
        animator.ResetTrigger("Attack");
    }
    private void FastAttack()
    {
        agent.speed = 0;
        animator.SetTrigger("AttackFast");
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Patrol");
    }
    public void StartReLoad()
    {
        currentState = State.Reload;
    }
    protected void StartChasing()
    {
        currentState = State.Chasing;
    }
    protected void NormalAttack()
    {
        agent.speed = 0;
        animator.SetTrigger("Attack");
        animator.ResetTrigger("Patrol");

    }

    protected void ReLoad()
    {
        agent.speed = 0;
        animator.ResetTrigger("Attack");
        animator.SetTrigger("Reload");
        
    }

    protected void ChasePlayer()
    {
        animator.SetTrigger("Chase");
        agent.speed = chaseSpeed;
        animator.ResetTrigger("Patrol");
        animator.ResetTrigger("Attack");
        agent.SetDestination(player.position);
    }

    protected void StartAttacking()
    {
        currentState = State.Attacking;
    }

    protected void NormalAttacking()
    {
        agent.speed = 0;
        animator.SetTrigger("AttackNormal");
        animator.ResetTrigger("Chase");
        StartReLoad();
    }
    protected void AttackPlayer()
    {
        agent.speed = 0;
        animator.SetTrigger("Attack");
        animator.ResetTrigger("Chase");
    }

    protected void StartPatrolling()
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
        attackcoli.enabled = false;
    }

    public void OnCollider()
    {
        attackcoli.enabled = true;
    }

    public void OnOk()
    {
        oke = false;
    }
}
