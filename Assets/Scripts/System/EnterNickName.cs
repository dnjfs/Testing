using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class EnterNickName : MonoBehaviour
{
    //�г��� �Է� �Լ�
    private InputField InputName;
    public string enterName;

    void Start()
    {
        InputName = GameObject.Find("InputField_Name").GetComponent<InputField>();
        if (PlayerPrefs.HasKey("Name"))
            InputName.text = PlayerPrefs.GetString("Name"); //�г��� �ҷ�����
    }

    public void SaveName()
    {
        PlayerPrefs.SetString("Name", InputName.text); //�г��� ����
        if (InputName.text != "") //������� ���� ���
            GameObject.Find("GameSystem").GetComponent<SceneChange>().ChangeStartScene();
    }

    //Login ������ GameManager�� ���� Start ������ GameManager�� ������ �� ���� �̸��� �־���
    //void CreateNickName()
    //{
    //    enterName = InputName.text; //input field�� �Է��� �ؽ�Ʈ ���� ������
    //    GameManager.instance.nickName = enterName;  //������ �г����� GameManager �̱����� �г��� ������ ����
    //}
}
