using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearTest : MonoBehaviour
{
    Repetition repetition;

    void Start()
    {
        repetition = GameObject.Find("GameSystem").GetComponent<Repetition>();
    }

    public void OnClickClear()
    {
        repetition.leftBlock.Clear();
    }
}
