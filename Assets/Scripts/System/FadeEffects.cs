using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeEffects : MonoBehaviour
{
    public static float fadeTime = 1f; //Fade ȿ�� �� ��� �ð�  

    public static void FadeIn(Image fadeImage)
    {
        //���̵��� �Լ� ����(��Ӵٰ� �����)
        Sequence seq = DOTween.Sequence();  //DOTween Sequence ����
        seq.OnStart(() =>
        {
            fadeImage.gameObject.SetActive(true); //�̹��� Ȱ��ȭ
        });
        seq.Append(fadeImage.DOFade(0f, fadeTime));  //0f ����� 2f���� ����

        seq.OnComplete(() =>
        {
            fadeImage.gameObject.SetActive(false); //�̹��� ��Ȱ��ȭ
        });        
    }
    
    public static void FadeOut(Image fadeImage)
    {
        //���̵�ƿ� �Լ� ����(��ٰ� ��ο���)
        Sequence seq = DOTween.Sequence();  //DOTween Sequence ����
        seq.OnStart(() =>
        {
            fadeImage.gameObject.SetActive(true); //�̹��� Ȱ��ȭ
        });
        seq.Append(fadeImage.DOFade(1f, fadeTime));  //1f ����� 2f���� ����

        seq.OnComplete(() =>
        {
            fadeImage.gameObject.SetActive(false); //�̹��� ��Ȱ��ȭ
        });
    }


    //���̵� �ƿ� �� �ٷ� �� �̵��ϴ� �Լ�(�ڿ������� ���̵� �ƿ��� ���� �ӽ÷� ����)
    public static void FadeOutAndLoadScene(Image fadeImage, string sceneName)
    {
        //���̵�ƿ� �Լ� ����(��ٰ� ��ο���)
        Sequence seq = DOTween.Sequence();  //DOTween Sequence ����
        seq.OnStart(() =>
        {
            fadeImage.gameObject.SetActive(true); //�̹��� Ȱ��ȭ
        });
        seq.Append(fadeImage.DOFade(1f, fadeTime));  //1f ����� 2f���� ����

        seq.OnComplete(() =>
        {
            LoadingManager.LoadScene(sceneName);
            //blackImage.gameObject.SetActive(false); //�̹��� ��Ȱ��ȭ
        });
    }

}
