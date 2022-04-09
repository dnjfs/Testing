using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    
    public static string changeScene;   //��ȯ�� ��
    public Slider ProgressBar;  //���� ��
    public Image BlackImage;    //���̵� ȿ���� ���� ���� �̹���
    public Image ClearImage;    //���̵� ȿ���� ���� ���� �̹���

    //�ð� ��� �ε�ȭ��
    private float time_loading = 5;
    private float time_current;
    private float time_start;
    //private bool isEnded = true;

    private void Start()
    {
        FadeEffects.FadeIn(BlackImage, 0.5f); //���̵��� ȭ��
        StartCoroutine(LoadScene());
    }

    public static void LoadScene(string sceneName)
    {
        changeScene = sceneName;    //��ȯ�� �� ����
        SceneManager.LoadScene("Loading");  //�ε� ������ �̵�
    }

    IEnumerator LoadScene()
    {
        yield return null;

        //LoadSceneAsync(): �񵿱� ������� ���� �ҷ���(�ҷ����� ���� �ٸ� �۾� ����)
        AsyncOperation op = SceneManager.LoadSceneAsync(changeScene);   
        op.allowSceneActivation = false;

        //float timer = 0.0f; //�ð� �ʱ�ȭ
        ProgressBar.value = 0.0f;   //���൵ �����̴� �ʱ�ȭ

        /*
        while (!op.isDone)
        {
            yield return null;
            timer += Time.deltaTime;
            if (op.progress < 0.9f)
            {
                //�غ� �� ���� �ʾҴٸ�
                //Mathf.Lerp(float ������, float ������, float �Ÿ�����): ���������� ������������ �Ÿ� ���� ��ȯ
                ProgressBar.value = Mathf.Lerp(ProgressBar.value, op.progress, timer);
                if (ProgressBar.value >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                ProgressBar.value = Mathf.Lerp(ProgressBar.value, 1.0f, timer);
                
                if (ProgressBar.value == 1.0f)
                {
                    //�غ� �� �Ǿ��ٸ�
                    op.allowSceneActivation = true;

                    //FadeEffects.FadeOut(ClearImage, 0.5f); //���̵�ƿ� ȭ��
                    yield break;
                }
            }
        }
        */

        //5�� ���� �ε��� �������� �����ֱ�
        time_current = time_loading;
        time_start = Time.time;

        while(!op.isDone)
        {
            yield return null;
            time_current = Time.time - time_start;
            if(time_current < time_loading)
            {
                ProgressBar.value = time_current / time_loading;
            }
            
            if (ProgressBar.value >= 0.99f)
            {
                //�غ� �� �Ǿ��ٸ�
                op.allowSceneActivation = true;
                yield break;
            }

        }
        FadeEffects.FadeOut(ClearImage, 0.5f); //���̵�ƿ� ȭ��
    }
}