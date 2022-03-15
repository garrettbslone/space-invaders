using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public int direction = 1;
    public float speed = 3f;
    public bool flip = false;

    private Rigidbody2D _rigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        if (flip)
            transform.rotation = Quaternion.Euler(180f, 0f, 0f);
        _rigidbody = GetComponent<Rigidbody2D>();
        Fire();
    }

    void Fire()
    {
        _rigidbody.velocity = Vector2.down * direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Shield Part") && !other.CompareTag("Player Bullet") &&
            !other.CompareTag("Enemy Bullet")) return;
        Destroy(other.gameObject);
        Destroy(gameObject);
    }

}
