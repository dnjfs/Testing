using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ending : MonoBehaviour
{
    //�հ��̸� Ż�� �ִϸ��̼�, ���հ��̸� ���ӿ��� ������ �̵��ϴ� ��ũ��Ʈ

    //Player Prefab�� ���� public ����
    public GameObject Player;
    GameObject player;

    public GameObject exitElevator_leftDoor;    //Ż�ⱸ ������������ ���� ��
    public GameObject exitElevator_rightDoor;    //Ż�ⱸ ������������ ������ ��

    public GameObject endingLeftDoor;   //Ż�ⱸ ���� ��
    public GameObject endingRightDoor;   //Ż�ⱸ ������ ��

    void Awake()
    {
        //�÷��̾� �⺻ ����
        player = (GameObject)Instantiate(Player, new Vector3(-50.3f, 7.56f, -2.24f), Quaternion.identity);
    }

    void Start()
    {
        //���� ���۵Ǹ� ���� ����.
        exitElevator_leftDoor.transform.DOLocalMoveX(3f, 3f).SetRelative();  //3�ʰ� X �������� 3��ŭ �̵�
        exitElevator_rightDoor.transform.DOLocalMoveX(-3f, 3f).SetRelative();  //3�ʰ� X �������� -3��ŭ �̵�

    }

    public void FailOrPass()    //������ ��ȭâ ��� ��)
    {
        //��� ���� ����
        player.transform.GetChild(0).gameObject.GetComponent<BGAudioPlay>().PlayEndingBG();

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
            //�ⱸ�� ����
            endingLeftDoor.transform.DOLocalMoveX(5f, 3f).SetRelative();  //3�ʰ� X �������� 5��ŭ �̵�
            endingRightDoor.transform.DOLocalMoveX(-6.5f, 3f).SetRelative();  //3�ʰ� X �������� -5��ŭ �̵�

            this.GetComponent<Dialog_Corridor>().CreatePassMessage(); //�հ� �޽��� ���
            //ȭ�� ��ġ ��� ����
        });

        //������ �̵�(�÷��̾� ���� ���� -> ���� �̹��� ��Ȱ��ȭ)
        player.transform.DOLocalMoveZ(30f, 5f).SetRelative(); //5�ʰ� Z �������� 30��ŭ �̵�
        seq.AppendInterval(7f); //30�� ������
        seq.OnComplete(() => {
            //��ŷ ������ �̵�(�� ���� ��ŷ ����)
            this.GetComponent<SceneChange>().ChangeRankingScene();    //�� �ְ� �̵� 
        });
    }

    //���հ��̶�� ���ӿ��� ������ �̵�
    public void IsFail()
    {
        //���ӿ��� ������ �̵�
        this.GetComponent<SceneChange>().ChangeGameOverScene();
    }
}
