using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletLifetime = 2.0f;

    float timeAlive = 0;
    private void Update()
    {
        if ((timeAlive += Time.deltaTime) >= bulletLifetime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.CompareTo("MainCamera") == 0 || collision.tag.CompareTo("Enemy") == 0)
            return;

        Destroy(gameObject);
    }
}
