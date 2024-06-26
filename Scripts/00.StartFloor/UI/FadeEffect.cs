using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum FadeState
{
    FadeIn, // ���� �����
    FadeOut, // ���� ��ο���
    FadeInOut, // �� -> ��
    FadeOutIn, // �� -> ��
    FadeLoop  // ������ �ݺ�
}
public class FadeEffect : MonoBehaviour
{
    [SerializeField]
    [Range(0.01f, 10f)]
    private float fadeTime;

    [SerializeField]
    private AnimationCurve fadeCurve;
    private Image image;
    private FadeState fadeState;
    void Start()
    {
        image = GetComponent<Image>();
        this.gameObject.SetActive(false);
    }

    public Coroutine UseFadeEffect(FadeState fade)
    {
        this.gameObject.SetActive(true);
        return OnFade(fade);
    }

    private Coroutine OnFade(FadeState state)
    {
        fadeState = state;

        switch (fadeState)
        {
            case FadeState.FadeIn:
                return StartCoroutine(Fade(1, 0));
            case FadeState.FadeOut:
                return StartCoroutine(Fade(0, 1));
            case FadeState.FadeInOut:
                return StartCoroutine(FadeInOut());
            case FadeState.FadeOutIn:
                return StartCoroutine(FadeOutIn());
            case FadeState.FadeLoop:
                return StartCoroutine(FadeInOut());
        }
        return null;
    }
    private IEnumerator FadeInOut()
    {
        while (true)
        {   
            // �ڷ�ƾ ���ο��� �ڷ�ƾ �Լ��� ȣ���ϸ� �ش� �ڷ�ƾ �Լ��� ����Ǿ�� ���� ������ ����
            yield return StartCoroutine(Fade(1, 0)); // Fade In

            yield return StartCoroutine(Fade(0, 1)); // Fade Out

            if (fadeState == FadeState.FadeInOut)
            {
                this.gameObject.SetActive(false);
                break; // 1ȸ�� ����ϴ� ����
            }
        }
    }
    private IEnumerator FadeOutIn()
    {
        while (true)
        {
            // �ڷ�ƾ ���ο��� �ڷ�ƾ �Լ��� ȣ���ϸ� �ش� �ڷ�ƾ �Լ��� ����Ǿ�� ���� ������ ����
            yield return StartCoroutine(Fade(0, 1)); // Fade Out
            
            yield return StartCoroutine(Fade(1, 0)); // Fade In

            if (fadeState == FadeState.FadeOutIn)
            {
                this.gameObject.SetActive(false);
                break; // 1ȸ�� ����ϴ� ����
            }
            
        }
    }


    private IEnumerator Fade(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            Color color = image.color;
            color.a = Mathf.Lerp(start, end, fadeCurve.Evaluate(percent));
            image.color = color;
            yield return null;
        }
    }

    public void OffFadeObject()
    {
        if (this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(false);
        }
    }
}
