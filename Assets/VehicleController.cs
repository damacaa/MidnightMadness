using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    public static VehicleController selectedVehicle = null;

    public float speed = 10f;
    public float handling = 1f;
    public float drifFactor = 0.95f;

    float angle = 0f;

    Rigidbody2D rb;

    public Transform[] seats;
    int ocupantCount = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
    }

    public void Move(float x, float y)
    {
        rb.AddForce(transform.up * speed * y, ForceMode2D.Force);

        float turnFactor = Mathf.Clamp01(rb.velocity.magnitude / 8);

        angle += handling * -x * turnFactor * Mathf.Sign(Vector2.Dot(rb.velocity, transform.up));
        rb.MoveRotation(angle);

        Vector2 forwardVelocity = transform.up * Vector2.Dot(rb.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(rb.velocity, transform.right);

        rb.velocity = forwardVelocity + (drifFactor * rightVelocity);

        //rb.AddTorque(handling * -x * rb.velocity.magnitude * Vector3.Dot(transform.up, rb.velocity));
    }

    public bool SetDriver(out Transform transform)
    {
        if (ocupantCount > 0)
        {
            transform = null;
            return false;
        }

        rb.isKinematic = false;
        transform = seats[0];
        ocupantCount++;
        return true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
