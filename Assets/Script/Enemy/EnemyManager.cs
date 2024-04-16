using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    // Enemy Ŭ���� ����
    public class Enemy
    {
        public int minNum;
        public int maxNum;
        public int health;

        public Enemy()
        {
            //�ϴ� ���Ƿ� ���� ���������, ���߿� ���������� ���� ���� �ʿ�
            //�̰� NumgerAssignment�� ���������� �����
            this.minNum = Random.Range(0, 6);
            this.maxNum = Random.Range(minNum, 15);
            this.health = Random.Range(5, 40);
        }
    }

    // ���� ������ ť
    public List<Enemy> enemies = new List<Enemy>();

    // �� ���� �� ť�� �߰�
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        // �� 10�� ���� ����
        for (int i = 0; i < 10; i++)
        {
            AddEnemy();
        }
    }

    // ���� ť�� �߰��ϴ� �Լ�
    public void AddEnemy()
    {
        Enemy newEnemy = new Enemy();
        enemies.Add(newEnemy);
    }
}
