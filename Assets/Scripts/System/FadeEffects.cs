using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffects : MonoBehaviour
{
    //Fade In, Fade Out ȿ���� ���� ��ũ��Ʈ

    //Fade ȿ�� �� ��� �ð�  
    public float FadeTime = 2f;
    //Fade ȿ�� ������� �ð�
    public float Fadingtime = 0f;

    public void FadeIn(Image FadeInImg)
    {
        //���̵��� �Լ� ����
        StartCoroutine(coFadeIn(FadeInImg));
    }

    public void FadeOut(Image FadeOutImg)
    {
        //���̵�ƿ� �Լ� ����
        StartCoroutine(coFadeOut(FadeOutImg));
    }

    //�������� �̹����� �����ϰ� ����� �ڷ�ƾ �Լ� -> Fade In ȿ���� ���� �ڷ�ƾ �Լ�
    IEnumerator coFadeIn(Image FadeInImg)
    {
        //Fade In ȿ���� �� �̹��� Ȱ��ȭ
        FadeInImg.gameObject.SetActive(true);

        //Fade In ���� �ð� �ʱ�ȭ
        Fadingtime = 0f;
        
        //�ӽ÷� ������ ������ ����
        Color tempColor = FadeInImg.color;

        while (tempColor.a > 0f)
        {
            Fadingtime += (Time.timeScale * Time.deltaTime) / FadeTime;

            //���� ����
            tempColor.a = Mathf.Lerp(1, 0, Fadingtime);

            //�⺻ �̹����� �ٲ� ���� ����
            FadeInImg.color = tempColor;

            yield return null;
        }

        //Fade In ȿ���� �� �̹��� ��Ȱ��ȭ
        FadeInImg.gameObject.SetActive(false);

        //�ڷ�ƾ ����
        yield return null;
    }

    //������ �̹����� �������ϰ� ����� �ڷ�ƾ �Լ� -> Fade Out ȿ���� ��Ÿ���� �ڷ�ƾ �Լ�
    IEnumerator coFadeOut(Image FadeOutImg)
    {
        //Fade Out ȿ���� �� �̹��� Ȱ��ȭ
        FadeOutImg.gameObject.SetActive(true);

        //Fade Out ���� �ð� �ʱ�ȭ
        Fadingtime = 0f;

        //�ӽ÷� ������ ������ ����
        Color tempColor = FadeOutImg.color;

        while (tempColor.a < 1f)
        {
            Fadingtime += (Time.timeScale * Time.deltaTime) / FadeTime;

            //���� ����
            tempColor.a = Mathf.Lerp(0, 1, Fadingtime);

            //�⺻ �̹����� �ٲ� ���� ����
            FadeOutImg.color = tempColor;

            yield return null;
        }

        //�ڷ�ƾ ����
        yield return null;
    }
}
