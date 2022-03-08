using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public delegate void OnShotDelegate(int livesRemaining, GameObject bullet);
    public OnShotDelegate onShot;
    public float speed = 1f;
    public GameObject bullet;

    private Vector3 _pos;
    private int _livesRemaining = 3;

    // Start is called before the first frame update
    void Start()
    {
        _pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float axis = Input.GetAxis("Horizontal");
        float x = axis * speed * Time.deltaTime + transform.position.x;
        transform.position = new Vector3(x, transform.position.y, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }

    public void ResetPlayer()
    {
        transform.position = _pos;
        _livesRemaining = 3;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Player hit, {_livesRemaining - 1} lives remaining\n");
        if (other.CompareTag("Enemy Bullet"))
        {
            onShot?.Invoke(--_livesRemaining, other.gameObject);
        }
    }
    
    public void Fire()
    {
        var b = Instantiate(bullet, transform.position, Quaternion.identity);
        b.GetComponent<BulletController>().direction = -1;
        Destroy(b, 3f);
    }
}

