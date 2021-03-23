using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{

    public Text startDialog;

    //게임 시작시 메시지
    private string[] startDialogText = {"미로의 모든 길을 지나는 즉시 출구가 열릴 것입니다.",
                                    "괴물들을 피해 열린 출구로 탈출하십시오.",
                                    "그럼 행운을 빕니다."};


    void Start()
    {
        //게임 시작 메시지 출력
        StartCoroutine(_typing());
    }

    IEnumerator _typing()
    {
        //메시지의 개수만큼 메시지 출력
        for (int j = 0; j < startDialogText.Length; j++)
        {
            //첫 딜레이 설정(2초)
            yield return new WaitForSeconds(3f);

            //타이핑 효과는 각 text의 길이만큼 반복
            for (int i = 0; i <= startDialogText[j].Length; i++)
            {
                //Substing(start index, 표현할 글자 개수) -> index부터 i까지의 글자를 표현 == 타이핑 효과
                startDialog.text = startDialogText[j].Substring(0, i);

                //한 번에 표현되지 않도록 딜레이를 줌
                yield return new WaitForSeconds(0.15f);
            }
        }

        //딜레이 설정
        yield return new WaitForSeconds(3f);

        //메시지 지우기
        startDialog.text = "";
    }
}
