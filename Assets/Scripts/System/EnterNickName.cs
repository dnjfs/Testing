using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class EnterNickName : MonoBehaviour
{
    //�г��� �Է� �Լ�
    public InputField nickNameInput;
    public string enterName;

    //�г��� ����
    //�г��� ���� ���� ����
    public void CreateNickName()
    {
        enterName = nickNameInput.text;
        //GameManager.instance.SetNickName(nickNameInput.text);
        GameManager.instance.nickName = enterName;
    }
}
