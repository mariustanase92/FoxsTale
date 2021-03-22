using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    //private Rigidbody2D _theRB;
    //[SerializeField] private float _knockBackForce = 1f;
    //private void Awake()
    //{
        //_theRB = GetComponent<Rigidbody2D>();
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            PlayerHealthController.Instance.DealDamage();
    }
}
