using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OverworldUIController : MonoBehaviour
{
    public static OverworldUIController Instance;

    [SerializeField] private Image _fadeScreen;
    [SerializeField] private float _fadeSpeed = 3;
    private bool _shouldFadeToBlack, _shouldFadeFromBlack;

    [SerializeField] private GameObject _levelInfoPanel;
    [SerializeField] private TextMeshProUGUI _levelNameText;
    [SerializeField] private TextMeshProUGUI _gemsFound;
    [SerializeField] private TextMeshProUGUI _gemsTotal;
    [SerializeField] private TextMeshProUGUI _currentTime;
    [SerializeField] private TextMeshProUGUI _timeToBeat;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _fadeScreen.gameObject.SetActive(true);
        _levelInfoPanel.gameObject.SetActive(false);
        FadeFromBlack();
    }

    void Update()
    {
        if (_shouldFadeToBlack)
        {
            _fadeScreen.color = new Color(_fadeScreen.color.r, _fadeScreen.color.g, _fadeScreen.color.b, Mathf.MoveTowards(_fadeScreen.color.a, 1f, _fadeSpeed * Time.deltaTime)); //3 * timeDelta = a third of a sec
            if (_fadeScreen.color.a == 1f)
            {
                _shouldFadeToBlack = false;
            }
        }

        if (_shouldFadeFromBlack)
        {
            _fadeScreen.color = new Color(_fadeScreen.color.r, _fadeScreen.color.g, _fadeScreen.color.b, Mathf.MoveTowards(_fadeScreen.color.a, 0f, _fadeSpeed * Time.deltaTime)); //3 * timeDelta = a third of a sec
            if (_fadeScreen.color.a == 0f)
            {
                _shouldFadeFromBlack = false;
            }
        }
    }

    public void FadeToBlack()
    {
        _shouldFadeToBlack = true;
        _shouldFadeFromBlack = false;
    }

    public void FadeFromBlack()
    {
        _shouldFadeFromBlack = true;
        _shouldFadeToBlack = false;
    }

    public float GetFadeSpeed()
    {
        return _fadeSpeed;
    }

    public void ShowInfo(MapPoint levelInfo)
    {
        _levelNameText.text = levelInfo.GetLevelName();
        _gemsFound.text = "FOUND: " + levelInfo.GetGemsCollected();
        _gemsTotal.text = "IN LEVEL: " + levelInfo.GetTotalGemsCollected();
        _timeToBeat.text = "TARGET: " + levelInfo.GetTimeToBeat() + "s";
        
        if(levelInfo.GetBestTime() == 0)
        {
            _currentTime.text = "BEST: ----";
        }
        else
        {
            _currentTime.text = "BEST: " + levelInfo.GetBestTime().ToString("F1") + "s";
        }

        _levelInfoPanel.SetActive(true);
    }

    public void HideInfo()
    {
        _levelInfoPanel.SetActive(false);
    }
}
