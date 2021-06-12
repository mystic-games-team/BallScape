using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : Enemy
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!CanUpdate())
        {
            return;
        }

        Vector3 direction = (PlayerController.get.transform.position - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            rigidbody.position += new Vector2(direction.x, direction.y).normalized * Time.fixedDeltaTime * speed;
            rigidbody.rotation = Vector2.SignedAngle(Vector2.down, direction.normalized);
        }
    }
}
