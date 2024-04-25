using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    [SerializeField] int startchip_player = 30;

    public List<Player> players = new List<Player>();
    public int currentPot = 0; // ���� ���� �Ѿ�
    public int bet; // ���þ�
    [SerializeField] private Text betamount;
    [SerializeField] private Text playeramount;
    [SerializeField] private Text betavailableamount;
    [SerializeField] private Text playerbetamount;
    [SerializeField] private Text enemyHp;
    [SerializeField] private Text enemyBet;
    public bool isPlayerTurn = true; // �� ������ ���� ����
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
        // ���� ���� �� �÷��̾� ������ ����
        isPlayerTurn = true;
        if (isPlayerTurn)
        {
            Debug.Log("Player's turn");
        }
        else
        {
            StartAITurn();
        }
        // �÷��̾� �ʱ�ȭ ����
        players.Add(new Player("�÷��̾�", startchip_player,1,10));
        Debug.Log("�÷��̾� ����");
        for (int i = 0; i < 100; i++)
        {
            int minNum = Random.Range(0, 6);
            int maxNum = Random.Range(minNum, 15);
            int hp = Random.Range(5, 40);
            players.Add(new Player("����"+i.ToString(), hp, minNum, maxNum));
        }
    }
    private void Update()
    {
        betamount.text = "��ü ���� ��:" + currentPot.ToString();
        playeramount.text = "�÷��̾� ��ȭ:" + players[0].Hp.ToString();
        betavailableamount.text = "���� �� :" + raiseamount.ToString();
        playerbetamount.text = ($"���� �� : {players[0].bet}");
        enemyHp.text = ($"�� Hp : {players[stage].Hp}");
        enemyBet.text = ($"�� ���� : {players[stage].bet}");
    }
    // �÷��̾��� ������ ó���ϴ� �޼ҵ�
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
            Debug.Log("����");
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
        Debug.Log($"�÷��̾� �ε���:{playerId}�� bet = {player.bet}");
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

    // ���� ���� ������Ʈ�ϴ� �޼ҵ�
    public void UpdatePot(int amount)
    {
        currentPot += amount;
    }

    // ���� ���� ���� ������ ��ȯ�ϴ� �޼ҵ�
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
        Debug.Log("����������, ����");
        EndPlayerTurn();
    }
    private void ExactCalculation()
    {
        if(NumberAssignment.Instance.allynewNumber > NumberAssignment.Instance.enemynewNumber)
        {
            players[0].Hp += this.currentPot;
            players[stage].bet = 0;
            players[0].bet = 0;
            Debug.Log("�÷��̾��" + currentPot+"����");
            this.bet = 3;
            this.currentPot = 5;
        }else if(NumberAssignment.Instance.allynewNumber < NumberAssignment.Instance.enemynewNumber)
        {
            players[stage].Hp += this.currentPot;
            players[stage].bet = 0;
            Debug.Log($"��{ stage}���� {currentPot} ����");
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
            Debug.Log($"����");
        }
        if (players[stage].Hp == 0)
        {
            GetHighestBet();
            stage++;
            Debug.Log("���� ��������" + stage);
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

    // AI �� ����
    public void EndAITurn()
    {
        isPlayerTurn = true;
    }
}
