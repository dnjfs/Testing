using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterCreate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //�÷��̾� �����Ǹ� �ý��� ���۸޽��� ���
        GameObject.FindWithTag("GameSystem").GetComponent<DialogManager>().StartMessage();
    }
}
