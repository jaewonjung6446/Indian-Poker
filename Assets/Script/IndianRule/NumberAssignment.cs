using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberAssignment : MonoBehaviour
{
    public static NumberAssignment Instance;
    public Button assignButton;  // 숫자 배정 버튼
    public Text EnemyNum;
    //public Text AllyNum;
    public int allyexistingNumber;  // 이미 배정받은 숫자
    public int enemyexistingNumber;
    public int allynewNumber=-1;
    public int enemynewNumber=-1;
    //public int stage= 0;
    private List<int> allyavailableNumbers;  // 사용 가능한 숫자 리스트
    private List<int> enemyavailableNumbers;  // 사용 가능한 숫자 리스트

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
            if (i != enemyexistingNumber)  // 기존 숫자 제외
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
            if (i != allyexistingNumber)  // 기존 숫자 제외
            {
                allyavailableNumbers.Add(i);
            }
        }
    }

    void AssignNewNumber()
    {
        GameManager.gameManager.stage++;
        Debug.Log($"현재 스테이지 = {GameManager.gameManager.stage}");
        InitializeNumbers(GameManager.gameManager.stage);
        if (allyavailableNumbers.Count > 0)
        {
            int allyindex = Random.Range(0, allyavailableNumbers.Count);
            allynewNumber = allyavailableNumbers[allyindex];
            allyavailableNumbers.RemoveAt(allyindex);  // 뽑힌 숫자 제거
            //AllyNum.text = allynewNumber.ToString();
            Debug.Log($"아군 새로운 숫자: {allynewNumber}");
        }else
        {
            Debug.Log("아군에 더 이상 사용 가능한 숫자가 없습니다. 카드를 재배치합니다");
            IntiallizeNum_Ally();
        }
        if (enemyavailableNumbers.Count > 0)
        {
            int enemyindex = Random.Range(0, enemyavailableNumbers.Count);
            enemynewNumber = enemyavailableNumbers[enemyindex];
            enemyavailableNumbers.RemoveAt(enemyindex);  // 뽑힌 숫자 제거
            EnemyNum.text = enemynewNumber.ToString();
            Debug.Log($"적군 새로운 숫자: {enemynewNumber}");
        } else
        {
            Debug.Log("적군에 더 이상 사용 가능한 숫자가 없습니다.");
        }
        if(allynewNumber > enemynewNumber)
        {
            Debug.Log("아군 승리");
        }
        else if(allynewNumber < enemynewNumber)
        {
            Debug.Log("적군 승리");
        }
        else
        {
            Debug.Log("동률");
        }
    }
}
