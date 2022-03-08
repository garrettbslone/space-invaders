using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    private List<GameObject> _parts;

    // Start is called before the first frame update
    void Start()
    {
        _parts = new List<GameObject>();
        
        foreach (var part in GameObject.FindGameObjectsWithTag("Shield Part"))
        {
            if (part.transform.IsChildOf(transform))
                _parts.Add(part);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePart(GameObject part)
    {
        _parts.Remove(part);
        
        if (_parts.Count == 0)
        {
            Destroy(gameObject);
        }
    }
}
