using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public delegate void OnEnemyHit(GameObject enemy, GameObject bullet, int points);
    public OnEnemyHit onEnemyHit;
    public int points;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player Bullet"))
        {
            onEnemyHit?.Invoke(gameObject, other.gameObject, points);
            Destroy(other);
            Destroy(gameObject);
        }
    }

    public void Fire()
    {
        var b = Instantiate(bullet, transform.position, Quaternion.identity);
        Destroy(b, 3f);
    }
}
