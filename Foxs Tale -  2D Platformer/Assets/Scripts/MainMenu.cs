using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string _startScene;
    [SerializeField] private string _continueScene;
    [SerializeField] private string _levelToCheck;
    [SerializeField] private GameObject _continueButtonObj;

    private Image _continueImage;
    private Button _continueButton;
    private Text _continueText;

    private void Awake()
    {
        _continueImage = _continueButtonObj.GetComponent<Image>();
        _continueButton = _continueButtonObj.GetComponent<Button>();
        _continueText = _continueButtonObj.GetComponentInChildren<Text>();
    }

    private void Start()
    {
        if (_levelToCheck != null)
        {
            if (PlayerPrefs.HasKey(_levelToCheck + "_unlocked"))
            {
                _continueImage.color = new Color(_continueImage.color.r, _continueImage.color.g, _continueImage.color.b,  1);
                _continueText.color = new Color(_continueText.color.r, _continueText.color.g, _continueText.color.b, 1f);
                _continueButton.interactable = true;            
            }
            else
            {
                _continueImage.color = new Color(_continueImage.color.r, _continueImage.color.g, _continueImage.color.b, .5f);
                _continueText.color = new Color(_continueText.color.r, _continueText.color.g, _continueText.color.b, .5f);
                _continueButton.interactable = false;
            }    
        } 
    }

    public void NewGameGame()
    {
        SceneManager.LoadScene(_startScene);

        PlayerPrefs.DeleteAll();
    }

    public void ContinueGame()
    {
        SceneManager.LoadScene(_continueScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
