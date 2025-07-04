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
    private bool IsSinking = false; // 用于标记敌人是否正在下沉

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
        //移动物体
        if (IsSinking)
        {
            transform.Translate(-transform.up * Time.deltaTime); // 向下移动
        }
    }

    public void TakeDamage(int damage, Vector3 hitPoint)
    {
        //判断敌人是否已经死亡，如果死亡返回
        if (isDead == true)
        {
            return;
        }
        // 声音播放  
        enemyAudioSource.Play();
        // 粒子效果播放  
        enemyParticles.transform.position = hitPoint;
        enemyParticles.Play();

        // 检查敌人是否死亡  
        health  -= damage;
        Debug.Log("Enemy took damage: " + damage + ", remaining health: " + health);
        if (health <= 0)
        {
            Die(); // 调用死亡方法    
        }
           
        
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true; // 设置死亡标志
        Debug.Log("Enemy died.");
        if (enemyAnimator != null)
        {
            enemyAnimator.SetTrigger("Death"); // 触发Animator的Death参数
        }
        else
        {
            Debug.LogWarning("未找到Animator组件，无法播放死亡动画。");
        }
        enemyCollider.isTrigger = true; // 禁用碰撞体，防止进一步的碰撞检测
        GetComponent<NavMeshAgent>().enabled = false; // 禁用NavMeshAgent组件
        GetComponent<Rigidbody>().isKinematic = true; // 设置刚体为运动学，防止物理影响
        enemyAudioSource.clip = DieClip; // 设置死亡音频剪辑
        enemyAudioSource.Play();
    }

    public void StartSinking()
    {
        IsSinking = true;
        Destroy(gameObject, 2f); // 2秒后销毁敌人对象
    }
}
