using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    private Animator _anim;
    [SerializeField] private float bounceForce = 20f;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            PlayerController.Instance.theRB.velocity = new Vector2(PlayerController.Instance.theRB.velocity.x, bounceForce);
            AudioManager.Instance.PlaySFX(10);
            _anim.SetTrigger("Bounce");
        }
    }
}
