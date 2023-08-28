using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField]
    public int maxHealth;
    [SerializeField]
    public int damage;
    [SerializeField]
    public float speed;
    [SerializeField]
	public int expDropped;

    public int currentHealth;

}
