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

    private Animator _animator;
    private GameObject _s, _e;
    private static readonly int Shooting = Animator.StringToHash("Shooting");
    private static readonly int Exploding = Animator.StringToHash("Exploding");

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        // _s = GetComponent<GameObject>("s");
        foreach (var t in GetComponentsInChildren<Transform>())
        {
            if (t.gameObject.name == "e")
            {
                _e = t.gameObject;
            } else if (t.gameObject.name == "s")
            {
                _s = t.gameObject;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player Bullet"))
        {
            _e.GetComponent<SpriteRenderer>().enabled = true;
            _animator.SetTrigger(Exploding);
            onEnemyHit?.Invoke(gameObject, other.gameObject, points);
            Destroy(other);
            Destroy(gameObject);
        }
    }

    public void Fire()
    {
        _s.GetComponent<SpriteRenderer>().enabled = true;
        _animator.SetTrigger(Shooting);
        var b = Instantiate(bullet, transform.position, Quaternion.identity);
        Destroy(b, 3f);
    }
}
