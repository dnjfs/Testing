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

    //블록이 플레이어와 충돌했다면(OnTriggerEnter로 해도 괜찮을 듯)
    void OnTriggerExit(Collider other)
    {
        //블록의 이름을 가져옴
        blockName = other.gameObject.name;
        //블록이 해당하는 블록 배열의 인덱스를 가져옴(이름에서 가져옴)
        splitText = blockName.Split('(');   //'('를 기준으로 string 분할

        string indexChar = splitText[1].Substring(0, 2);  //분할한 문자열의 첫번째, 두번째 값 가져옴
        index = int.Parse(indexChar); //char형 index를 int로 변환


        //블록별 중복 횟수를 저장하는 배열의 해당 인덱스번째의 값 1 증가
        //GameObject.FindWithTag("GameSystem").GetComponent<Repetition>().addCount(index);
        system.GetComponent<Repetition>().addCount(index);
    }
}
