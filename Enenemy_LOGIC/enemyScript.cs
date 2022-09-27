using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class enemyScript : MonoBehaviour
{
    Animator animator;
    public UnityEngine.AI.NavMeshAgent agent;
    public GameObject healthBarUI;
    public Image slider;
    public LayerMask whatIsGround, whatIsPlayer;
    public float maxhealth;
    [SerializeField] float health;
    //bullet
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    //bullet^^^^
    public Transform player;
    public GameObject player_heal;
    //public Transform Target;
    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    //public ParticleSystem MuzzleFlash;
    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    void Start()
    {
        //ParticleSystem ps = GameObject.Find("HitEffect").GetComponent<ParticleSystem>();
        health = maxhealth;
        slider.fillAmount = Calculate_Health();
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player_2(Clone)").transform;
        player_heal = GameObject.Find("Player_2(Clone)");

    }
    void Awake()
    {
        //Target = GameObject.Find("target").transform;//------------
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
    void Update()
    {
        slider.fillAmount = Calculate_Health();
        //Check for sight and attack range       
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        animator.SetFloat("Blend", agent.velocity.magnitude);
        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (!alreadyAttacked && playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (!alreadyAttacked && playerInAttackRange && playerInSightRange) AttackPlayer();
        if (alreadyAttacked && playerInAttackRange && playerInSightRange) Patroling();
       
    }
    
    float Calculate_Health()
    {
        return health / maxhealth;
    }
    IEnumerator unstuned()
    {
        yield return new WaitForSeconds(4f);
        if (health > 0)
        {
            animator.SetBool("stuned", false);
            health += 25;
        }
    }
    //Nav Mesh Logic
    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
       
        
    }
    
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y/*+ randomY*/, transform.position.z + randomZ);
       
        print("look for dsncn");
        agent.SetDestination(walkPoint);
    }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(2f);
        animator.Play("shoot");       
        Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        
        Invoke(nameof(ResetAttack), timeBetweenAttacks);
        Patroling();
    }
    private void AttackPlayer()
    {
        //enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            Debug.Log("shooted");
            StartCoroutine(Shoot());
            alreadyAttacked = true;
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
    //End of it
    void Die()
    {
        player.GetComponent<characterStat>().Heal(30);
        this.gameObject.SetActive(false);
    }
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }
        if (health == 25f)
        {
            animator.SetBool("stuned", true);
            StartCoroutine(unstuned());
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
}
