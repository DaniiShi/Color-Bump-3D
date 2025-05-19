using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public CanvasGroup StartPanelCanvasGroup;
    public CanvasGroup FinishPanelCanvasGroup;
    public CanvasGroup PlayingPanelCanvasGroup;
    public CanvasGroup LosePanelCanvasGroup;

    [Header("Настройки анимации")]
    public float fadeDuration = 0.3f;

    public void StartState()
    {
        FadeIn(StartPanelCanvasGroup);
        FadeIn(PlayingPanelCanvasGroup);
        FadeOut(FinishPanelCanvasGroup);
        FadeOut(LosePanelCanvasGroup);
    }

    public void PlayState()
    {
        FadeOut(StartPanelCanvasGroup);
        FadeIn(PlayingPanelCanvasGroup);
        FadeOut(FinishPanelCanvasGroup);
        FadeOut(LosePanelCanvasGroup);
    }

    public void FinishState()
    {
        FadeOut(StartPanelCanvasGroup);
        FadeOut(PlayingPanelCanvasGroup);
        FadeIn(FinishPanelCanvasGroup);
        FadeOut(LosePanelCanvasGroup);
    }
    public void LoseState()
    {
        FadeOut(StartPanelCanvasGroup);
        FadeOut(PlayingPanelCanvasGroup);
        FadeOut(FinishPanelCanvasGroup);
        FadeIn(LosePanelCanvasGroup);
    }

    private void FadeIn(CanvasGroup cg)
    {
        if (cg == null) return;
        cg.interactable = true;
        cg.blocksRaycasts = true;
        StopCoroutineIfRunning(cg);
        StartCoroutine(FadeCanvasGroup(cg, cg.alpha, 1f));
    }

    private void FadeOut(CanvasGroup cg)
    {
        if (cg == null) return;
        cg.interactable = false;
        cg.blocksRaycasts = false;
        StopCoroutineIfRunning(cg);
        StartCoroutine(FadeCanvasGroup(cg, cg.alpha, 0f));
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float from, float to)
    {
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            cg.alpha = Mathf.Lerp(from, to, elapsed / fadeDuration);
            yield return null;
        }
        cg.alpha = to;
    }

    private void StopCoroutineIfRunning(CanvasGroup cg)
    {
        // Хак: гарантируем, что не будет дублирующихся корутин
        StopCoroutine(FadeCanvasGroup(cg, 0f, 0f));
    }
}
