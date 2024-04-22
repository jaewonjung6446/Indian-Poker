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
    public int currentPot = 0; // ���� ���� �Ѿ�
    public int bet = 10; // ���þ�
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
    }
    // �÷��̾��� ������ ó���ϴ� �޼ҵ�
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
        //currentPot�� 0�� �ǰ� �¸������� �Űܰ��� �޼ҵ� �ʿ�
        this.currentPot = 0;
        //playeramount.text = "�÷��̾� ��ȭ:" + players[0].Hp.ToString();
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

    // ���� ���� ������Ʈ�ϴ� �޼ҵ�
    public void UpdatePot(int amount)
    {
        currentPot += amount;
        Debug.Log("Current pot: " + currentPot);

    }

    // ���� ���� ���� ������ ��ȯ�ϴ� �޼ҵ�
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
        Debug.Log("����������, ����");
    }
}
