using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class FadeEffects : MonoBehaviour
{
    public static void FadeIn(Image fadeImage, float fadeTime)
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
    
    public static void FadeOut(Image fadeImage, float fadeTime)
    {
        //���̵�ƿ� �Լ� ����(��ٰ� ��ο���)
        Sequence seq = DOTween.Sequence();  //DOTween Sequence ����
        seq.OnStart(() =>
        {
            fadeImage.gameObject.SetActive(true); //�̹��� Ȱ��ȭ
        });
        seq.Append(fadeImage.DOFade(1f, fadeTime));  //1f ����� 2f���� ����
    }


    //���̵� �ƿ� �� �ٷ� �� �̵��ϴ� �Լ�(�ڿ������� ���̵� �ƿ��� ���� �ӽ÷� ����)
    public static void FadeOutAndLoadScene(Image fadeImage, string sceneName, float fadeTime)
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
            if (sceneName == "Maze_T" || sceneName == "Maze_E" || sceneName == "Maze_S")
                LoadingManager.LoadScene(sceneName);
            else        //Playing�� ���� �ٸ� ������ �̵��� �ε�ȭ�� ���ʿ�
                SceneManager.LoadScene(sceneName);
            //blackImage.gameObject.SetActive(false); //�̹��� ��Ȱ��ȭ
        });
    }

}
