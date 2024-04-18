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
    public int currentPot = 0; // ���� ���� �Ѿ�
    public int bet = 10; // ���þ�
    public Text betamount;
    public Text playeramount;
    public Text betavailableamount;

    private void Awake()
    {
        gameManager = this;
    }
    void Start()
    {
        // �÷��̾� �ʱ�ȭ ����
        players.Add(new Player("�÷��̾�", startchip_player));
        players.Add(new Player("����", startchip_enemy));
        betamount.text = "��ü ���� ��:" + currentPot.ToString();
        playeramount.text = "�÷��̾� ��ȭ:" + players[0].chips.ToString();
    }
    private void Update()
    {
        betavailableamount.text = "���� �� :" + bet.ToString();
    }
    // �÷��̾��� ������ ó���ϴ� �޼ҵ�
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
        playeramount.text = "�÷��̾� ��ȭ:" + players[0].chips.ToString();
    }

    // ���� ���� ������Ʈ�ϴ� �޼ҵ�
    public void UpdatePot(int amount)
    {
        currentPot += amount;
        Debug.Log("Current pot: " + currentPot);
        betamount.text = "��ü ���� ��:"+currentPot.ToString();
        playeramount.text = "�÷��̾� ��ȭ:"+players[0].chips.ToString();

    }

/*    // ���� ���� ���� ������ ��ȯ�ϴ� �޼ҵ�
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
