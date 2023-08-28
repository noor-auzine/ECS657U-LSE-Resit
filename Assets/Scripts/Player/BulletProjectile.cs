using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour {

    private Rigidbody bulletRigidbody;
    private EnemyStats enemyStats;
    private int damage = 20;

    private void Awake() {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void Start() {
        float speed = 50f;
        bulletRigidbody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<EnemyStats>() != null) {
            enemyStats = other.GetComponent<EnemyStats>();
            enemyStats.currentHealth -= damage;
        }
        Destroy(gameObject);
    }

}