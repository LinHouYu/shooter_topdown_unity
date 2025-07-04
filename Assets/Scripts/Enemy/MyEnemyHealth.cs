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
        //声音播放
        enemyAudioSource.Play();
        // 粒子效果播放
        enemyParticles.transform.position = hitPoint; // 确保粒子效果在敌人位置
        enemyParticles.Play(); // 播放粒子效果

    }
    public int GetHealth()
    {
        return health;
    }
}
