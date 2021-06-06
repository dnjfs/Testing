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
        if (other.tag == "CountBlocks")
        {
            //����� �̸��� ������
            blockName = other.gameObject.name;
            //����� �ش��ϴ� ��� �迭�� �ε����� ������(�̸����� ������)
            splitText = blockName.Split('(');   //'('�� �������� string ����

            string indexChar = splitText[1].Substring(0, 2);  //������ ���ڿ��� ù��°, �ι�° �� ������
            index = int.Parse(indexChar); //char�� index�� int�� ��ȯ


            //��Ϻ� �ߺ� Ƚ���� �����ϴ� �迭�� �ش� �ε�����°�� �� 1 ����
            //GameObject.FindWithTag("GameSystem").GetComponent<Repetition>().addCount(index);
            system.GetComponent<Repetition>().addCount(index);
        }
    }
}
