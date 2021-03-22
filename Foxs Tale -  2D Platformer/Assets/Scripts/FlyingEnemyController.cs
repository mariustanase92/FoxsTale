using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    private int _currentPoint;
    [SerializeField] private float _moveSpeed;
    
    [SerializeField] private float _chaseSpeed;
    [SerializeField] private float _distanceToAttackPlayer;
    [SerializeField] float _waitAfterAttack;
    float _attackCounter;
    private Vector3 _attackTarget;
    
    [SerializeField] private SpriteRenderer _theSR;


    private void Start()
    {
        for (int i = 0; i < _points.Length; i++)
            _points[i].parent = null;
    }

    void Update()
    {
        //Stand
        if (_attackCounter > 0f)
            _attackCounter -= Time.deltaTime;
        
        //Resume Search
        else
        {
            //Roam - no Player in proximity
            if (Vector3.Distance(transform.position, PlayerController.Instance.transform.position) > _distanceToAttackPlayer)
            {
                _attackTarget = Vector3.zero; //reset  target

                transform.position = Vector3.MoveTowards(transform.position, _points[_currentPoint].position, _moveSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, _points[_currentPoint].position) < .05f)
                {
                    _currentPoint++;

                    if (_currentPoint >= _points.Length)
                        _currentPoint = 0;
                }

                //face right
                if (transform.position.x < _points[_currentPoint].position.x)
                    _theSR.flipX = true;
                else if (transform.position.x > _points[_currentPoint].position.x)
                    _theSR.flipX = false;
            }

            //Chase Player
            else
            {
                if (_attackTarget == Vector3.zero) //no target
                {
                    _attackTarget = PlayerController.Instance.transform.position;
                }

                if (transform.position.x < PlayerController.Instance.transform.position.x)
                    _theSR.flipX = true;
                else if (transform.position.x > PlayerController.Instance.transform.position.x)
                    _theSR.flipX = false;

                transform.position = Vector3.MoveTowards(transform.position, _attackTarget, _chaseSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, _attackTarget) < -.1f)
                {
                    _attackCounter = _waitAfterAttack; //wait until search for target again
                    _attackTarget = Vector3.zero; //no target
                }
            }
        }  
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Stompbox")
            gameObject.SetActive(false);
    }
}
