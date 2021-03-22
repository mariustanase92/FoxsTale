using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public float waitToRespawn;

    [HideInInspector] public int gemsCollected;

    public string levelToLoad;

    private float _timeInLevel;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _timeInLevel = 0f;
    }

    private void Update()
    {
        _timeInLevel += Time.deltaTime;
    }

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCo());
    }

    private IEnumerator RespawnCo()
    {
        PlayerController.Instance.gameObject.SetActive(false);
        yield return new WaitForSeconds(waitToRespawn - (1f / UIController.Instance.fadeSpeed));
        UIController.Instance.FadeToBlack();

        yield return new WaitForSeconds((1f / UIController.Instance.fadeSpeed) + .2f);

        UIController.Instance.FadeFromBlack();

        CameraController.Instance.stopFollow = false;

        PlayerController.Instance.gameObject.SetActive(true);

        PlayerController.Instance.transform.position = CheckpointController.Instance.GetSpawnPoint();

        PlayerHealthController.Instance.currentHealth = PlayerHealthController.Instance.GetMaxHealth();

        UIController.Instance.UpdateHealthDisplay();

        AudioManager.Instance.PlaySFX(11);
    }

    public int GetCurrentGems()
    {
        return gemsCollected;
    }

    public void EndLevel()
    {
        StartCoroutine(EndLevelCo());
    }

    public IEnumerator EndLevelCo()
    {
        AudioManager.Instance.PlayLevelVictory();

        PlayerController.Instance.stopInput = true;
        PlayerController.Instance.StopMoving();

        CameraController.Instance.stopFollow = true;

        UIController.Instance.levelCompleteMenu.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        UIController.Instance.FadeToBlack();

        yield return new WaitForSeconds((1f / UIController.Instance.fadeSpeed) + .3f ); //fadetime + quarter of a sec

        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_unlocked", 1); //set an int of 1 to the active scene - will be used to set levelLocked = false
        PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name); //save current level name
        
        if(PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_gems"))
        {
            if(gemsCollected > PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_gems"))
            {
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_gems", gemsCollected);
            }
        }
        else
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_gems", gemsCollected);

        if(PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + "_time"))
        {
            if(_timeInLevel < PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + "_time"))
            {
                PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_time", _timeInLevel);
            }
        }
        else
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + "_time", _timeInLevel);


        SceneManager.LoadScene(levelToLoad);
    }

    public int GetGemsCollected()
    {
        return gemsCollected;
    }

    public float GetTimeInLevel()
    {
        return _timeInLevel;
    }
}
