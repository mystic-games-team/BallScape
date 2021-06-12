using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    static public PlayerController get { private set; get; }

    [Header("Movement")]
    [SerializeField]
    float speed = 5.0f;

    [Header("Ball")]
    [SerializeField] Rigidbody2D ball;
    [SerializeField] float force = 10.0F;

    [Header("Damage")]
    [SerializeField] Color colorDamage;
    [SerializeField] float damageCooldown = 2.0F;

    Rigidbody2D rb;
    SpriteRenderer sprite;
    Camera camera;
    Coroutine currentColorCoroutine = null;

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
        if (currentColorCoroutine == null)
        {
            currentColorCoroutine = StartCoroutine(ChangeLifeAnimation(colorDamage));
            UIHUDLifeBar.get.DecreaseLifes(damage, OnDead);
        }
    }

    IEnumerator ChangeLifeAnimation(Color animColor)
    {
        int cycles = 2;
        int currentCycles = 0;
        float timeStart = Time.time;

        while (currentCycles < cycles)
        {
            while (Time.time - timeStart < damageCooldown / cycles)
            {
                float t = (Time.time - timeStart) / 0.4f;

                if (t <= 0.5f)
                {
                    sprite.color = Color.Lerp(Color.white, animColor, t * 2);
                }
                else if (t > 0.5f)
                {
                    sprite.color = Color.Lerp(animColor, Color.white, (t - 0.5f) * 2);
                }

                yield return null;
            }

            timeStart = Time.time;
            ++currentCycles;
        }

        currentColorCoroutine = null;
    }

    void OnDead()
    {
        enabled = false;
    }
}
