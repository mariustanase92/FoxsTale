using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTankController : MonoBehaviour
{
    [HideInInspector] public enum bossStates
    {
        shooting,
        hurt,
        moving,
        ended
    };

    public bossStates _currentState;
    
    [Header ("Movement")]
    [SerializeField] private float _defaultSpeed = 5f;
    private float _moveSpeed = 5f;
    private Transform _leftPoint, _rightPoint;
    private bool _isMovingRight;
    private Vector3 _originalPos;

    private Transform _theBoss;
    private Animator _anim;

    [Header ("Bullet")]
    [SerializeField] private GameObject _bulletObj;
    private Transform _firePoint;
    [SerializeField] private float _defaultTimeBetweenShots = .7f;
    private float _timeBetweenShots = .7f;
    private float _shotCounter;

    [Header("Mine")]
    [SerializeField] private GameObject _mineObj;
    private Transform _minePoint;
    [SerializeField] private float _defaultTimeBetweenMines = 1.1f;
    private float _timeBetweenMines = 1.1f;
    private float _mineCounter;

    [Header("Health")]
    [SerializeField] private int _defaultHP = 5;
    private int _currentHP = 5;
    [SerializeField] private float _currHurtTime = .9f;
    private float _hurtCounter;
    private Transform _hitBox;
    private bool _isDefeated;
    [SerializeField] private GameObject _explosion;

    [Header("Difficulty")]
    [SerializeField] private float _shotSpeedUp = 1.2f;
    [SerializeField] private float _mineSpeedUp = 1.2f;
    [SerializeField] private List<GameObject> _activateObjects = new List<GameObject>();

    private void Awake()
    {
        _leftPoint = GameObject.Find("leftPoint").gameObject.GetComponent<Transform>();
        _rightPoint = GameObject.Find("rightPoint").gameObject.GetComponent<Transform>();

        //Boss
        _theBoss = GameObject.Find("tank").gameObject.GetComponent<Transform>();
        _originalPos = _theBoss.transform.localPosition;
        _firePoint = GameObject.Find("firePoint").gameObject.GetComponentInChildren<Transform>();
        _minePoint = GameObject.Find("minePoint").gameObject.GetComponentInChildren<Transform>();
        _hitBox = GameObject.Find("hitBox").gameObject.GetComponentInChildren<Transform>();
        _anim = GetComponentInChildren<Animator>();

        //Stats
        _timeBetweenShots = _defaultTimeBetweenShots;
        _timeBetweenMines = _defaultTimeBetweenMines;
        _currentHP = _defaultHP;
        _moveSpeed = _defaultSpeed;
    }

    private void Start()
    {
        _currentState = bossStates.shooting;
        PlayerHealthController.OnPlayerDied += ResetBoss;
    }

    private void Update()
    {
        switch(_currentState)
        {
            case bossStates.shooting:

                //shoot cooldown
                _shotCounter -= Time.deltaTime;

                //can shoot again
                if(_shotCounter <= 0f)
                {
                    _shotCounter = _timeBetweenShots;
                    var newBullet = Instantiate(_bulletObj, _firePoint.position, _firePoint.rotation);
                    newBullet.transform.localScale = _theBoss.localScale;
                }

                break;

            case bossStates.hurt:

                if (_hurtCounter > 0)
                {
                    _hurtCounter -= Time.deltaTime;

                    if (_hurtCounter <= 0f)
                    {
                        _currentState = bossStates.moving;
                        _mineCounter = 0f;

                        if(_isDefeated)
                        {
                            _theBoss.gameObject.SetActive(false);
                            Instantiate(_explosion, _theBoss.position, _theBoss.rotation);

                            foreach (GameObject go in _activateObjects)
                                go.SetActive(true);

                            AudioManager.Instance.StopBossMusic();

                            _currentState = bossStates.ended;
                        }
                    } 
                }      

                break;

            case bossStates.moving:

                if(_isMovingRight)
                {
                    _theBoss.position += new Vector3(_moveSpeed * Time.deltaTime, 0f, 0f);

                    //stop moving
                    if (_theBoss.position.x > _rightPoint.position.x)
                    {
                        _theBoss.localScale = new Vector3(1f, 1f, 1f);
                        _isMovingRight = false;

                        EndMovement();
                    }                       
                }
                else
                {
                    _theBoss.position -= new Vector3(_moveSpeed * Time.deltaTime, 0f, 0f);

                    //stop moving
                    if (_theBoss.position.x < _leftPoint.position.x)
                    {
                        _theBoss.localScale = new Vector3(-1f, 1f, 1f);
                        _isMovingRight = true;

                        EndMovement();
                    }
                }

                //Plant mines
                _mineCounter -= Time.deltaTime;
                if(_mineCounter <= 0f)
                {
                    _mineCounter = _timeBetweenMines;
                    Instantiate(_mineObj, _minePoint.position, _minePoint.rotation);
                }

                break;
        }

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.H))
            TakeHit();
#endif

    }

    public void TakeHit()
    {
        _currentState = bossStates.hurt;
        _hurtCounter = _currHurtTime;

        _anim.SetTrigger("Hit");
        AudioManager.Instance.PlaySFX(0);

        BossTankMine[] mines = FindObjectsOfType<BossTankMine>();
        if(mines.Length > 0f)
        {
            foreach(BossTankMine foundMine in mines)
            {
                foundMine.Explode();
            }
        }

        _currentHP--;
        _moveSpeed++;

        if(_currentHP <= 0)
        {
            _isDefeated = true;
        }
        else //increase fire rate
        {
            _timeBetweenShots /= _shotSpeedUp;
            _timeBetweenMines /= _mineSpeedUp;
        }
    }

    private void EndMovement()
    {
        _currentState = bossStates.shooting;
        _shotCounter = 0f;
        _anim.SetTrigger("StopMoving");
        _hitBox.gameObject.SetActive(true);
    }

    private void ResetBoss()
    {
        _theBoss.transform.localPosition = _originalPos;

        _currentHP = _defaultHP;
        _timeBetweenMines = _defaultTimeBetweenMines;
        _timeBetweenShots = _defaultTimeBetweenShots;
        _moveSpeed = _defaultSpeed;

        BossTankMine[] mines = FindObjectsOfType<BossTankMine>();
        if (mines.Length > 0f)
        {
            foreach (BossTankMine foundMine in mines)
            {
                foundMine.Explode();
            }
        }
    }
}
