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
        enterName = nickNameInput.text;
        //GameManager.instance.SetNickName(nickNameInput.text);
        GameManager.instance.nickName = enterName;
    }
}
