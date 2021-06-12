using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float speed = 5.0F;
    [SerializeField] int maxLife = 1;
    public int damageOnHit = 1;

    int currentLife = 0;

    protected new Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        currentLife = maxLife;
    }

    void DecreaseLife(int amount)
    {
        currentLife -= amount;
        if (currentLife <= 0)
        {
            GetComponent<Collider2D>().enabled = false;
            UIHUDKills.get.AddKill();
        }
    }

    private void OnBecameInvisible()
    {
        if (currentLife <= 0)
        {
            EnemyManager.instance.DeleteEnemy(this);
        }
    }

    protected bool CanUpdate()
    {
        return currentLife > 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.CompareTo("Ball") == 0)
        {
            DecreaseLife(1);
        }
    }
}
