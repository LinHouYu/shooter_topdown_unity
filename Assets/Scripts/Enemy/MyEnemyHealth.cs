using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class MyEnemyHealth : MonoBehaviour
{
    public int health = 100;
    public bool isDead = false; // 用于标记敌人是否死亡
    public AudioSource enemyAudioSource;
    private ParticleSystem enemyParticles;
    public AudioClip DieClip;
    private Animator enemyAnimator; // 用Animator替换Animation
    //private bool isDead = false; // 防止多次死亡
    private CapsuleCollider enemyCollider; // 用于获取敌人的碰撞体

    private void Awake()
    {
        enemyAudioSource = GetComponent<AudioSource>();
        enemyParticles = GetComponentInChildren<ParticleSystem>();
        enemyAnimator = GetComponent<Animator>(); // 获取Animator组件
        enemyCollider = GetComponent<CapsuleCollider>(); // 获取敌人的碰撞体
    }

    // Update is called once per frame  
    void Update()
    {
    }

    public void TakeDamage(int damage, Vector3 hitPoint)
    {
        health -= damage;
        Debug.Log("Enemy took damage: " + damage + ", remaining health: " + health);
        // 声音播放  
        enemyAudioSource.Play();
        // 粒子效果播放  
        enemyParticles.transform.position = hitPoint;
        enemyParticles.Play();

        // 检查敌人是否死亡  
        health  -= damage;
        //if(health <= 0)
        
            Die();
        
    }

    private void Die()
    {

        enemyAnimator.SetTrigger("Death"); // 触发Animator的Death参数
        enemyCollider.enabled = false; // 禁用碰撞体，防止进一步的碰撞检测
        enemyAudioSource.clip = DieClip; // 设置死亡音频剪辑
        enemyAudioSource.Play();
    }
    void StartSinking()
    {
        GetComponent<NavMeshAgent>().enabled = false; // 禁用NavMeshAgent组件
    }

}
