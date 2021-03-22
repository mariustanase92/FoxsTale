using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//There are layers that wont interact with this Script (Project Settings -> Physics2D)
public class Stompbox : MonoBehaviour
{
    public GameObject deathEffect;

    public GameObject collectible;
    [Range(0, 100)] public float chanceToDrop; //drop chance

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            PlayerController.Instance.Bounce();
            //other.transform.gameObject.SetActive(false);

            Instantiate(deathEffect, other.transform.position, other.transform.rotation);
            
            float dropSelect = Random.Range(0, 100f);

            if (dropSelect <= chanceToDrop)
            {
                Instantiate(collectible, other.transform.position, other.transform.rotation);
            }

            AudioManager.Instance.PlaySFX(3);
        }
        
        if(other.tag == "Boss")
        {
            float dropSelect = Random.Range(0, 100f);

            if (dropSelect <= chanceToDrop)
            {
                Instantiate(collectible, other.transform.position, other.transform.rotation);
            }
        }
    }
}
