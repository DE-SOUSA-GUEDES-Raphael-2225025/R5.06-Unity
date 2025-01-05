using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] GameObject monstersToSpawn;
    [SerializeField] int maxAmount;
    [SerializeField] int minAmount;
    [SerializeField] int minDelayToSpawn;
    [SerializeField] int maxDelayToSpawn;

    private float delay = 10;

    void Update()
    {
        if (delay > 0) {
            delay -= Time.deltaTime;
        } else {
            delay = Random.Range(minDelayToSpawn, maxDelayToSpawn);
            StartCoroutine(SpawnEnnemies());
        }
    }

    IEnumerator SpawnEnnemies() {
        int ennemieToSpawn = Random.Range(minAmount, maxAmount);
        for (int i = 0; i < ennemieToSpawn; i++) {
            Instantiate(monstersToSpawn, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            yield return new WaitForSeconds(1f);
        }
        yield return null;
    }
}
