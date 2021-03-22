using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldCameraController : MonoBehaviour
{
    [SerializeField] private Vector2 _minPos, _maxPos;
    [SerializeField] private Transform _followTarget;

    private void LateUpdate()
    {
        float xPos = Mathf.Clamp(_followTarget.position.x, _minPos.x, _maxPos.x);
        float yPos = Mathf.Clamp(_followTarget.position.y, _minPos.y, _maxPos.y);
        Vector3 newPos = new Vector3(_followTarget.position.x, _followTarget.position.y, transform.position.z);

        //transform.position = new Vector3(xPos, yPos, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, newPos, 1f);
        
    }

}
