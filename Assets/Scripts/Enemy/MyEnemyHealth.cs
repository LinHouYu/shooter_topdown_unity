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
    private bool IsSinking = false; // ���ڱ�ǵ����Ƿ������³�

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
        //�ƶ�����
        if (IsSinking)
        {
            transform.Translate(-transform.up * Time.deltaTime); // �����ƶ�
        }
    }

    public void TakeDamage(int damage, Vector3 hitPoint)
    {
        //�жϵ����Ƿ��Ѿ������������������
        if (isDead == true)
        {
            return;
        }
        // ��������  
        enemyAudioSource.Play();
        // ����Ч������  
        enemyParticles.transform.position = hitPoint;
        enemyParticles.Play();

        // �������Ƿ�����  
        health  -= damage;
        Debug.Log("Enemy took damage: " + damage + ", remaining health: " + health);
        if (health <= 0)
        {
            Die(); // ������������    
        }
           
        
    }

    private void Die()
    {
        if (isDead) return;
        isDead = true; // ����������־
        Debug.Log("Enemy died.");
        if (enemyAnimator != null)
        {
            enemyAnimator.SetTrigger("Death"); // ����Animator��Death����
        }
        else
        {
            Debug.LogWarning("δ�ҵ�Animator������޷���������������");
        }
        enemyCollider.isTrigger = true; // ������ײ�壬��ֹ��һ������ײ���
        GetComponent<NavMeshAgent>().enabled = false; // ����NavMeshAgent���
        GetComponent<Rigidbody>().isKinematic = true; // ���ø���Ϊ�˶�ѧ����ֹ����Ӱ��
        enemyAudioSource.clip = DieClip; // ����������Ƶ����
        enemyAudioSource.Play();
    }

    public void StartSinking()
    {
        IsSinking = true;
        Destroy(gameObject, 2f); // 2������ٵ��˶���
    }
}
