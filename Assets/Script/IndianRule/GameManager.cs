using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    [SerializeField] int startchip_player = 30;
    [SerializeField] int startchip_enemy = 30;

    private List<Player> players = new List<Player>();
    public int currentPot = 0; // 현재 팟의 총액
    public int bet = 10; // 배팅액
    public Text betamount;
    public Text playeramount;
    public Text betavailableamount;

    private void Awake()
    {
        gameManager = this;
    }
    void Start()
    {
        // 플레이어 초기화 예시
        players.Add(new Player("플레이어", startchip_player));
        players.Add(new Player("적군", startchip_enemy));
        betamount.text = "전체 배팅 액:" + currentPot.ToString();
        playeramount.text = "플레이어 재화:" + players[0].chips.ToString();
    }
    private void Update()
    {
        betavailableamount.text = "배팅 액 :" + bet.ToString();
    }
    // 플레이어의 배팅을 처리하는 메소드
    public void Call(int playerId)
    {
        Player player = players[playerId];

        if (player.PlaceBet(currentPot - bet))
        {
            UpdatePot(currentPot - bet);
        }
    }

    public void Raise(int playerId)
    {
        Player player = players[playerId];
        if(player.PlaceBet(bet))
        {
            UpdatePot(bet);
        }

    }

    public void Fold(int playerId)
    {
        players[playerId].Fold();
        playeramount.text = "플레이어 재화:" + players[0].chips.ToString();
    }

    // 현재 팟을 업데이트하는 메소드
    public void UpdatePot(int amount)
    {
        currentPot += amount;
        Debug.Log("Current pot: " + currentPot);
        betamount.text = "전체 배팅 액:"+currentPot.ToString();
        playeramount.text = "플레이어 재화:"+players[0].chips.ToString();

    }

/*    // 현재 가장 높은 배팅을 반환하는 메소드
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
*/
}
