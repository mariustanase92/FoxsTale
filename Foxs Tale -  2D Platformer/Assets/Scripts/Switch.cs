using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] private GameObject _objectToSwitch;
    private SpriteRenderer _theSR;
    [SerializeField] private Sprite _downSprite;
    private bool _hasSwitched;
    [SerializeField] private bool deactivateOnSwitch;

    private void Start()
    {
        _theSR = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !_hasSwitched)
        {
            if(deactivateOnSwitch)
            {
                _objectToSwitch.SetActive(false);
                AudioManager.Instance.PlaySFX(9);
            }
            else
            {
                _objectToSwitch.SetActive(true);
                AudioManager.Instance.PlaySFX(9);
            }

            _theSR.sprite = _downSprite;
            _hasSwitched = true;
        }
    }
}
