using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject Player; // 玩家对象
    public Vector3 offset; // 摄像机与玩家之间的偏移量
    public float Smooting = 10f; // 平滑跟随的速度
    private void Awake()
    {
        if (Player == null)
        {
            Debug.LogError("CameraFollow脚本需要设置Player对象。");
        }
        Player = GameObject.FindGameObjectsWithTag("Player")[0];
    }
    private void Start()
    {
        if (Player == null)
        {
            Debug.LogError("CameraFollow脚本在Start方法中未找到Player对象。请确保Player对象已正确设置。");
        }
        // 设置摄像机的位置为玩家位置加上一个偏移量
        offset = transform.position - Player.transform.position;

    }
    private void FixedUpdate()
    {
        if (Player != null)
        {
            // 更新摄像机的位置，使其跟随玩家
            Vector3 newPosition = Player.transform.position + offset;
            transform.position = newPosition;
        }
        else
        {
            Debug.LogWarning("CameraFollow脚本在FixedUpdate方法中未找到Player对象。请确保Player对象已正确设置。");
        }
        transform.position = Vector3.Lerp(transform.position, Player.transform.position + offset, Smooting*Time.deltaTime); // 平滑跟随玩家 就是人物停了相机还会在往前移动一段距离

    }
}
