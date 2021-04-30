using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class EnterNickName : MonoBehaviour
{
    //닉네임 입력 함수
    public InputField nickNameInput;
    public string enterName;

    //닉네임 저장
    //닉네임 조건 넣을 예정
    public void CreateNickName()
    {
        enterName = nickNameInput.text; //input field에 입력한 텍스트 값을 저장함
        GameManager.instance.nickName = enterName;  //저장한 닉네임을 GameManager 싱글톤의 닉네임 변수에 저장
    }
}
