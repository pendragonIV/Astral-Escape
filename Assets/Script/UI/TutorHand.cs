using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorHand : MonoBehaviour
{
    private void Start()
    {
        Vector3 destination = transform.localPosition + new Vector3(0f, .5f, 0);
        this.transform.DOLocalMove(destination, 1).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Restart);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(this.gameObject);
        }
    }
}
