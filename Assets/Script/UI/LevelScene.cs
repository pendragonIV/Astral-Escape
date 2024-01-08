using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScene : MonoBehaviour
{
    public static LevelScene instance;

    [SerializeField]
    private Transform levelHolderPrefab;
    [SerializeField]
    private Transform levelsContainer;
    [SerializeField]
    private Transform starPrefab;

    public Transform sceneTransition;

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

    void Start()
    {
        PrepareLevels();
    }
    public void PlayChangeScene()
    {
        sceneTransition.GetComponent<Animator>().Play("SceneTransition");
    }

    private void PrepareLevels()
    {
        for (int i = 0; i < LevelManager.instance.levelData.GetLevels().Count; i++)
        {
            Transform holder = Instantiate(levelHolderPrefab, levelsContainer);
            holder.name = i.ToString();
            Level level = LevelManager.instance.levelData.GetLevelAt(i);
            if (LevelManager.instance.levelData.GetLevelAt(i).isPlayable)
            {
                holder.GetComponent<LevelHolder>().EnableHolder();
            }
            else
            {
                holder.GetComponent<LevelHolder>().DisableHolder();
            }
            
            if(i != LevelManager.instance.currentLevelIndex)
            {
                DisableHolder(holder);
            }
        }
    }

    private void DisableHolder(Transform holder)
    {
        holder.gameObject.SetActive(false);
    }

    private void EnableHolder(Transform holder)
    {
        holder.gameObject.SetActive(true);
        holder.rotation = Quaternion.Euler(0, 0, 15);
        holder.localScale = new Vector3(.6f, .6f, .6f);
        holder.GetComponent<RectTransform>().anchoredPosition = new Vector2(700, 200);

        holder.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 0), 1f).SetEase(Ease.OutBack);
        holder.DOScale(new Vector3(1, 1, 1), 1f).SetEase(Ease.OutBack);
        holder.DORotate(new Vector3(0, 0, 0), 1f).SetEase(Ease.OutElastic);
    }

    public void NextLevel()
    {
        DisableHolder(levelsContainer.GetChild(LevelManager.instance.currentLevelIndex));

        if(LevelManager.instance.currentLevelIndex < LevelManager.instance.levelData.GetLevels().Count - 1)
        {
            LevelManager.instance.currentLevelIndex++;
        }
        else
        {
            LevelManager.instance.currentLevelIndex = 0;
        }

        EnableHolder(levelsContainer.GetChild(LevelManager.instance.currentLevelIndex));
    }

    public void PreviousLevel()
    {
        DisableHolder(levelsContainer.GetChild(LevelManager.instance.currentLevelIndex));

        if (LevelManager.instance.currentLevelIndex > 0)
        {
            LevelManager.instance.currentLevelIndex--;
        }
        else
        {
            LevelManager.instance.currentLevelIndex = LevelManager.instance.levelData.GetLevels().Count - 1;
        }

        EnableHolder(levelsContainer.GetChild(LevelManager.instance.currentLevelIndex));
    }
}
