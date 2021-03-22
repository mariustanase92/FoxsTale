using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    Slammer _slammerScript;
    private void Awake()
    {
        _slammerScript = GetComponentInParent<Slammer>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Platform" || other.tag == "Object" || other.tag == "Ground")
            _slammerScript._hitGround = true;
        else if (other.tag == "Player")
        {
            LevelManager.Instance.RespawnPlayer();
            AudioManager.Instance.PlaySFX(8);
        }    
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Platform" || other.tag == "Object" || other.tag == "Ground")
            _slammerScript._hitGround = false;
    }

}
