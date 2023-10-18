using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    [SerializeField] int spawnCount;
    [SerializeField] Vector2 spawnAreaStart;
    [SerializeField] Vector2 spawnAreaEnd;

    private void Awake()
    {
        Vector3 point;
        for (int i = 0; i < spawnCount; i++)
        {
            for (int ii = 0; ii < 1000; ii++)
            {
                point = new Vector3(Random.Range(spawnAreaStart.x, spawnAreaEnd.x),
                                    Random.Range(spawnAreaStart.y, spawnAreaEnd.y));
                if (!Physics2D.BoxCast(point, new Vector2(1f, 1f), 0f, Vector2.zero, 0f))
                {
                    Instantiate(enemy, point, Quaternion.identity);
                    break;
                }
            }
        }
    }
}
