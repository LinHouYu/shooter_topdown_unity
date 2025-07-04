using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MvEnemyMovement : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent nav;
    private MyEnemyHealth enemyHealth; // ��������һ�����˽����ű�

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<MyEnemyHealth>();

    }

    // Update is called once per frame
    void Update()
    {
        //������������������Ļ����Ҿ�ÿһ֡������ʱ�򣬲��ٽ��е���������
        if(enemyHealth.health == 100)
        {
           nav.SetDestination(player.position); // ��Ȼ����Ŀ��λ�ã������ٸ���
        }
    }
}
