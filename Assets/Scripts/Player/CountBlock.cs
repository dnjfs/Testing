using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountBlock : MonoBehaviour
{
    string blockName;
    int index;

    public string[] splitText = new string[2];

    GameObject system;

    void Start()
    {
        blockName = "";
        index = 0;

        system = GameObject.FindWithTag("GameSystem");
    }

    //충돌을 벗어나면
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "CountBlocks")
        {
            //블록 이름을 가져옴
            blockName = other.gameObject.name;
            //블록 이름의 숫자를 저장함(블록 번호)
            splitText = blockName.Split('(');  

            string indexChar = splitText[1].Substring(0, 2);  //블록 번호는 두자릿수 인덱스
            index = int.Parse(indexChar); //문자를 숫자로 바꿈


            //중복성 검사 스크립트의 addCount 함수에 충돌한 블록의 인덱스 전달
            //GameObject.FindWithTag("GameSystem").GetComponent<Repetition>().addCount(index);
            system.GetComponent<Repetition>().addCount(index);
        }
    }
}
