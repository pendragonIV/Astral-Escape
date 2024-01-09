using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public SceneChanger sceneChanger;
    public GameScene gameScene;

    #region Game status
    private Level currentLevelData;
    private bool isGameWin = false;
    private bool isGameLose = false;
    private bool isGamePause = false;
    private float timeLeft;

    #endregion

    private void Start()
    {
        currentLevelData = LevelManager.instance.levelData.GetLevelAt(LevelManager.instance.currentLevelIndex);
        GameObject map = Instantiate(currentLevelData.map);
        GridCellManager.instance.SetTileMap(map.transform.GetChild(1).GetChild(0).GetComponent<Tilemap>()); 
        PuzzleManager.instance.SetContainer(map.transform.GetChild(0));
        timeLeft = currentLevelData.timeLimit;
        Time.timeScale = 1;
    }

    private void Update()
    {
        if(isGameWin || isGameLose)
        {
            return;
        }
        timeLeft -= Time.deltaTime;
        gameScene.SetTimeLeft(timeLeft);
        if (timeLeft <= 0)
        { 
            Lose();
        }
    }

    public void Win()
    {
        if (LevelManager.instance.levelData.GetLevels().Count > LevelManager.instance.currentLevelIndex + 1)
        {
            if (LevelManager.instance.levelData.GetLevelAt(LevelManager.instance.currentLevelIndex + 1).isPlayable == false)
            {
                LevelManager.instance.levelData.SetLevelData(LevelManager.instance.currentLevelIndex + 1, true, false);
            }
        }

        isGameWin = true;
        StartCoroutine(WaitToWin());
        LevelManager.instance.levelData.SaveDataJSON();
    }

    private IEnumerator WaitToWin()
    {
        yield return new WaitForSeconds(.5f);
        gameScene.ShowWinPanel();
    }



    public void Lose()
    {
        isGameLose = true;
        gameScene.ShowLosePanel();
    }

    public bool IsGameWin()
    {
        return isGameWin;
    }

    public bool IsGameLose()
    {
        return isGameLose;
    }

    public void PauseGame()
    {
        isGamePause = true;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        isGamePause = false;
        Time.timeScale = 1;
    }

    public bool IsGamePause()
    {
        return isGamePause;
    }
}

