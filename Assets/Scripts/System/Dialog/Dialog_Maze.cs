using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Dialog_Maze : MonoBehaviour
{
    //����� �޽���
    public Text messageText;
    public Image backGround;
    public Button skipButton;

    //�÷��̾�(���� ������ ����)
    GameObject player;

    //���� ���۽� �޽���
    private string[] startDialogText = {"�̷��� ��� ���� ������ ��� �ⱸ�� ���� ���Դϴ�.",
                                    "�������� ���� ���� �ⱸ�� Ż���Ͻʽÿ�.",
                                    "�츮�� ��ȣ�� ����� �ⱸ�� �̲� ���Դϴ�.",
                                    "�׷� ����� ���ϴ�."};

    //��� ���� �� ������ �� �ý��� �޽���
    private string[] createElevatorText = { "�ⱸ�� ���Ƚ��ϴ�.",
                                    "���������� ��ư�� ���� �̰��� Ż���ϼ���."};

    public void StartMessage()
    {
        //���� ���� �ý��� �޽���
        TypingEffect(startDialogText);

        //���⼭ �÷��̾� ������ ����
        //�÷��̾ ������ Ÿ�ֿ̹� Player�� SetPlayerLevel() ����
        player = GameObject.FindWithTag("Player").gameObject;
    }

    public void CreateElevatorMessage()
    {
        //���������� ���� �޽���
        TypingEffect(createElevatorText);
    }

    public void CloseElevatorMessage()
    {
        //���������� ���� �޽��� ��ŵ ��ư �̺�Ʈ
        bool isMessageActive = messageText.gameObject.activeSelf; //�޽����� Ȱ��ȭ �Ǿ��ִ��� ����

        if (isMessageActive)   //�޽����� Ȱ��ȭ �Ǿ��ִٸ�
        {
            //�޽����� ���̵� ȿ���� �ݴ´�.
            Sequence closeSeq = DOTween.Sequence();  //DOTween Sequence ����
            closeSeq.Append(backGround.DOFade(0f, 1f)); //�޽��� ���̵� ȿ��

            closeSeq.Join(messageText.gameObject.GetComponent<Text>().DOFade(0f, 0.5f));    //�޽��� ���� ���̵�

            closeSeq.OnComplete(() => {
                backGround.gameObject.SetActive(false);     //�޽��� ��� ��Ȱ��ȭ
                messageText.gameObject.SetActive(false);    //�ؽ�Ʈ ��Ȱ��ȭ
                messageText.text = " ";//�ؽ�Ʈ �ʱ�ȭ
                player.GetComponent<Player>().SetPlayerLevel(); //�÷��̾� ���� ����(������ ���� ����)
            });
        }
    }

    void TypingEffect(string[] textArray)
    {
        //Ÿ���� ȿ��
        messageText.text = " ";//�ؽ�Ʈ �ʱ�ȭ
        backGround.gameObject.SetActive(true);  //�ؽ�Ʈ ��� Ȱ��ȭ
        messageText.gameObject.SetActive(true); //�ؽ�Ʈ Ȱ��ȭ
        skipButton.gameObject.SetActive(true);  //��ŵ ��ư Ȱ��ȭ

        Sequence seq = DOTween.Sequence();  //DOTween Sequence ����(Sequence: Tween���� �ð��� ������ ���� �迭�Ͽ� �ϳ��� ��� ����)
        seq.Append(backGround.DOFade(1f, 2f));  //�ؽ�Ʈ ��� ���̵� ȿ��(1f ����� 2f���� ����)
        seq.Join(messageText.gameObject.GetComponent<Text>().DOFade(1f, 2f));

        float typingTime = 5f;
        for (int i = 0; i < textArray.Length; i++)
        {
            string currentStr = textArray[i];    //���� �޽����� ����� �� ���� ����

            seq.Append(messageText.DOText(currentStr, typingTime));    //������ ���� DOText Ʈ���� ����
            seq.Append(messageText.DOText("", 0.15f).SetDelay(2f));    //������ ���� DOText Ʈ���� ����
            typingTime--;
        }

        seq.Append(backGround.DOFade(0f, 2f));  //�ؽ�Ʈ ��� ���̵�ƿ� ȿ��(0f ����� 2f���� ����)
        seq.OnComplete(() => {
            backGround.gameObject.SetActive(false); //�ؽ�Ʈ ��� ��Ȱ��ȭ
            messageText.gameObject.SetActive(false);    //�ؽ�Ʈ ��Ȱ��ȭ
            messageText.text = " ";//�ؽ�Ʈ �ʱ�ȭ
            player.GetComponent<Player>().SetPlayerLevel(); //�÷��̾� ���� ����(������ ���� ����)
        });
    }
}
