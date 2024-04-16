using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberAssignment : MonoBehaviour
{
    public Button assignButton;  // ���� ���� ��ư
    public Text EnemyNum;
    public int allyexistingNumber;  // �̹� �������� ����
    public int enemyexistingNumber;
    int allynewNumber=-1;
    int enemynewNumber=-1;
    private List<int> allyavailableNumbers;  // ��� ������ ���� ����Ʈ
    private List<int> enemyavailableNumbers;  // ��� ������ ���� ����Ʈ

    void Start()
    {
        InitializeNumbers();
        assignButton.onClick.AddListener(AssignNewNumber);
    }

    void InitializeNumbers()
    {
        allyavailableNumbers = new List<int>();
        enemyavailableNumbers = new List<int>();
        for (int i = 1; i <= 10; i++)
        {
            if (i != allyexistingNumber)  // ���� ���� ����
            {
                allyavailableNumbers.Add(i);
            }
        }
        for (int i = EnemyManager.instance.enemies[0].minNum; i <= EnemyManager.instance.enemies[0].maxNum; i++)
        {
            if (i != enemyexistingNumber)  // ���� ���� ����
            {
                enemyavailableNumbers.Add(i);
            }
        }
        EnemyNum.text = "";
    }

    void AssignNewNumber()
    {
        if (allyavailableNumbers.Count > 0)
        {
            int allyindex = Random.Range(0, allyavailableNumbers.Count);
            allynewNumber = allyavailableNumbers[allyindex];
            allyavailableNumbers.RemoveAt(allyindex);  // ���� ���� ����
            EnemyNum.text = allynewNumber.ToString();
            Debug.Log($"�Ʊ� ���ο� ����: {allynewNumber}");
        }else
        {
            Debug.Log("�Ʊ��� �� �̻� ��� ������ ���ڰ� �����ϴ�.");
        }
        if (enemyavailableNumbers.Count > 0)
        {
            int enemyindex = Random.Range(0, enemyavailableNumbers.Count);
            enemynewNumber = enemyavailableNumbers[enemyindex];
            enemyavailableNumbers.RemoveAt(enemyindex);  // ���� ���� ����
            Debug.Log($"���� ���ο� ����: {enemynewNumber}");
        } else
        {
            Debug.Log("������ �� �̻� ��� ������ ���ڰ� �����ϴ�.");
        }
        if(allynewNumber > enemynewNumber)
        {
            Debug.Log("�Ʊ� �¸�");
        }
        else if(allynewNumber < enemynewNumber)
        {
            Debug.Log("���� �¸�");
        }
        else
        {
            Debug.Log("����");
        }
    }
}
