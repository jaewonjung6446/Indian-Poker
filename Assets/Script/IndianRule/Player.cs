using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string playerName;
    public int Hp; // 플레이어의 Hp

    public Player(string name, int startingHp)
    {
        playerName = name;
        Hp = startingHp;
    }

    // 배팅을 추가하는 메소드
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

    // 배팅을 취소하는 메소드, 칩은 돌려 받지 못함
    public void Fold()
    {
        Debug.Log(playerName + " folds");
        //this.chips -= GameManager.gameManager.bet;
        GameManager.gameManager.bet = 10;
        GameManager.gameManager.currentPot = 0;
    }
}
