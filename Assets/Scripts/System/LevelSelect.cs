using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    //��ũ�Ѻ信�� ������ �����ϴ� �Լ� - ������
    //�ڵ� ����, �ּ� �߰��ؾ���
    //�����̵� �Ӹ� �ƴ϶� ��ư Ŭ������ �̵��� �����ϰ�? �ƴϸ� �����̵常�̳� ��ư���θ� �̵��ϰ� ����??

    public GameObject scrollbar;
    float scroll_pos = 0;
    float[] pos;

    void Update()
    {
        pos = new float[transform.childCount];
        float distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }

        if (Input.GetMouseButton(0))
        {
            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scroll_pos < pos[i] + (distance / 2) && scroll_pos > pos[i] - (distance / 2))
                {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.1f);
                }
            }
        }
    }

    /*
    public string currentLevel; //���� ���̵�

    void Start()
    {
        //GameManager �̱����� gameLevel ������(���� ����)

    }

    public void LeftButton()
    {
        //���� ���� �ؽ�Ʈ�� ������

        //���� ���� ������ hard�� nomal�̶�� ������ �̵�
        //else �̵� �Ұ�
        //���� ���� �ؽ�Ʈ �����ͼ� GameManager �̱����� gameLevel�� ����(string)
    }

    public void RightButton()
    {
        //���� ���� �ؽ�Ʈ�� ������
        //���� ���� ������ easy�� nomal�̶�� ������ �̵�
        //else �̵� �Ұ�
        //���� ���� �ؽ�Ʈ �����ͼ� GameManager �̱����� gameLevel�� ����(string)
    }*/

}
