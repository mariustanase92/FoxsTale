using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivator : MonoBehaviour
{
    [SerializeField] private GameObject _theBossBattle;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            _theBossBattle.SetActive(true);

            AudioManager.Instance.PlayBossMusic();

            gameObject.SetActive(false);
        }
    }
}
