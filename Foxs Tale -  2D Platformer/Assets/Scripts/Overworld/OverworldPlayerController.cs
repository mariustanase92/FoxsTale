using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldPlayerController : MonoBehaviour
{
    [SerializeField] private LevelSelectManager _levelSelectManager;

    public MapPoint currentPoint;
    private MapPoint previousPoint;
    public float moveSpeed = 10f;
    private bool hasDestination = false;
    private bool levelLoading = false;

    private void Awake()
    {
        //previousPoint = currentPoint;
    }

    private void Update()
    {
        
        //Set destination
        if (!hasDestination && !levelLoading && !currentPoint.IsLevelLocked())
        {
            if (Input.GetAxisRaw("Horizontal") > .9f)
            {
                if (currentPoint.right != null)
                {
                    SetNextPoint(currentPoint.right);
                    //SetPreviousPoint(previousPoint.left);
                    hasDestination = true;
                }
            }
            else if (Input.GetAxisRaw("Horizontal") < -.9f)
            {
                if (currentPoint.left != null)
                {
                    SetNextPoint(currentPoint.left);
                    //SetPreviousPoint(previousPoint.right);
                    hasDestination = true;
                }
            }
            else if (Input.GetAxisRaw("Vertical") > .9f)
            {
                if (currentPoint.up != null)
                {
                    SetNextPoint(currentPoint.up);
                    //SetPreviousPoint(previousPoint.down);
                    hasDestination = true;
                }
            }
            else if (Input.GetAxisRaw("Vertical") < -.9f)
            {
                if (currentPoint.down != null)
                {
                    SetNextPoint(currentPoint.down);
                    //SetPreviousPoint(previousPoint.up);
                    hasDestination = true;
                }
            }
        }

        //Move
        if (hasDestination && !levelLoading)
            transform.position = Vector3.MoveTowards(transform.position, currentPoint.transform.position, moveSpeed * Time.deltaTime);

        //Reached destination
        if (Vector3.Distance(transform.position, currentPoint.transform.position) < 0.1f && !levelLoading)
        {
            hasDestination = false;
            

            if (currentPoint._isLevel && currentPoint.levelToLoad != "")
            {
                OverworldUIController.Instance.ShowInfo(currentPoint);
                if (Input.GetButtonDown("Jump"))
                {
                    levelLoading = true;
                    _levelSelectManager.LoadLevel();
                }
            }
        }  
    }

    private void SetNextPoint(MapPoint nextPoint)
    {
        currentPoint = nextPoint;
        OverworldUIController.Instance.HideInfo();

        AudioManager.Instance.PlaySFX(5);
    }

    private void SetPreviousPoint(MapPoint lastPoint)
    {
        previousPoint = lastPoint;
    }

}
