using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    static public PlayerController get { private set; get; }

    [Header("Movement")]
    [SerializeField]
    float speed = 5.0f;
    [SerializeField]
    ScriptableObject behaviour;

    [Header("Ball")]
    [SerializeField] Rigidbody2D ball;
    [SerializeField] float force = 10.0F;

    Rigidbody2D rb;
    SpriteRenderer sprite;
    Camera camera;

    private void Awake()
    {
        get = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        camera = Camera.allCameras[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPoint = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
            Vector2 direction = new Vector2(worldPoint.x, worldPoint.y) - ball.position;
            ball.AddForce(direction.normalized * force, ForceMode2D.Force);
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W)) { direction.y += 1; }
        if (Input.GetKey(KeyCode.S)) { direction.y -= 1; }

        if (Input.GetKey(KeyCode.A)) { direction.x -= 1; }
        if (Input.GetKey(KeyCode.D)) { direction.x += 1; }

        if (direction != Vector2.zero)
        {
            rb.position += direction.normalized * Time.fixedDeltaTime * speed;
            rb.rotation = Vector2.SignedAngle(Vector2.down, direction.normalized);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag.CompareTo("Enemy") == 0)
        {
            RecieveDamage(collision.gameObject.GetComponent<Enemy>().damageOnHit);
        }
        else if (collision.gameObject.tag.CompareTo("Projectile") == 0)
        {

        }
    }

    public void RecieveDamage(int damage)
    {
        // TODO: active anim

        UIHUDLifeBar.get.DecreaseLifes(damage, OnDead);
    }


    void OnDead()
    {

    }
}
