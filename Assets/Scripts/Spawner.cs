using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject[] enemyReference;

    private GameObject spawnedEnemy;
    
    [SerializeField]
    private Transform leftPos, rightPos;

    private int randomIndex;
    private int randomSide;
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    IEnumerator SpawnEnemies() {
        while (true) {
            yield return new WaitForSeconds(Random.Range(1, 3));
            randomIndex = Random.Range(0, enemyReference.Length);
            randomSide = Random.Range(0, 2);
            spawnedEnemy = Instantiate(enemyReference[randomIndex]);

            if (randomSide == 0)
            {
                spawnedEnemy.transform.position = leftPos.position;
                spawnedEnemy.GetComponent<Enemy>().speed = Random.Range(4, 10);
                //spawnedEnemy.GetComponent<Enemy>().sr.flipX = false;
            }
            else
            {
                spawnedEnemy.transform.position = rightPos.position;
                spawnedEnemy.GetComponent<Enemy>().speed = -Random.Range(4, 10);
                spawnedEnemy.transform.localScale = new Vector3(-spawnedEnemy.transform.localScale.x, spawnedEnemy.transform.localScale.y, spawnedEnemy.transform.localScale.z);
                //spawnedEnemy.GetComponent<Enemy>().sr.flipX = true;
            }
        }
    }
}
