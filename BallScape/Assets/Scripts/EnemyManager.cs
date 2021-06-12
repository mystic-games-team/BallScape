using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    static public EnemyManager instance;

    [Header("Enemies")]
    [SerializeField]
    GameObject[] enemiesGO;
    [SerializeField]
    Collider2D[] spawnZones;

    List<Enemy> enemies = new List<Enemy>();

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnRandomEnemy();
        }
    }

    public void SpawnRandomEnemy()
    {
        Collider2D collider = spawnZones[Random.Range(0, spawnZones.Length)];
        Vector3 localPos = new Vector3(Random.Range(0.0f, collider.bounds.size.x), Random.Range(0.0f, collider.bounds.size.y));
        Vector3 position = collider.transform.position + localPos - collider.bounds.size * 0.5f;
        enemies.Add(Instantiate(enemiesGO[Random.Range(0, enemiesGO.Length)], position, enemiesGO[0].transform.rotation, null).GetComponent<Enemy>());
    }

    public void DeleteEnemy(Enemy enemy)
    {
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
        }

        Destroy(enemy.gameObject);
    }
}
