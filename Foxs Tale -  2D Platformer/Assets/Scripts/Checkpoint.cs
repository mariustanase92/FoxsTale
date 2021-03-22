using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public SpriteRenderer theSR;

    public Sprite cpOn, cpOff;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            CheckpointController.Instance.DeactivateCheckpoints();
            
            theSR.sprite = cpOn;

            CheckpointController.Instance.SetSpawnPoint(transform.position);
           
        }
    }

    public void ResetCheckpoint()
    {
        theSR.sprite = cpOff;
    }
}
