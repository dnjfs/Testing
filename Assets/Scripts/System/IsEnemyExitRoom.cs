using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IsEnemyExitRoom : MonoBehaviour
{
    //���� ��� ��ȣ(�� ��° Enemy����)
    public int EnemyBlockNumber;
    
    void Start()
    {
        EnemyBlockNumber = GameObject.FindWithTag("GameSystem").GetComponent<EnemyStart>().createEnemy; //Enemy ��ȣ ����
    }
    
    //�浹���� ��� ��
    void OnTriggerExit(Collider other)
    {
        //�浹�� ������Ʈ�� Enemy���
        if (other.tag == "Monster")
        {
            //Enemy ��ȣ�� Enemy ���� ��� ��ȣ�� ��ġ�ϴٸ�
            if (other.GetComponent<Enemy>().EnemyNumber == EnemyBlockNumber)
            {
                StartCoroutine(DelayCloseDoor(other.GetComponent<Enemy>()));
            }
        }
    }
    private IEnumerator DelayCloseDoor(Enemy enemy)
    {
        yield return new WaitForSeconds(3f); //3�� �Ŀ� �� �ݱ�
        if (!enemy.isNear) //���� ��ó�� �÷��̾ ���� ���
            enemy.agent.speed = 0; //�����̵� �ϵ��� �׺�޽� ������Ʈ �ӵ��� 0���� ����
        GameObject.FindWithTag("GameSystem").GetComponent<EnemyStart>().ExitEnemyDoor(); //���� �ݴ´�
    }
}
