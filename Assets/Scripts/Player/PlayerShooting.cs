using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
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
        //可以添加射击特效、音效等
        //例如：Instantiate(bulletPrefab, transform.position, transform.rotation);
    }
}
