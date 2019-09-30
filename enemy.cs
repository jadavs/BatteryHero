using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{
    public float health = 50f;
    public GameObject loot;
    public float y_position = 6;
    public GameObject reactor;

    void Start() {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.destination = reactor.transform.position;
    }

    public void TakeDamage(float dmg) {
        health -= dmg;
        if (health <= 0f) {
            Die();
        }

    }

    void Die () {
        Instantiate(loot, transform.position + new Vector3(0, y_position, 0), transform.rotation);
        Destroy(gameObject);
    }
}
