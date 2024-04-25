using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    [SerializeField] int startchip_player = 30;

    public List<Player> players = new List<Player>();
    public int currentPot = 0; // 현재 팟의 총액
    public int bet; // 배팅액
    [SerializeField] private Text betamount;
    [SerializeField] private Text playeramount;
    [SerializeField] private Text betavailableamount;
    [SerializeField] private Text playerbetamount;
    [SerializeField] private Text enemyHp;
    [SerializeField] private Text enemyBet;
    public bool isPlayerTurn = true; // 턴 관리를 위한 변수
    public int currentCost = 3;
    public int stage = 1;
    private int raiseamount = 3;
    private bool suggestEnd = false;
    private void Awake()
    {
        gameManager = this;
    }
    void Start()
    {
        // 게임 시작 시 플레이어 턴으로 시작
        isPlayerTurn = true;
        if (isPlayerTurn)
        {
            Debug.Log("Player's turn");
        }
        else
        {
            StartAITurn();
        }
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
        enemyHp.text = ($"적 Hp : {players[stage].Hp}");
        enemyBet.text = ($"적 배팅 : {players[stage].bet}");
    }
    // 플레이어의 배팅을 처리하는 메소드
    public void Call(int playerId)
    {
        Player player = players[playerId];
        if (player.PlaceBet(GetHighestBet()))
        {
            player.bet = GetHighestBet();
            UpdatePot(GetHighestBet());
            player.Hp -= GetHighestBet();
        }
        else if(!player.PlaceBet(GetHighestBet()))
        {
            UpdatePot(player.Hp);
            player.Hp = 0;
            ExactCalculation();
        }

        if (suggestEnd)
        {
            Debug.Log("정산");
            ExactCalculation();
        }else if (!suggestEnd)
        {
            suggestEnd = true;
        }
        if (isPlayerTurn)
        {
            EndPlayerTurn();
        }
    }
        
    public void Raise(int playerId)
    {
        Player player = players[playerId];
        player.bet = GetHighestBet()+raiseamount;
        Debug.Log($"플레이어 인덱스:{playerId}의 bet = {player.bet}");
        if (player.PlaceBet(player.bet))
        {
            UpdatePot(bet);
            player.Hp -= player.bet;
        }
        else if(!player.PlaceBet(player.bet))
        {
            UpdatePot(player.Hp);
            player.Hp = 0;
            ExactCalculation();
        }

        if (isPlayerTurn)
        {
            EndPlayerTurn();
        }
        suggestEnd = false;
    }

    public void Fold(int playerId)
    {
        foreach (Player a in players)
        {
            a.Fold();
        }
        this.currentPot = 0;
        if(playerId ==0)
        {
            players[0].iswin = false;
            players[stage].iswin = true;
        }
        else if(playerId == stage)
        {
            players[stage].iswin = false;
            players[0].iswin = true;
        }
        suggestEnd = false;
        EndPhase_Fold();
        NumberAssignment.Instance.AssignNewNumber();
    }

    // 현재 팟을 업데이트하는 메소드
    public void UpdatePot(int amount)
    {
        currentPot += amount;
    }

    // 현재 가장 높은 배팅을 반환하는 메소드
    public int GetHighestBet()
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
        if (isPlayerTurn)
        {
            EndPlayerTurn();
        }
        this.currentPot = 5;
    }
    private void EndPhase()
    {
        Debug.Log("엔드페이즈, 정산");
        EndPlayerTurn();
    }
    private void ExactCalculation()
    {
        if(NumberAssignment.Instance.allynewNumber > NumberAssignment.Instance.enemynewNumber)
        {
            players[0].Hp += this.currentPot;
            players[stage].bet = 0;
            players[0].bet = 0;
            Debug.Log("플레이어에게" + currentPot+"지급");
            this.bet = 3;
            this.currentPot = 5;
        }else if(NumberAssignment.Instance.allynewNumber < NumberAssignment.Instance.enemynewNumber)
        {
            players[stage].Hp += this.currentPot;
            players[stage].bet = 0;
            Debug.Log($"적{ stage}에게 {currentPot} 지급");
            players[0].bet = 0;
            this.bet = 3;
            this.currentPot = 5;
        }
        else if(NumberAssignment.Instance.allynewNumber == NumberAssignment.Instance.enemynewNumber)
        {
            players[0].Hp += players[0].bet;
            players[0].bet = 0;
            players[stage].Hp += players[stage].bet;
            players[stage].bet = 0;
            this.bet = 3;
            this.currentPot = 5;
            Debug.Log($"동률");
        }
        if (players[stage].Hp == 0)
        {
            GetHighestBet();
            stage++;
            Debug.Log("현재 스테이지" + stage);
        }
        NumberAssignment.Instance.AssignNewNumber();
    }
    public void EndPlayerTurn()
    {
        isPlayerTurn = false;
        StartAITurn();
    }
    private void StartAITurn()
    {
        if (isPlayerTurn)
        {
            EndAITurn();
        }
        EnemyAI.enemyAI.Decision();
        EndAITurn();
    }

    // AI 턴 종료
    public void EndAITurn()
    {
        isPlayerTurn = true;
    }
}
