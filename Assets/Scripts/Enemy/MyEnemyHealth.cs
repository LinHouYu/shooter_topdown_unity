using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MyEnemyHealth : MonoBehaviour
{
    public int health = 100;
    public AudioSource enemyAudioSource;
    private ParticleSystem enemyParticles;
    private void Awake()
    {
        enemyAudioSource = GetComponent<AudioSource>();
        enemyParticles = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int damage, Vector3 hitPoint)
    {
        health -= damage;
        Debug.Log("Enemy took damage: " + damage + ", remaining health: " + health);
        //��������
        enemyAudioSource.Play();
        // ����Ч������
        enemyParticles.transform.position = hitPoint; // ȷ������Ч���ڵ���λ��
        enemyParticles.Play(); // ��������Ч��

    }
    public int GetHealth()
    {
        return health;
    }
}
