using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private void FixedUpdate()
    {
        GetPossibility(0);
    }
    private float GetPossibility(int enemyIndex)
    {
        float a = (EnemyManager.instance.enemies[enemyIndex].maxNum - NumberAssignment.Instance.allynewNumber);
        float b = (EnemyManager.instance.enemies[enemyIndex].maxNum - EnemyManager.instance.enemies[enemyIndex].minNum);
        float winRate =a/b;
        Debug.Log(winRate.ToString());
        return 0;
    }
}