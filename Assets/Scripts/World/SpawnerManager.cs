using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private LightingManager lightingManager;
    [SerializeField]
    private List<GameObject> enemies = new List<GameObject>(); // The prefab to spawn
    private Vector3 xRange; // Range for x-axis
    private float y = 1.2f;
    private Vector3 zRange; // Range for z-axis
    private int enemyType;

    [SerializeField]
    public int coolDown;

    private float timeToWaitTo;


    void Start() {
        lightingManager = GetComponent<LightingManager>();
        timeToWaitTo = 0f;
    }

    private void Update() {
        if (Time.time >= timeToWaitTo) {
            if (lightingManager.timeOfDay < 18) {
                enemyType = 0;
            } else {
                enemyType = 1;
            }
            Vector3 spawnManagerPosition = transform.position;
            float xRange = Random.Range(-105.0f, 105.0f);
            float zRange = Random.Range(-130.0f, 130.0f);
            Vector3 spawnLocation = new Vector3(spawnManagerPosition.x + xRange, y, spawnManagerPosition.z + zRange);
            Spawn(spawnLocation, enemyType);
            timeToWaitTo = Time.time + coolDown;
        }
    }

    private void Spawn(Vector3 spawnLoc, int enemyType) {
        Instantiate(enemies[enemyType], spawnLoc, Quaternion.identity);
    }
}