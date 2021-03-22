using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    [SerializeField] private float _speed = 6f;

    void Start()
    {
        AudioManager.Instance.PlaySFX(2);
    }

    void Update()
    {
        transform.position += new Vector3(-_speed * transform.localScale.x * Time.deltaTime, 0, 0f);   
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            PlayerHealthController.Instance.DealDamage();
        }

        AudioManager.Instance.PlaySFX(1);

        Destroy(gameObject);
    }
}
