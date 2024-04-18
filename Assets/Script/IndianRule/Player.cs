using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string playerName;
    public int chips; // �÷��̾��� Ĩ ����

    public Player(string name, int startingChips)
    {
        playerName = name;
        chips = startingChips;
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
        //this.chips -= GameManager.gameManager.bet;
        GameManager.gameManager.bet = chips;
        GameManager.gameManager.currentPot = 0;
    }
}
