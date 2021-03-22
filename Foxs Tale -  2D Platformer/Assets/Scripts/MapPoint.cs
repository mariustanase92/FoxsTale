using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPoint : MonoBehaviour
{
    public MapPoint up, down, right, left;
    public bool _isLevel;
    [SerializeField] private bool _isLocked;
    public string levelToLoad;
    [SerializeField] private string _levelToCheck;
    [SerializeField] private string _levelName;

    private int _gemsCollected;
    [SerializeField] private int _gemsTotal;
    private float _bestTime;
    [SerializeField] private float _timeToBeat;

    [SerializeField] private GameObject _gemBadge;
    [SerializeField] private GameObject _timeBadge;

    private void Start()
    {
        if(_isLevel && levelToLoad != null)
        {
            if (PlayerPrefs.HasKey(levelToLoad + "_gems"))
                _gemsCollected = PlayerPrefs.GetInt(levelToLoad + "_gems");

            if (PlayerPrefs.HasKey(levelToLoad + "_time"))
                _bestTime = PlayerPrefs.GetFloat(levelToLoad + "_time");

            _isLocked = true;

            if(_levelToCheck != null)
            {
                if(PlayerPrefs.HasKey(_levelToCheck + "_unlocked"))
                {
                    if (PlayerPrefs.GetInt(_levelToCheck + "_unlocked") == 1)
                        _isLocked = false;
                }
            }

            if (_gemsCollected >= _gemsTotal)
                _gemBadge.SetActive(true);

            if (_bestTime <= _timeToBeat && _bestTime != 0f)
                _timeBadge.SetActive(true);
        }

        //if (levelToLoad == _levelToCheck)
            //_isLocked = false;
    }

    public bool IsLevelLocked()
    {
        return _isLocked;
    }

    public string GetLevelName()
    {
        return _levelName;
    }

    public int GetGemsCollected()
    {
        return _gemsCollected;
    }

    public int GetTotalGemsCollected()
    {
        return _gemsTotal;
    }

    public float GetBestTime()
    {
        return _bestTime;
    }


    public float GetTimeToBeat()
    {
        return _timeToBeat;
    }

}
