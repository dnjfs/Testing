using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMessage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //�÷��̾� �����Ǹ� �ý��� ���۸޽��� ���
        GameObject.FindWithTag("GameSystem").GetComponent<DialogManager>().StartMessage();
    }
}
