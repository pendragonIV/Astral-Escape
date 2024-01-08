using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Transform gameLogo;
    [SerializeField]
    private Transform tutorPanel;
    [SerializeField]
    private Transform guideLine;
    [SerializeField]
    private Transform playBtn;
    [SerializeField]
    private Transform sceneComponents;


    private void Start()
    {
        tutorPanel.gameObject.SetActive(false);

        gameLogo.GetComponent<CanvasGroup>().alpha = 0f;
        gameLogo.GetComponent<CanvasGroup>().DOFade(1, 2f).SetUpdate(true);
        gameLogo.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -700, 0);
        gameLogo.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 0), 2f, false).SetEase(Ease.OutQuint).SetUpdate(true);
        StartUpBtn();
    }

    private void StartUpBtn()
    {
        Vector3 defaultPos = playBtn.GetComponent<RectTransform>().anchoredPosition;
        playBtn.GetComponent<RectTransform>().anchoredPosition = new Vector3(defaultPos.x, 10, defaultPos.z);
        playBtn.GetComponent<RectTransform>().DOAnchorPos(defaultPos, 1f, false).SetEase(Ease.OutElastic).SetUpdate(true);
    }

    public void ShowTutorPanel()
    {
        tutorPanel.gameObject.SetActive(true);
        guideLine.gameObject.SetActive(true);
        FadeIn(tutorPanel.GetComponent<CanvasGroup>(), guideLine.GetComponent<RectTransform>());
        sceneComponents.gameObject.SetActive(false);
        playBtn.gameObject.SetActive(false);
    }

    public void HideTutorPanel()
    {
        StartCoroutine(FadeOut(tutorPanel.GetComponent<CanvasGroup>(), guideLine.GetComponent<RectTransform>()));
        sceneComponents.gameObject.SetActive(true);
        playBtn.gameObject.SetActive(true);
    }   

    private void FadeIn(CanvasGroup canvasGroup ,RectTransform rectTransform)
    {
        canvasGroup.alpha = 0f;
        canvasGroup.DOFade(1, .3f).SetUpdate(true);

        rectTransform.anchoredPosition = new Vector3(0, 700, 0);
        rectTransform.DOAnchorPos(new Vector2(0, 0), .3f, false).SetEase(Ease.OutQuint).SetUpdate(true);
    }

    private IEnumerator FadeOut(CanvasGroup canvasGroup, RectTransform rectTransform)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.DOFade(0, .3f).SetUpdate(true);

        rectTransform.anchoredPosition = new Vector3(0, 0, 0);
        rectTransform.DOAnchorPos(new Vector2(0, 700), .3f, false).SetEase(Ease.OutQuint).SetUpdate(true);

        yield return new WaitForSecondsRealtime(.3f);
        guideLine.gameObject.SetActive(true);
        tutorPanel.gameObject.SetActive(false);
    }

}
