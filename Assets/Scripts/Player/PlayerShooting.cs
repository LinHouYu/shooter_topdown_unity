using System;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private AudioSource GunAudio;
    float fireRate = 0.5f; //射击间隔时间
    private void Awake()
    {
        //获取玩家的音频源组件
        GunAudio = GetComponent<AudioSource>();
        if (GunAudio == null)
        {
            Debug.LogError("PlayerShooting脚本需要附加在一个带有AudioSource组件的GameObject上。");
        }
    }
    void Update()
    {
        //获取用户的开火按键
        if (Input.GetButtonDown("Fire1"))
        {
            //射击
            Shoot();
        }

        //发射射线，检测有没有击中敌人

    }




    void Shoot()
    {
        //在这里实现射击逻辑
        Debug.Log("Player is shooting!");
        Debug.Log(DateTime.Now.ToString("G"));
        GunAudio.Play();
        //可以添加射击特效、音效等
        //例如：Instantiate(bulletPrefab, transform.position, transform.rotation);
    }
}
