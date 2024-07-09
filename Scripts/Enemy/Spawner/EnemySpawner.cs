using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemyPrefab;
    public string enemyTagName;
    public int enemyMaxCount;
    public int enemyCount = 0;
    public float spawnDelay;
    [HideInInspector]
    public float timer;

    GameObject objToSpawn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            objToSpawn = new GameObject(enemyTagName);
            objToSpawn.tag = enemyTagName;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            timer += Time.deltaTime;
            if (timer > spawnDelay && enemyCount < enemyMaxCount)
            {
                timer = 0;
                ++enemyCount;
                int randSpawnPoint = Random.Range(0, spawnPoints.Length);
                GameObject temp = Instantiate(enemyPrefab, spawnPoints[randSpawnPoint].position, transform.rotation);
                temp.transform.SetParent(GameObject.FindWithTag(enemyTagName).transform);
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (GameObject.FindWithTag(enemyTagName))
            {
                EnemyDestroy();
            }
        }
    }

    void EnemyDestroy()
    {
        Destroy(GameObject.FindWithTag(enemyTagName));
        enemyCount = 0;
    }
}