using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField]
    float speed = 5.0f;
    [SerializeField]
    ScriptableObject behaviour;

    Rigidbody2D rb;
    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
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
}
