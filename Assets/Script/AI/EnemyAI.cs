using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private float winRate;
    private void FixedUpdate()
    {
        Debug.Log(GetWinPossibility(GameManager.gameManager.stage));
    }
    private float GetWinPossibility(int stage)
    {
        float a = (GameManager.gameManager.players[stage].maxNum - NumberAssignment.Instance.allynewNumber);
        float b = (GameManager.gameManager.players[stage].maxNum - GameManager.gameManager.players[stage].minNum);
        if(a > b)
        {
            winRate = 1;
        }
        else
        {
            winRate = a / b;
        }
        Debug.Log(winRate.ToString());
        return winRate;
    }
}