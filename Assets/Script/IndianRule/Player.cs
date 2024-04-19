using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string playerName;
    public int Hp; // �÷��̾��� Hp

    public Player(string name, int startingHp)
    {
        playerName = name;
        Hp = startingHp;
    }

    // ������ �߰��ϴ� �޼ҵ�
    public bool PlaceBet(int amount)
    {
        if (amount > Hp)
        {
            Debug.Log("Not enough chips!");
            return false;
        }
        else
        {
            Hp -= amount;
            Debug.Log(playerName + " bets " + amount);
            return true;
        }
    }

    // ������ ����ϴ� �޼ҵ�, Ĩ�� ���� ���� ����
    public void Fold()
    {
        Debug.Log(playerName + " folds");
        //this.chips -= GameManager.gameManager.bet;
        GameManager.gameManager.bet = 10;
        GameManager.gameManager.currentPot = 0;
    }
}
