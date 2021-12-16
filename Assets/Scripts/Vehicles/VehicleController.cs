using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    public static VehicleController selectedVehicle = null;

    public float speed = 10f;
    public float handling = 1f;
    public float drifFactor = 0.95f;
    public Color paintColor;
    public SpriteRenderer[] colorChangingParts;
    public float Speed
    {
        get
        {
            return rb.velocity.magnitude;
        }
    }

    float angle = 0f;

    Rigidbody2D rb;

    public Transform[] seats;
    CharacterController[] passengers;

    int ocupantCount = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        passengers = new CharacterController[seats.Length];

        foreach (SpriteRenderer s in colorChangingParts)
        {
            s.color = paintColor;
        }
    }

    public void Move(float x, float y)
    {
        if (y < 0 && Vector2.Dot(rb.velocity, transform.up) > 0)
            return;

        rb.AddForce(transform.up * speed * y, ForceMode2D.Force);

        float turnFactor = Mathf.Clamp01(rb.velocity.magnitude / 8);

        angle += handling * -x * turnFactor * Mathf.Sign(Vector2.Dot(rb.velocity, transform.up));
        rb.MoveRotation(angle);

        Vector2 forwardVelocity = transform.up * Vector2.Dot(rb.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(rb.velocity, transform.right);

        rb.velocity = forwardVelocity + (drifFactor * rightVelocity);
    }

    public bool GetIn(CharacterController character, out Transform transform)
    {
        for (int i = 0; i < passengers.Length; i++)
        {
            if (!passengers[i])
            {
                passengers[i] = character;
                transform = seats[i];
                return true;
            }
        }

        transform = null;
        return false;
    }

    public void GetOut(CharacterController character)
    {
        for (int i = 0; i < passengers.Length; i++)
        {
            if (passengers[i] == character)
            {
                passengers[i] = null;
                return;
            }
        }
    }

    float minVehicleSpeed = 0.1f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<EnemyBehaviour>(out EnemyBehaviour enemy) && Speed > minVehicleSpeed)
        {
            Vector3 dir = enemy.transform.position - transform.position;
            enemy.Stun(5, dir.normalized * Speed * 0.5f);
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        foreach (SpriteRenderer s in colorChangingParts)
        {
            s.color = paintColor;
        }
    }
#endif
}
