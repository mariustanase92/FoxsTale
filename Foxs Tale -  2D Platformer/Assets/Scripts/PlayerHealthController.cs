using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController Instance; //only 1 of this script exists in the scene, if we make more, all the values will be the same to all of them
                                                   //static vars do not show in the inspector

    [HideInInspector] public int currentHealth;
    [SerializeField] private int maxHealth;

    public static Action OnPlayerDied;
    public static void CallOnPlayerDied() { OnPlayerDied?.Invoke(); }

    public float invincibleLength;
    private float invinicibleCounter;

    private SpriteRenderer theSR;

    public GameObject deathEffect;


    private void Awake()
    {
        Instance = this; //make this script the one Unity will look for in the memory when we reffer to this instance
    }

    private void Start()
    {
        currentHealth = maxHealth;

        theSR = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(invinicibleCounter > 0)
        {
            invinicibleCounter -= Time.deltaTime;

            if(invinicibleCounter <= 0)
            {
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 1f);
            }
        }
    }

    public void DealDamage()
    {
        if (invinicibleCounter <= 0)
        {
            currentHealth--;

            if (currentHealth <= 0)
            {
                CallOnPlayerDied();
                currentHealth = 0;

                Instantiate(deathEffect, transform.position, transform.rotation);

                AudioManager.Instance.PlaySFX(8);

                LevelManager.Instance.RespawnPlayer();
            }
            else
            {
                invinicibleCounter = invincibleLength;
                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, .5f);

                PlayerController.Instance.KnockBack();

                AudioManager.Instance.PlaySFX(9);
            }

            UIController.Instance.UpdateHealthDisplay();
        }
    }

    public void HealPlayer()
    {
        currentHealth++;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UIController.Instance.UpdateHealthDisplay();
    }

    public int GetPlayerHealth()
    {
        return currentHealth;
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

}
