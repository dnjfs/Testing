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
        enterName = nickNameInput.text; //input field�� �Է��� �ؽ�Ʈ ���� ������
        GameManager.instance.nickName = enterName;  //������ �г����� GameManager �̱����� �г��� ������ ����
    }
}
