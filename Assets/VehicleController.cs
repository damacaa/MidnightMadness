using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    public float speed = 10f;
    public float handling = 1f;
    public Transform[] seats;
    int ocupantCount = 0;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
    }

    public void Move(float x, float y)
    {
        rb.AddForce(transform.up * speed * y);
        rb.AddTorque(handling * -x * rb.velocity.magnitude * Vector3.Dot(transform.up, rb.velocity));
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
