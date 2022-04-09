using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Dialog_Corridor : MonoBehaviour
{
    //����� �޽���
    public Text messageText;
    public Image backGround;
    public Image talker;
    public Text talkerText;

    public Button whiteRoomSkipButton;  //��ȭâ ��ŵ ��ư

    public Image fog;   //fog �̹���(��Ȱ��ȭ �� ��)

    //�Ͼ�� �޽���
    private string theWhiteRoomDialogText = "Ż���� �����մϴ�. �׷��� ���� ���� �ƴմϴ�. \n" +
            "����� ���ϴ� �ش��� ���⼭ ã�� �� ���� ���Դϴ�.";

    //Ż�� �� �հ� �޽���
    private string passDialogText = "�հ��� �����մϴ�. \n" + "�ⱸ�� �����ø� �δ�� ���ϴ� ��Ⱑ ����� ��ٸ��� ���� ���Դϴ�. \n" +
        "�����κ��� �츮 �ΰ��� ������ �����ֽñ� �ٶ��ϴ�.";



    public void EnterWhiteRoomMessage()
    {
        fog.gameObject.SetActive(false);    //�Ȱ� ��Ȱ��ȭ

        //�Ͼ�� ���� �޽���
        messageText.text = ""; //�ؽ�Ʈ �ʱ�ȭ
        messageText.gameObject.SetActive(true);  //�ؽ�Ʈ Ȱ��ȭ
        backGround.gameObject.SetActive(true); //�ؽ�Ʈ ��� Ȱ��ȭ

        Sequence seq = DOTween.Sequence();  //DOTween Sequence ����
        seq.Append(backGround.DOFade(1f, 1f));  //�ؽ�Ʈ ��� ���̵� ȿ��
        seq.Join(talker.DOFade(1f, 1f));
        seq.Join(talkerText.DOFade(1f, 1f));
        seq.Append(messageText.DOText(theWhiteRoomDialogText, 5f));    //�ؽ�Ʈ ���
        //whiteRoomSkipButton.onClick.AddListener(delegate { this.GetComponent<writtenOath>().OpenReportCard(); });  //��ŵ ��ư �̺�Ʈ �߰�(���༭ ���� �ٷ� ����ǥ ����)
    }

    //�Ͼ�� ���� �޽����� ��Ȱ��ȭ �ϴ� �Լ�
    public void CloseWhiteDialog()
    {
        backGround.gameObject.SetActive(false); //�ؽ�Ʈ ��� ��Ȱ��ȭ
        messageText.gameObject.SetActive(false);    //�ؽ�Ʈ ��Ȱ��ȭ
        messageText.text = " ";    //�޽��� �ʱ�ȭ
    }

    //Ż�� �� �հ� �޽��� ���
    public void CreatePassMessage()
    {
        messageText.text = ""; //�ؽ�Ʈ �ʱ�ȭ
        messageText.gameObject.SetActive(true);  //�ؽ�Ʈ Ȱ��ȭ
        backGround.gameObject.SetActive(true); //�ؽ�Ʈ ��� Ȱ��ȭ
        whiteRoomSkipButton.gameObject.SetActive(false);  //��ŵ ��ư ��Ȱ��ȭ

        Sequence seq = DOTween.Sequence();  //DOTween Sequence ����(Sequence: Tween���� �ð��� ������ ���� �迭�Ͽ� �ϳ��� ��� ����)

        seq.Append(messageText.DOText(passDialogText, 5f));    //��� Ÿ���� ȿ��
        seq.AppendInterval(2f); //2�� ������
        seq.Append(messageText.DOFade(0f, 0.5f)); //�޽��� ���̵� ȿ��
        seq.Append(backGround.DOFade(0f, 0.5f)); //��� ���̵� ȿ��
        seq.Join(talker.DOFade(0f, 0.5f));
        seq.Join(talkerText.DOFade(0f, 0.5f));

        seq.Append(messageText.DOText("", 0.5f));    //��� �ʱ�ȭ
        seq.AppendInterval(1f); //1�� ������

        seq.OnComplete(() => {
            messageText.gameObject.SetActive(false);  //�ؽ�Ʈ ��Ȱ��ȭ
            backGround.gameObject.SetActive(false); //�ؽ�Ʈ ��� ��Ȱ��ȭ
        });
    }
}