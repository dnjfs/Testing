using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ending : MonoBehaviour
{
    //�հ��̸� Ż�� �ִϸ��̼�, ���հ��̸� ���ӿ��� ������ �̵��ϴ� ��ũ��Ʈ

    public GameObject joyStick; //�÷��̾� ���̽�ƽ

    GameObject player;
    public GameObject endingLeftDoor;   //Ż�ⱸ ���� ��
    public GameObject endingRightDoor;   //Ż�ⱸ ������ ��

    public void FailOrPass()    //������ ��ȭâ ��� ��)
    {
        //GameManager�� �հ� ���θ� ������
        if (GameManager.instance.isPass)    //�հ��̶��
        {
            IsPass();   //�հ� ��� ����
        }
        else
        {
            IsFail();   //���հ� ��� ����
        }
    }


    //�հ��̶�� ��ȭâ�� ������ �ⱸ�� ������ �ǹ� ������ ������ �Ǵ� ����
    public void IsPass()
    {
        Sequence seq = DOTween.Sequence();  //DOTween Sequence ����
        seq.OnStart(() => {
            this.GetComponent<DialogManager>().CreatePassMessage(); //�հ� �޽��� ���
            //ȭ�� ��ġ ��� ����

            //�ⱸ�� ����
            endingLeftDoor.transform.DOLocalMoveX(5f, 3f).SetRelative();  //3�ʰ� X �������� 5��ŭ �̵�
            endingRightDoor.transform.DOLocalMoveX(-6.5f, 3f).SetRelative();  //3�ʰ� X �������� -5��ŭ �̵�
        });

        //������ �̵�(�÷��̾� ���� ���� -> ���� �̹��� ��Ȱ��ȭ)
        player = GameObject.FindWithTag("Player").gameObject;
        player.transform.DOLocalMoveZ(30f, 5f).SetRelative(); //3�ʰ� Z �������� 30��ŭ �̵�

        seq.OnComplete(() => {
            //��ŷ ������ �̵�(�� ���� ��ŷ ����?)
            //this.GetComponent<SceneChange>().ChangeRankingScene();    //�� �ְ� �̵� 
        });
    }

    //���հ��̶�� ���ӿ��� ������ �̵�
    public void IsFail()
    {
        //���ӿ��� ������ �̵�
        this.GetComponent<SceneChange>().ChangeGameOverScene();
    }
}
