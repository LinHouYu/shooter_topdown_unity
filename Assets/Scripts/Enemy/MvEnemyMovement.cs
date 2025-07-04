using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MvEnemyMovement : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent nav;
    private MyEnemyHealth enemyHealth; // 假设你有一个敌人健康脚本

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<MyEnemyHealth>();

    }

    // Update is called once per frame
    void Update()
    {
        //死否死亡，如果死亡的话，我就每一帧进来的时候，不再进行导航的设置
        if(enemyHealth.health == 100)
        {
           nav.SetDestination(player.position); // 仍然设置目标位置，但不再更新
        }
    }
}
