using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slammer : MonoBehaviour
{
    [SerializeField] private GameObject _smasherObject;
    [SerializeField] LayerMask _whatIsGround;
    public bool _hitGround = false;

    [SerializeField] private Transform _slamTarget;
    private SlamTarget _slamTargetScript;

    [SerializeField] private float _slamSpeed;
    [SerializeField] private float _waitAfterSlam;
    private bool _isGoingDown = false;
    


    private void Start()
    {
        _slamTarget.parent = null;
        _slamTargetScript = _slamTarget.GetComponent<SlamTarget>();
        _slamTargetScript.OnPlayerInRange += PlayerInRange;
    }

    private void PlayerInRange(object sender, System.EventArgs e)
    {
        InvokeRepeating("GroundPound", 0, Time.deltaTime);
    }

    private void ReturnToOriginalPosition()
    {    
        if (_isGoingDown)
            CancelInvoke("ReturnToOriginalPosition");
        else
        {
            _smasherObject.transform.position = Vector3.MoveTowards(_smasherObject.transform.position, transform.position, _slamSpeed * .5f * Time.deltaTime);

            if (Vector3.Distance(_smasherObject.transform.position, transform.position) < .01f)
                CancelInvoke("ReturnToOriginalPosition");
        }
    }

    private void GroundPound()
    {
        _isGoingDown = true;
        _smasherObject.transform.position = Vector3.MoveTowards(_smasherObject.transform.position, _slamTarget.position, _slamSpeed * Time.deltaTime);

        if (_hitGround || Vector3.Distance(_smasherObject.transform.position, _slamTarget.position) < .01f)
        {          
            CancelInvoke("GroundPound");
            _isGoingDown = false;
            StartCoroutine(GroundPoundCooldownCo());           
        }
    }

    private IEnumerator GroundPoundCooldownCo()
    {
        AudioManager.Instance.PlaySFX(9);
        yield return new WaitForSeconds(_waitAfterSlam);
        InvokeRepeating("ReturnToOriginalPosition", 0, Time.deltaTime);
    }
}
