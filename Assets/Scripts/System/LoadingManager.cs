using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    
    public static string changeScene;   //��ȯ�� ��

    private void Start()
    {
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

        while (!op.isDone)
        {
            yield return null;
            
             if (op.progress == 0.9f)
             {
                //������� 100�ۼ�Ʈ��
                op.allowSceneActivation = true;
                yield break; 
             }
        }
    }
}
