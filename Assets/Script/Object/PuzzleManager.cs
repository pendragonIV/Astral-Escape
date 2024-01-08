using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager instance;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    [SerializeField]
    private List<GameObject> triggeredPuzzle;
    [SerializeField]
    private Transform puzzleContainer;
    [SerializeField]
    private int totalTrigger;

    private void Start()
    {
        triggeredPuzzle = new List<GameObject>();
    }

    public void SetContainer(Transform container)
    {
        puzzleContainer = container;
        GetTotalTrigger();
    }

    private void GetTotalTrigger()
    {
        foreach (Transform child in puzzleContainer)
        {
            totalTrigger += child.childCount;
        }
    }


    public void CheckPuzzle()
    {
        if(triggeredPuzzle.Count == totalTrigger)
        {
            GameManager.instance.Win();
        }
    }

    public void AddTriggered(GameObject trigger)
    {
        if(triggeredPuzzle.Contains(trigger))
        {
            Debug.Log("Already triggered");
            return;
        }
        triggeredPuzzle.Add(trigger);
    }

    public void RemoveTriggered(GameObject trigger)
    {
        if(!triggeredPuzzle.Contains(trigger))
        {
            Debug.Log("No such trigger");
            return;
        }
        triggeredPuzzle.Remove(trigger);
    }


}
