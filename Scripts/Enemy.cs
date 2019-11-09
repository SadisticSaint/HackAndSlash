using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Attacker))]
[RequireComponent(typeof(NavMeshAgent))]

public class Enemy : PooledMonoBehaviour, ITakeHit
{
    [SerializeField]
    private GameObject impactParticle;
    [SerializeField]
    private int maxHealth = 3;

    private int currentHealth;

    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private Attacker attacker;
    private Character target;

    private bool isDead { get { return currentHealth <= 0; } }

    private void Awake()
    {
        animator = GetComponent<Animator>(); //change to GetComponentsInChildren for multiple enemy types?
        navMeshAgent = GetComponent<NavMeshAgent>();
        attacker = GetComponent<Attacker>();
    }

    private void OnEnable()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (isDead)
            return;

        if (target == null)
        {
            AcquireTarget();
        }
        else
        {
            var distance = Vector3.Distance(transform.position, target.transform.position);

            if (distance > 2) //shouldn't be hardcoded
            {
                FollowTarget();
            }
            else
            {
                TryToAttack();
            }
        }
    }

    private void AcquireTarget()
    {
        target = Character.All
            .OrderBy(t => Vector3.Distance(transform.position, t.transform.position))
            .FirstOrDefault(); //Why do we need first or default? error

        animator.SetFloat("Speed", 0);
    }

    private void FollowTarget()
    {
        animator.SetFloat("Speed", 1);
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(target.transform.position);
        //What would happen if target were an array/list?
        //it wouldn't cycle through different characters
    }

    private void TryToAttack()
    {
        animator.SetFloat("Speed", 0);
        navMeshAgent.isStopped = true; //what does this do? does setting float to 0 do the same thing?

        if (attacker.CanAttack)
        {
            animator.SetTrigger("Attack");
            attacker.Attack(target);
        }
    }

    public void TakeHit(IAttack hitBy)
    {
        //currentHealth--;
        currentHealth -= hitBy.Damage;

        Instantiate(impactParticle, transform.position + new Vector3(0, 2, 0), Quaternion.identity);

        if (isDead)
        {
            navMeshAgent.isStopped = true;
            Die();
        }
        else
        {
            animator.SetTrigger("Hit");
        }
    }

    private void Die()
    {
        animator.SetTrigger("Die");

    }
}
