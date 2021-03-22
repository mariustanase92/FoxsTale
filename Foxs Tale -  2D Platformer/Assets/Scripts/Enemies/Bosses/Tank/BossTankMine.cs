using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankMine : MonoBehaviour
{
    [SerializeField] private GameObject _explosion;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Destroy(gameObject);

            Instantiate(_explosion, transform.position, transform.rotation);

            PlayerHealthController.Instance.DealDamage();

            AudioManager.Instance.PlaySFX(3);
        }
    }

    public void Explode()
    {
        Destroy(gameObject);

        AudioManager.Instance.PlaySFX(3);

        Instantiate(_explosion, transform.position, transform.rotation);
    }
}
