using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    // Enemy 클래스 정의
    public class Enemy
    {
        public int minNum;
        public int maxNum;
        public int health;

        public Enemy()
        {
            //일단 임의로 만든 스펙생성기, 나중에 레벨에따른 범위 설정 필요
            //이걸 NumgerAssignment에 범위설정에 써야함
            this.minNum = Random.Range(0, 6);
            this.maxNum = Random.Range(minNum, 15);
            this.health = Random.Range(5, 40);
        }
    }

    // 적을 저장할 큐
    public List<Enemy> enemies = new List<Enemy>();

    // 적 생성 및 큐에 추가
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        // 적 10명 생성 예시
        for (int i = 0; i < 10; i++)
        {
            AddEnemy();
        }
    }

    // 적을 큐에 추가하는 함수
    public void AddEnemy()
    {
        Enemy newEnemy = new Enemy();
        enemies.Add(newEnemy);
    }
}
