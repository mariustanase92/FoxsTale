using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public static CheckpointController Instance;

    private Checkpoint[] checkpoints;

    private Vector3 spawnPoint;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        checkpoints = FindObjectsOfType<Checkpoint>(); //find all<> ACTIVE in the scene

        spawnPoint = PlayerController.Instance.transform.position;
    }

    public void DeactivateCheckpoints()
    {
        //call Reset on all the scripts
        for(int i = 0; i < checkpoints.Length; i++)
        {
            checkpoints[i].ResetCheckpoint();
        }
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        if (spawnPoint != newSpawnPoint)
            AudioManager.Instance.PlaySFX(11);

        spawnPoint = newSpawnPoint;  
    }

    public Vector3 GetSpawnPoint()
    {
        return spawnPoint;
    }
}
