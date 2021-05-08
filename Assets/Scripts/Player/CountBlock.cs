using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountBlock : MonoBehaviour
{
    string blockName;
    int index;

    public string[] splitText;

    GameObject system;

    void Start()
    {
        blockName = "";
        index = 0;

        system = GameObject.FindWithTag("GameSystem");
    }

    //����� �÷��̾�� �浹�ߴٸ�(OnTriggerEnter�� �ص� ������ ��)
    void OnTriggerExit(Collider other)
    {
        //�ڽ�(���)�� �̸��� ������
        blockName = other.gameObject.name;
        //�ڽ�(���)�� �ش��ϴ� ��� �迭�� �ε����� ������(�̸����� ������)
        splitText = blockName.Split('(');   //'('�� �������� string ����

        string indexChar = splitText[1].Substring(0, 2);  //������ ���ڿ��� ù��°, �ι�° �� ������
        index = int.Parse(indexChar); //char�� index�� int�� ��ȯ


        //��Ϻ� �ߺ� Ƚ���� �����ϴ� �迭�� �ش� �ε�����°�� �� 1 ����
        //GameObject.FindWithTag("GameSystem").GetComponent<Repetition>().addCount(index);
        system.GetComponent<Repetition>().addCount(index);
    }
}

    /*
    string blockName;
    int index;
    public string[] splitText;
    char[] separatorChar = { '(', ')' };
    GameObject system;

    void Start()
    {
        blockName = "";
        index = 0;
        system = GameObject.FindWithTag("GameSystem");
    }

    //�÷��̾ �浹 ó��
    void OnTriggerExit(Collider other)
    {
        //�浹�� ����� �̸��� ������
        blockName = other.gameObject.name;
        //����� �ش��ϴ� ��� �迭�� �ε����� ������(����� �̸����� ������)
        splitText = blockName.Split(separatorChar);   //'('�� ')'�� �������� string ����
        string test = splitText[1];
        index = int.Parse(test); //string�� index�� int�� ��ȯ
    
    
        //��Ϻ� �ߺ� Ƚ���� �����ϴ� �迭�� �ش� �ε�����°�� �� 1 ����
        //GameObject.FindWithTag("GameSystem").GetComponent<Repetition>().addCount(index);
        system.GetComponent<Repetition>().addCount(index);
    }
    */
