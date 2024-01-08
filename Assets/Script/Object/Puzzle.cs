using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    private Vector3 rotation;

    private void Start()
    {
        rotation = this.transform.rotation.eulerAngles;
    }

    public void Rotate()
    {
        rotation.z += 90;
        this.transform.DORotate(rotation, ObjectRotator.DELAY_TIME).OnComplete(() =>
        {
            PuzzleManager.instance.CheckPuzzle();
        });
        if (rotation.z == 360 || rotation.z == -360)
        {
            rotation.z = 0;
        }
    }


}
