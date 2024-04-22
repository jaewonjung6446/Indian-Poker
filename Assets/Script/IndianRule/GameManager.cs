using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    [SerializeField] int startchip_player = 30;
    [SerializeField] int startchip_enemy = 30;

    public List<Player> players = new List<Player>();
    public int currentPot = 0; // 현재 팟의 총액
    public int bet = 10; // 배팅액
    [SerializeField] private Text betamount;
    [SerializeField] private Text playeramount;
    [SerializeField] private Text betavailableamount;
    [SerializeField] private Text playerbetamount;
    public int currentCost = 3;
    public int stage = 0;
    private int raiseamount = 3;
    private bool suggestEnd = false;
    private void Awake()
    {
        gameManager = this;
    }
    void Start()
    {
        // 플레이어 초기화 예시
        players.Add(new Player("플레이어", startchip_player,1,10));
        Debug.Log("플레이어 생성");
        for (int i = 0; i < 100; i++)
        {
            int minNum = Random.Range(0, 6);
            int maxNum = Random.Range(minNum, 15);
            int hp = Random.Range(5, 40);
            players.Add(new Player("적군"+i.ToString(), hp, minNum, maxNum));
        }
    }
    private void Update()
    {
        betamount.text = "전체 배팅 액:" + currentPot.ToString();
        playeramount.text = "플레이어 재화:" + players[0].Hp.ToString();
        betavailableamount.text = "배팅 액 :" + raiseamount.ToString();
        playerbetamount.text = ($"배팅 액 : {players[0].bet}");
    }
    // 플레이어의 배팅을 처리하는 메소드
    public void Call(int playerId)
    {
        Player player = players[playerId];

        if (suggestEnd)
        {
            EndPhase();
        }
        if (player.PlaceBet(currentPot - bet))
        {
            player.bet = GetHighestBet();
            UpdatePot(currentPot - bet);
        }
        suggestEnd = true;
    }

    public void Raise(int playerId)
    {
        Player player = players[playerId];
        player.bet += raiseamount;
        player.Hp -= bet;
        UpdatePot(player.bet);
    }

    public void Fold(int playerId)
    {
        players[playerId].Fold();
        //currentPot이 0이 되고 승리쪽으로 옮겨가는 메소드 필요
        this.currentPot = 0;
        //playeramount.text = "플레이어 재화:" + players[0].Hp.ToString();
        if(playerId ==0)
        {
            players[0].iswin = false;
            players[1].iswin = true;
        }
        else if(playerId == 1)
        {
            players[1].iswin = false;
            players[0].iswin = true;
        }
        EndPhase_Fold();
    }

    // 현재 팟을 업데이트하는 메소드
    public void UpdatePot(int amount)
    {
        currentPot += amount;
        Debug.Log("Current pot: " + currentPot);

    }

    // 현재 가장 높은 배팅을 반환하는 메소드
    private int GetHighestBet()
    {
        int highestBet = 0;
        foreach (Player p in players)
        {
            if (p.bet > highestBet)
            {
                highestBet = p.bet;
            }
        }
        return highestBet;
    }
    private void EndPhase_Fold()
    {
        foreach(Player a in players)
        {
            if (a.iswin)
            {
                a.Hp += currentPot;
            }
        }
    }
    private void EndPhase()
    {
        Debug.Log("엔드페이즈, 정산");
    }
}
