using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : Enemy
{
    enum States
    {
        Following,
        Shooting
    }

    [Header("Shooter Values")]
    [SerializeField] float shootingDistance = 3.0f;
    [SerializeField] float chargeTime = 0.5f;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletExit;
    [SerializeField] float bulletForce = 3000.0f;
    [SerializeField] float shootingCooldown = 2.0f;

    States currentState = States.Following;
    float currentChargingTime = 0;

    float currentCooldown = 2;
    bool isVisible = false;

    private void FixedUpdate()
    {
        if (!CanUpdate())
        {
            return;
        }

        currentCooldown += Time.fixedDeltaTime;

        Vector2 playerPos = PlayerController.get.rb.position;
        Vector3 direction = (playerPos - rigidbody.position).normalized;
        rigidbody.rotation = Vector2.SignedAngle(Vector2.down, direction.normalized);

        switch (currentState)
        {
            case States.Following:
                if (direction != Vector3.zero)
                {
                    rigidbody.position += new Vector2(direction.x, direction.y).normalized * Time.fixedDeltaTime * speed;

                    if (Vector2.Distance(rigidbody.position, playerPos) <= shootingDistance && isVisible && shootingCooldown <= currentCooldown)
                    {
                        currentState = States.Shooting;
                    }
                }
                break;
            case States.Shooting:
                if ((currentChargingTime += Time.deltaTime) >= chargeTime)
                {
                    Shoot();
                }
                break;
        }
    }

    public void Shoot()
    {
        currentCooldown = 0;
        currentChargingTime = 0;
        Rigidbody2D b = Instantiate(bullet, bulletExit.position, bullet.transform.rotation, null).GetComponent<Rigidbody2D>();
        b.rotation = rigidbody.rotation + 90;
        b.AddForce((PlayerController.get.rb.position - rigidbody.position).normalized * bulletForce);
        currentState = States.Following;
        CameraEffects.get.ShakeCamera(2, 0.1f);
    }

    private void OnBecameVisible()
    {
        isVisible = true;
    }

    private void OnBecameInvisible()
    {
        isVisible = false;
    }
}
