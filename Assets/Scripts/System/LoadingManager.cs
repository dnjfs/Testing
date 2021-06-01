using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    
    public static string changeScene;   //전환할 씬

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    public static void LoadScene(string sceneName)
    {
        changeScene = sceneName;    //전환할 씬 저장
        SceneManager.LoadScene("Loading");  //로딩 씬으로 이동
    }

    IEnumerator LoadScene()
    {
        yield return null;

        //LoadSceneAsync(): 비동기 방식으로 씬을 불러옴(불러오는 도중 다른 작업 가능)
        AsyncOperation op = SceneManager.LoadSceneAsync(changeScene);   
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            yield return null;
            
             if (op.progress == 0.9f)
             {
                //진행률이 100퍼센트면
                op.allowSceneActivation = true;
                yield break; 
             }
        }
    }
}
