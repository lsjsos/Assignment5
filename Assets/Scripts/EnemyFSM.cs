using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour
{
    public enum EnemyState { GoToBase, AttackBase, ChasePlayer, AttackPlayer, Photonization, SuicideBombing }
    public EnemyState currentState;
    public Sight sightSensor;
    public Transform baseTransform;
    public float baseAttackDistance;
    public float playerAttackDistance;
    public float lastShootTime;
    public GameObject bulletPrefab;
    public float fireRate;
    public Life life;
    public Life playerLife;
    public GameObject photon;
    public bool isPhoton = false;
    public bool isBomb = false;
    public ParticleSystem explosion;

    private NavMeshAgent agent;

    private void Awake()
    {
        baseTransform = GameObject.Find("Base").transform;
        agent = GetComponentInParent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        playerLife = GameObject.FindWithTag("Player").GetComponent<Life>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == EnemyState.GoToBase)
        {
            GoToBase();
        }
        else if (currentState == EnemyState.AttackBase)
        {
            AttackBase();
        }
        else if (currentState == EnemyState.ChasePlayer)
        {
            ChasePlayer();
        }
        else if(currentState == EnemyState.AttackPlayer)
        {
            AttackPlayer();
        }
        else if(currentState == EnemyState.Photonization)
        {
            Photonization();
        }
        else if(currentState == EnemyState.SuicideBombing)
        {
            SuicideBombing();
        }
    }

    void GoToBase()
    {
        agent.isStopped = false;
        agent.SetDestination(baseTransform.position);
        if (isPhoton)
        {
            currentState = EnemyState.Photonization;
        }
        if (sightSensor.detectedObject != null)
        {
            if (!sightSensor.detectedObject.CompareTag("PT"))
            {
                currentState = EnemyState.ChasePlayer;
            }
        }

        float distanceToBase = Vector3.Distance(transform.position, baseTransform.position);
        if (distanceToBase < baseAttackDistance)
        {
            currentState = EnemyState.AttackBase;
        }

        if (life.amount <= 300)
        {
            currentState = EnemyState.SuicideBombing;
        }
    }
    void AttackBase()
    {
        agent.isStopped = true;
        LookTo(baseTransform.position);
        Shoot();
    }
    void ChasePlayer()
    {
        agent.isStopped = false;
        if (isPhoton)
        {
            currentState = EnemyState.Photonization;
        }
        if (sightSensor.detectedObject == null)
        {
            currentState = EnemyState.GoToBase;
            return;
        }
        agent.SetDestination(sightSensor.detectedObject.transform.position);

        float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);
        if(distanceToPlayer <= playerAttackDistance)
        {
            currentState = EnemyState.AttackPlayer;
        }

        if (life.amount <= 300)
        {
            currentState = EnemyState.SuicideBombing;
        }
    }
    void AttackPlayer()
    {
        agent.isStopped = true;
        if (life.amount <= 300)
        {
            currentState = EnemyState.SuicideBombing;
        }
        if (sightSensor.detectedObject == null)
        {
            currentState = EnemyState.GoToBase;
            return;
        }

        LookTo(sightSensor.detectedObject.transform.position);
        Shoot();

        float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);
        if (distanceToPlayer > playerAttackDistance * 1.1f)
        {
            currentState = EnemyState.ChasePlayer;
        }
    }

    void Photonization()
    {
        photon.SetActive(true);
        life.amount += 300;

        currentState = EnemyState.SuicideBombing;
    }

    void SuicideBombing()
    {
        agent.speed += 10;
        agent.SetDestination(GameObject.FindWithTag("Player").transform.position);
        if (isBomb)
        {
            Destroy(transform.root.gameObject);
            Instantiate(explosion, transform.position, Quaternion.identity);
            playerLife.amount -= 300;
        }
    }

    void Shoot()
    {
        var timeSinceLastShoot = Time.time - lastShootTime;
        if (timeSinceLastShoot > fireRate)
        {
            lastShootTime = Time.time;
            Instantiate(bulletPrefab, transform.position, transform.rotation);
        }
    }

    void LookTo(Vector3 targetPosition)
    {
        Vector3 directionToPosition = Vector3.Normalize(targetPosition - transform.parent.position);
        directionToPosition.y = 0;
        transform.parent.forward = directionToPosition;
    }
}
