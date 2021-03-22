using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankHitbox : MonoBehaviour
{
    private BossTankController _bossTankController;

    private void Awake()
    {
        _bossTankController = GetComponentInParent<BossTankController>();   
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && PlayerController.Instance.transform.position.y > transform.position.y)
        {
           
            if(_bossTankController._currentState == BossTankController.bossStates.shooting)
                _bossTankController.TakeHit();

            PlayerController.Instance.Bounce();

            //gameObject.SetActive(false);
        }
    }
}
