using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool isGem, isHeal;

    private bool isCollected; //to get around not calling onTrigger twice if we have multiple colliders on the player

    public GameObject pickupEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !isCollected)
        {
            if(isGem)
            {
                LevelManager.Instance.gemsCollected++;

                isCollected = true; 
                Destroy(gameObject);

                Instantiate(pickupEffect, transform.position, transform.rotation);

                UIController.Instance.UpdateGemCount();

                AudioManager.Instance.PlaySFX(6);
            }

            if(isHeal)
            {
                if (PlayerHealthController.Instance.GetHealth() != PlayerHealthController.Instance.GetMaxHealth()) 
                {
                    PlayerHealthController.Instance.HealPlayer();
                    isCollected = true;
                    Destroy(gameObject);

                    Instantiate(pickupEffect, transform.position, transform.rotation);
                    AudioManager.Instance.PlaySFX(7);
                }
            }
        }
    }
}
