using System;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private AudioSource GunAudio;
    float time = 0f; //记录上次射击的时间
    public float fireRate = 0.10f; //射击间隔时间
    private Light gunLight; //枪口光
    private float lightDuration = 0.5f; //枪口光持续时间
    private LineRenderer gunLine; //获取LineRenderer组件
    private ParticleSystem gunParticle; //获取粒子系统组件
    private Animator enemyAnimator; // 添加Animator变量

    //开枪发射射线相关变量
    private Ray ShootRay; //射线
    private RaycastHit shootHit; //射线碰撞信息
    private int shootMask; //射线检测的图层

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
        //获取LineRenderer组件
        gunLine = GetComponent<LineRenderer>();
        gunParticle = GetComponentInChildren<ParticleSystem>();
        //获取射线检测的图层
        shootMask = LayerMask.GetMask("Shootable"); // 假设你有一个名为"Shootable"的图层
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
            gunLine.enabled = false; // 关闭LineRenderer
        }
    }

    void Shoot()
    {
        time = 0f; // Reset the time after shooting
        //在这里实现射击逻辑
        Debug.Log("Player is shooting!");
        Debug.Log(DateTime.Now.ToString("G"));
        gunLight.enabled = true; // 开启枪口光
        gunLine.SetPosition(0, transform.position); // 设置LineRenderer的起点为玩家位置
        gunLine.SetPosition(1, transform.position + transform.forward * 100f); // 设置LineRenderer的终点为玩家前方100单位
        gunLine.enabled = true;
        gunParticle.Play(); // 播放枪口粒子效果
        // 播放枪声
        GunAudio.Play();
        // 射击逻辑可以在这里实现，比如检测射线碰撞等
        //定义ray, 定义一个mask定义 Hit
        if (Physics.Raycast(transform.position, transform.forward, out shootHit, 100f, shootMask))
        {
            gunLine.SetPosition(1, shootHit.point); // 设置LineRenderer的终点为射线碰撞点
            //如果射线检测到物体
            Debug.Log("Hit: " + shootHit.collider.name);
            //在这里可以添加击中物体的逻辑，比如伤害计算等
            //例如：shootHit.collider.GetComponent<Enemy>().TakeDamage(damageAmount);
            MyEnemyHealth enemyHealth = shootHit.collider.GetComponent<MyEnemyHealth>(); // 假设有一个MyEnemyHealth脚本处理敌人生命值
            if (enemyHealth != null) // 检查敌人生命值脚本是否存在
            {
                enemyHealth.TakeDamage(10, shootHit.point); // 只调用扣血，不再直接触发动画
            }
        }
        else
        {
            gunLine.SetPosition(1, transform.position + transform.forward * 100f);
            Debug.Log("Missed!");
        }
    }
}