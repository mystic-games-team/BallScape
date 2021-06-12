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
    [SerializeField]
    Sprite[] enemiesSprites;

    List<Enemy> enemies = new List<Enemy>();

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
        StartCoroutine(Spawn());
        StartCoroutine(Spawn());
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (PlayerController.get.enabled)
        {
            SpawnRandomEnemy();
            yield return new WaitForSecondsRealtime(Random.Range(2.5F, 6.0F));
        }
    }

#if UNITY_EDITOR
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnRandomEnemy();
        }
    }
#endif

    public void SpawnRandomEnemy()
    {
        Collider2D collider = spawnZones[Random.Range(0, spawnZones.Length)];
        Vector3 localPos = new Vector3(Random.Range(0.0f, collider.bounds.size.x), Random.Range(0.0f, collider.bounds.size.y));
        Vector3 position = collider.transform.position + localPos - collider.bounds.size * 0.5f;
        enemies.Add(Instantiate(enemiesGO[Random.Range(0, enemiesGO.Length)], position, enemiesGO[0].transform.rotation, null).GetComponent<Enemy>());
        enemies[enemies.Count - 1].GetComponent<SpriteRenderer>().sprite = enemiesSprites[Random.Range(0, enemiesSprites.Length)];
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
