using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private List<Player> players = new List<Player>();
    private int currentPot = 0; // 현재 팟의 총액
    public Text betamount;
    public Text playeramount;

    void Start()
    {
        // 플레이어 초기화 예시
        players.Add(new Player("플레이어", 0));
        players.Add(new Player("적군", 0));
    }

    // 플레이어의 배팅을 처리하는 메소드
    public void Call(int playerId)
    {
        Player player = players[playerId];
        int highestBet = GetHighestBet();
        int betDifference = highestBet - player.currentBet;

        if (player.PlaceBet(betDifference))
        {
            UpdatePot(betDifference);
        }
    }

    public void Raise(int playerId)
    {
        Player player = players[playerId];
        if (player.PlaceBet(GetHighestBet() - player.currentBet + 10))
        {
            UpdatePot(GetHighestBet() - player.currentBet + 10);
        }
    }

    public void Fold(int playerId)
    {
        players[playerId].Fold();
    }

    // 현재 팟을 업데이트하는 메소드
    private void UpdatePot(int amount)
    {
        currentPot += amount;
        Debug.Log("Current pot: " + currentPot);
        betamount.text = "전체 배팅 액:"+currentPot.ToString();
        playeramount.text = "플레이어 재화:"+players[0].chips.ToString();

    }

    // 현재 가장 높은 배팅을 반환하는 메소드
    private int GetHighestBet()
    {
        int highestBet = 0;
        foreach (Player p in players)
        {
            if (p.currentBet > highestBet)
            {
                highestBet = p.currentBet;
            }
        }
        return highestBet;
    }
}
