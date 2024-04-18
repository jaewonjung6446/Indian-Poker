using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string playerName;
    public int chips; // 플레이어의 칩 개수
    public int currentBet; // 현재 라운드에서 플레이어가 건 배팅

    public Player(string name, int startingChips)
    {
        playerName = name;
        chips = startingChips;
        currentBet = 5;
    }

    // 배팅을 추가하는 메소드
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

    // 배팅을 취소하고 칩을 돌려받는 메소드
    public void Fold()
    {
        Debug.Log(playerName + " folds");
        currentBet = 0;
    }
}
