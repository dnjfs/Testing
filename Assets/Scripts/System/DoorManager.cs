using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;  //DOTween�� ����ϱ� ���� ����Ʈ

public class DoorManager : MonoBehaviour
{
    //�� ���� ��ũ��Ʈ(������)
    //���� GameManager�� �� Ÿ���� T, E, S���
    //������ ���������� ����, Ż�ⱸ �� ����?
    /*
    GameObject left;
    GameObject right;

    void Start()
    {
        left = GameObject.FindWithTag("Left");
        right = GameObject.FindWithTag("Right");
        OpenDoor(left, right);
    }

    public void OpenDoor(GameObject leftDoor, GameObject rightDoor)
    {
        leftDoor.transform.DOLocalMoveX(3f, 3f).SetRelative();  //3�ʰ� X �������� 3��ŭ �̵�
        rightDoor.transform.DOLocalMoveX(-3f, 3f).SetRelative();  //3�ʰ� X �������� -3��ŭ �̵�
    }
    */
}
