using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlammerBlock : MonoBehaviour
{
    private Rigidbody2D _theRB;
    void Start()
    {
        _theRB = GetComponent<Rigidbody2D>();
        _theRB.bodyType = RigidbodyType2D.Kinematic;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Platform" || other.tag == "Object")
        {
            
            _theRB.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Platform" || other.tag == "Object")
        {

            _theRB.bodyType = RigidbodyType2D.Kinematic;
        }
    }
}
