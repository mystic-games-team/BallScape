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
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            GetComponent<Collider2D>().enabled = false;
            UIHUDKills.get.AddKill();
            StartCoroutine(Die());
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
        return currentLife > 0 && PlayerController.get.enabled;
    }

    IEnumerator Die()
    {
        yield return new WaitForSecondsRealtime(1.5F);

        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        SpriteRenderer childSprite = null;
        if (gameObject.transform.childCount > 1)
        {
            childSprite = gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>();
        }

        while (sprite.color.a > 0)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - Time.deltaTime);
            if (childSprite != null) childSprite.color = new Color(childSprite.color.r, childSprite.color.g, childSprite.color.b, childSprite.color.a - Time.deltaTime);
            yield return null;
        }

        EnemyManager.instance.DeleteEnemy(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!PlayerController.get.enabled)
        {
            return;
        }

        if (collision.gameObject.tag.CompareTo("Ball") == 0)
        {
            DecreaseLife(1);
        }
    }
}
