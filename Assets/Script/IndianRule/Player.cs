using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string playerName;
    public int Hp; // �÷��̾��� Hp
    public int bet;
    public bool iswin = false;

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
        this.bet = 0;
    }
}
