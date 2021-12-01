using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    float speed = 0;
    Rigidbody2D rb;
    public void Shoot(Vector2 dir, float speed)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = dir * speed;
        this.speed = speed * 0.8f;
    }
    private void Update()
    {
        if (rb.velocity.magnitude < speed)
            gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collison with: "+collision.gameObject.name);
        if (collision.collider.tag == "Wall")// || collision.collider.tag == "Bullet")
            gameObject.SetActive(false);
    }
}
