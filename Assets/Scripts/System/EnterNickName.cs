using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class EnterNickName : MonoBehaviour
{
    //닉네임 입력 함수
    private InputField InputName;
    public string enterName;

    void Start()
    {
        InputName = GameObject.Find("InputField_Name").GetComponent<InputField>();
        if (PlayerPrefs.HasKey("Name"))
            InputName.text = PlayerPrefs.GetString("Name"); //닉네임 불러오기
    }

    public void SaveName()
    {
        PlayerPrefs.SetString("Name", InputName.text); //닉네임 저장
        if (InputName.text != "") //비어있지 않은 경우
            GameObject.Find("GameSystem").GetComponent<SceneChange>().ChangeStartScene();
    }

    //Login 씬에는 GameManager가 없어 Start 씬에서 GameManager가 생성될 때 직접 이름을 넣어줌
    //void CreateNickName()
    //{
    //    enterName = InputName.text; //input field에 입력한 텍스트 값을 저장함
    //    GameManager.instance.nickName = enterName;  //저장한 닉네임을 GameManager 싱글톤의 닉네임 변수에 저장
    //}
}
