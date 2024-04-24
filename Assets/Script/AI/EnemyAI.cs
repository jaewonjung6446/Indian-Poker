using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private float winRate;
    public GameManager gameManager;
    public NumberAssignment numberAssignment;
    public static EnemyAI enemyAI;
    private float foldChance = 0;
    private float callChance = 0;
    private float raiseChance = 0;
    private void Awake()
    {
        enemyAI = this;
    }
    private float GetWinPossibility(int stage)
    {
        float a = (gameManager.players[stage].maxNum - numberAssignment.allynewNumber);
        float b = (gameManager.players[stage].maxNum - gameManager.players[stage].minNum);
        if(a > b)
        {
            winRate = 1;
        }
        else
        {
            winRate = a / b;
        }
        if(winRate < 0)
        {
            return 0;
        }
        Debug.Log(winRate.ToString());
        return winRate;
    }
    public  void Decision()
    {
        if (GetWinPossibility(gameManager.stage)<0.3)
        {
            foldChance = 4;
            callChance = 3;
            raiseChance = 3;
            int decision = Random.Range(1, 11);
            if (decision <= 4)
            {
                gameManager.Fold(gameManager.stage);
                Debug.Log("폴드");
            }else if(decision <= 7&& 4<decision)
            {
                gameManager.Call(gameManager.stage);
                Debug.Log("콜");
            }
            else
            {
                gameManager.Raise(gameManager.stage);
                Debug.Log("레이즈");
            }
        }
        else if (GetWinPossibility(gameManager.stage) < 0.7 && 0.3 < GetWinPossibility(gameManager.stage))
        {
            foldChance = 4;
            callChance = 3;
            raiseChance = 3;
            int decision = Random.Range(1, 11);
            if (decision <= 4)
            {
                gameManager.Fold(gameManager.stage);
                Debug.Log("폴드");
            }
            else if (decision <= 7 && 4 < decision)
            {
                gameManager.Call(gameManager.stage);
                Debug.Log("콜");
            }
            else
            {
                gameManager.Raise(gameManager.stage);
                Debug.Log("레이즈");
            }
        }
        else if (0.7 <= GetWinPossibility(gameManager.stage))
        {
            foldChance = 4;
            callChance = 3;
            raiseChance = 3;
            int decision = Random.Range(1, 11);
            if (decision <= 4)
            {
                gameManager.Fold(gameManager.stage);
                Debug.Log("폴드");
            }
            else if (decision <= 7 && 4 < decision)
            {
                gameManager.Call(gameManager.stage);
                Debug.Log("콜");
            }
            else
            {
                gameManager.Raise(gameManager.stage);
                Debug.Log("레이즈");
            }
        }
    }
}