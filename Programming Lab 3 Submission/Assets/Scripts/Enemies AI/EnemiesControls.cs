using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemiesControls : MonoBehaviour
{
     public NavMeshAgent agent;
    public Transform player;
    public LayerMask groundlayer, playerlayer;

    // Patrol
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // Attack
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    public GameObject projectilePos;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        //agent = GetComponent<NavMeshAgent>();
    }


    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerlayer);

        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerlayer);
        //Debug.Log(playerInSightRange);

        // Patrol State
        if (!playerInSightRange && !playerInAttackRange) Patrol();
        // Chase State
        if(playerInSightRange && !playerInAttackRange) Chase();
        // Attack State
        if(playerInSightRange && playerInAttackRange) Attack();
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void Patrol() 
    {
        if(!walkPointSet) SearchWalkPoint();

        if(walkPointSet)
        {
            gameObject.transform.Rotate(-90,180,0);
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceWalkPoint = transform.position - walkPoint;

        // Walkpoint reached
        if (distanceWalkPoint.magnitude < 1f)
        {
            gameObject.transform.Rotate(-90,180,0);
            walkPointSet = false;
        }

    }
    
    private void SearchWalkPoint()
    {
        // Calculate new point
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint, -transform.up, 2f, groundlayer))
        {
            walkPointSet = true;
        }

    }

    private void Chase() 
    {
        agent.SetDestination(player.position);
    }

    private void Attack() 
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);
        gameObject.transform.Rotate(-90,180,0);

        if (!alreadyAttacked)
        {
            Rigidbody bulletRb = Instantiate(projectile, projectilePos.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            bulletRb.AddForce(transform.forward * 32f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }


    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
