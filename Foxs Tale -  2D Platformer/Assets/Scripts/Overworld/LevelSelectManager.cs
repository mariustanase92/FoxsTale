using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    [SerializeField] OverworldPlayerController _thePlayer;
    private MapPoint[] _allPoints;

    private void Start()
    {
        _allPoints = FindObjectsOfType<MapPoint>();

        if(PlayerPrefs.HasKey("CurrentLevel"))
        {
            foreach(MapPoint point in _allPoints) //look for the MapPoint that has "CurrentLevel" saved
            {
                if (point.levelToLoad == PlayerPrefs.GetString("CurrentLevel"))
                {
                    _thePlayer.transform.position = point.transform.position;
                    _thePlayer.currentPoint = point;
                }
            }
        }
    }

    public void LoadLevel()
    {
        StartCoroutine(LoadLevelCo());
    }    

   public IEnumerator LoadLevelCo()
    {
        AudioManager.Instance.PlaySFX(4);

        OverworldUIController.Instance.FadeToBlack();
        yield return new WaitForSeconds(1f / OverworldUIController.Instance.GetFadeSpeed());
        SceneManager.LoadScene(_thePlayer.currentPoint.levelToLoad);
    }
}
