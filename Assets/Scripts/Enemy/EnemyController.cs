using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyController : MonoBehaviour {

    [SerializeField]
	public float seekRadius;
	public Transform centrePoint;

	PlayerStats targetStats;
	Transform targetTransform;
	NavMeshAgent agent;
    private EnemyStats stats;
	public float patrolRadius = 320f;

	void Start()
	{
		centrePoint = GameObject.Find("PatrolCenter").transform;
        stats = GetComponent<EnemyStats>();
		targetStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
		targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
		agent = GetComponent<NavMeshAgent>();
        agent.speed = stats.speed;
	}

	void Update () {

		float distance = Vector3.Distance(targetTransform.position, transform.position);
		if (distance <= seekRadius) {
			agent.SetDestination(targetTransform.position);
			if (distance <= agent.stoppingDistance)
			{
				FaceTarget();
			}   
		} else {
			if(agent.remainingDistance <= agent.stoppingDistance)  {
				Vector3 point;
				if (RandomPoint(centrePoint.position, patrolRadius, out point))
				{
					Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
					agent.SetDestination(point);
				}
			}
		}

		if (stats.currentHealth <= 0) {
			Die();
		}

    }

	bool RandomPoint(Vector3 center, float patrolRadius, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * patrolRadius;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;

    }

	void FaceTarget () {

		Vector3 direction = (targetTransform.position - transform.position).normalized;
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

	}

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, seekRadius);
	}

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
			DealDamage();
        }
    }

	private void DealDamage() {
		targetStats.TakeDamage(stats.damage);
	}

	private void Die() {
		targetStats.GainExp(stats.expDropped);
		Destroy(gameObject);
	}

}