using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlane : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            LevelManager.Instance.RespawnPlayer();
            AudioManager.Instance.PlaySFX(8);
        }
    }
}
