using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string playerName;
    public int Hp; // 플레이어의 Hp
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

    // 배팅을 추가하는 메소드
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

    // 배팅을 취소하는 메소드, 칩은 돌려 받지 못함
    public void Fold()
    {
        Debug.Log(playerName + " folds");
        //this.chips -= GameManager.gameManager.bet;
        this.bet = 0;
    }
}
