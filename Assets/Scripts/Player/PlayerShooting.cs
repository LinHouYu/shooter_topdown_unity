using System;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private AudioSource GunAudio;
    float time = 0f; //记录上次射击的时间
    public float fireRate = 0.10f; //射击间隔时间
    private Light gunLight; //枪口光
    private float lightDuration = 0.5f; //枪口光持续时间


    //玩家射击脚本
    private void Awake()
    {
        //获取玩家的音频源组件
        GunAudio = GetComponent<AudioSource>();
        if (GunAudio == null)
        {
            Debug.LogError("PlayerShooting脚本需要附加在一个带有AudioSource组件的GameObject上。");
        }
        //获取枪口光组件
        gunLight = GetComponentInChildren<Light>();
        gunLight.enabled = false;
        if (gunLight == null)
        {
            Debug.LogError("PlayerShooting脚本需要附加在一个带有Light组件的GameObject上。");
        }
        else
        {
            gunLight.enabled = false;
        }
    }
    void Update()
    {
        time += Time.deltaTime;
        //获取用户的开火按键
        if (Input.GetButton("Fire1") && time >= fireRate)
        {
            //射击
            Shoot();
        }
        //如果枪口光开启了，持续一段时间后关闭
        if (time >= (fireRate * lightDuration))
        {
            gunLight.enabled = false; // 关闭枪口光
   
        }

    }
    void Shoot()
    {
        //在这里实现射击逻辑
        Debug.Log("Player is shooting!");
        Debug.Log(DateTime.Now.ToString("G"));
        GunAudio.Play();
        time = 0f; // Reset the time after shooting
        gunLight.enabled = true; // 开启枪口光
        
    }
}
