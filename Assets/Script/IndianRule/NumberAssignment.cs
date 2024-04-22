using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberAssignment : MonoBehaviour
{
    public static NumberAssignment Instance;
    public Button assignButton;  // ���� ���� ��ư
    public Text EnemyNum;
    //public Text AllyNum;
    public int allyexistingNumber;  // �̹� �������� ����
    public int enemyexistingNumber;
    public int allynewNumber=-1;
    public int enemynewNumber=-1;
    //public int stage= 0;
    private List<int> allyavailableNumbers;  // ��� ������ ���� ����Ʈ
    private List<int> enemyavailableNumbers;  // ��� ������ ���� ����Ʈ

    void Start()
    {
        Instance = this;
        //IntiallizeNum_Ally();
        IntiallizeNum_Ally();
        InitializeNumbers(1);
        AssignNewNumber();
        assignButton.onClick.AddListener(AssignNewNumber);
    }

    void InitializeNumbers(int stage)
    {
        enemyavailableNumbers = new List<int>();

        for (int i = GameManager.gameManager.players[stage].minNum; i <= GameManager.gameManager.players[stage].maxNum; i++)
        {
            if (i != enemyexistingNumber)  // ���� ���� ����
            {
                enemyavailableNumbers.Add(i);
            }
        }
    }
    void IntiallizeNum_Ally()
    {
        allyavailableNumbers = new List<int>();
        for (int i = 1; i <= 10; i++)
        {
            if (i != allyexistingNumber)  // ���� ���� ����
            {
                allyavailableNumbers.Add(i);
            }
        }
    }

    void AssignNewNumber()
    {
        GameManager.gameManager.stage++;
        Debug.Log($"���� �������� = {GameManager.gameManager.stage}");
        InitializeNumbers(GameManager.gameManager.stage);
        if (allyavailableNumbers.Count > 0)
        {
            int allyindex = Random.Range(0, allyavailableNumbers.Count);
            allynewNumber = allyavailableNumbers[allyindex];
            allyavailableNumbers.RemoveAt(allyindex);  // ���� ���� ����
            //AllyNum.text = allynewNumber.ToString();
            Debug.Log($"�Ʊ� ���ο� ����: {allynewNumber}");
        }else
        {
            Debug.Log("�Ʊ��� �� �̻� ��� ������ ���ڰ� �����ϴ�. ī�带 ���ġ�մϴ�");
            IntiallizeNum_Ally();
        }
        if (enemyavailableNumbers.Count > 0)
        {
            int enemyindex = Random.Range(0, enemyavailableNumbers.Count);
            enemynewNumber = enemyavailableNumbers[enemyindex];
            enemyavailableNumbers.RemoveAt(enemyindex);  // ���� ���� ����
            EnemyNum.text = enemynewNumber.ToString();
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
