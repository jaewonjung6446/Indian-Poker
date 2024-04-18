using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string playerName;
    public int chips; // �÷��̾��� Ĩ ����
    public int currentBet; // ���� ���忡�� �÷��̾ �� ����

    public Player(string name, int startingChips)
    {
        playerName = name;
        chips = startingChips;
        currentBet = 5;
    }

    // ������ �߰��ϴ� �޼ҵ�
    public bool PlaceBet(int amount)
    {
        if (amount > chips)
        {
            Debug.Log("Not enough chips!");
            return false;
        }
        else
        {
            chips -= amount;
            Debug.Log(playerName + " bets " + amount);
            return true;
        }
    }

    // ������ ����ϰ� Ĩ�� �����޴� �޼ҵ�
    public void Fold()
    {
        Debug.Log(playerName + " folds");
        currentBet = 0;
    }
}
