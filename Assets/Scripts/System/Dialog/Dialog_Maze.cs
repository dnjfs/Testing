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

    //���� ���۽� �޽���
    private string[] startDialogText = {"�̷��� ��� ���� ������ ��� �ⱸ�� ���� ���Դϴ�.",
                                    "�������� ���� ���� �ⱸ�� Ż���Ͻʽÿ�.",
                                    "�׷� ����� ���ϴ�."};

    //��� ���� �� ������ �� �ý��� �޽���
    private string[] createElevatorText = { "�ⱸ�� ���Ƚ��ϴ�." };

    public void StartMessage()
    {
        //���� ���� �ý��� �޽���
        TypingEffect(startDialogText);
    }

    public void CreateElevatorMessage()
    {
        //���������� ���� �޽���
        TypingEffect(createElevatorText);
    }

    void TypingEffect(string[] textArray)
    {
        //Ÿ���� ȿ��

        backGround.gameObject.SetActive(true);  //�ؽ�Ʈ ��� Ȱ��ȭ
        messageText.gameObject.SetActive(true); //�ؽ�Ʈ Ȱ��ȭ

        Sequence seq = DOTween.Sequence();  //DOTween Sequence ����(Sequence: Tween���� �ð��� ������ ���� �迭�Ͽ� �ϳ��� ��� ����)
        seq.Append(backGround.DOFade(1f, 2f));  //�ؽ�Ʈ ��� ���̵� ȿ��(1f ����� 2f���� ����)

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

        });
    }
}
