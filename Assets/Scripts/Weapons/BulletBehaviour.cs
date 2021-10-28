using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public void Shoot(Vector2 dir, float speed)
    {
        GetComponent<Rigidbody2D>().velocity = dir * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collison with: "+collision.gameObject.name);
        if (collision.collider.tag == "Wall")
            Destroy(gameObject);
    }
}
