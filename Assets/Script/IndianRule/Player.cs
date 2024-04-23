using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string playerName;
    public int Hp; // �÷��̾��� Hp
    public int bet;
    public int minNum;
    public int maxNum;
    public bool iswin = false;

    public Player(string name, int startingHp,int minNum, int maxNum)
    {
        playerName = name;
        Hp = startingHp;
        this.minNum = minNum;
        this.maxNum = maxNum;
    }

    // ������ �߰��ϴ� �޼ҵ�
    public bool PlaceBet(int amount)
    {
        if (amount > Hp)
        {
            return false;
        }
        else
        {
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
