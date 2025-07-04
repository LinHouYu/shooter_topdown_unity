using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class MyEnemyHealth : MonoBehaviour
{
    public int health = 100;
    public bool isDead = false; // ���ڱ�ǵ����Ƿ�����
    public AudioSource enemyAudioSource;
    private ParticleSystem enemyParticles;
    public AudioClip DieClip;
    private Animator enemyAnimator; // ��Animator�滻Animation
    //private bool isDead = false; // ��ֹ�������
    private CapsuleCollider enemyCollider; // ���ڻ�ȡ���˵���ײ��

    private void Awake()
    {
        enemyAudioSource = GetComponent<AudioSource>();
        enemyParticles = GetComponentInChildren<ParticleSystem>();
        enemyAnimator = GetComponent<Animator>(); // ��ȡAnimator���
        enemyCollider = GetComponent<CapsuleCollider>(); // ��ȡ���˵���ײ��
    }

    // Update is called once per frame  
    void Update()
    {
    }

    public void TakeDamage(int damage, Vector3 hitPoint)
    {
        health -= damage;
        Debug.Log("Enemy took damage: " + damage + ", remaining health: " + health);
        // ��������  
        enemyAudioSource.Play();
        // ����Ч������  
        enemyParticles.transform.position = hitPoint;
        enemyParticles.Play();

        // �������Ƿ�����  
        health  -= damage;
        //if(health <= 0)
        
            Die();
        
    }

    private void Die()
    {

        enemyAnimator.SetTrigger("Death"); // ����Animator��Death����
        enemyCollider.enabled = false; // ������ײ�壬��ֹ��һ������ײ���
        enemyAudioSource.clip = DieClip; // ����������Ƶ����
        enemyAudioSource.Play();
    }
    void StartSinking()
    {
        GetComponent<NavMeshAgent>().enabled = false; // ����NavMeshAgent���
    }

}
