using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // 玩家移动速度
    public float moveSpeed = 6f;

    private Rigidbody rb;
    private Animator animator;

    private void Awake()
    {
        // 获取玩家的刚体组件和动画组件
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        if (rb == null)
        {
            Debug.LogError("PlayerMovement脚本需要附加在一个带有Rigidbody组件的GameObject上。");
        }
    }

    private void FixedUpdate()
    {
        // 获取输入
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // 按住Shift键加速移动
        moveSpeed = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) ? 10f : 6f;

        // 计算移动向量
        Vector3 movement = new Vector3(horizontal, 0f, vertical).normalized * moveSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);

        // 移动、旋转、动画
        Move(horizontal, vertical);
        Turning();
        Animating(horizontal, vertical);

        // 右键按住相机缩小一点视野，右键松开相机恢复
        float targetFOV = Input.GetMouseButton(1) ? 60f : 70f;
        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, targetFOV, Time.deltaTime * 5f);
    }

    /// <summary>
    /// 玩家移动
    /// </summary>
    private void Move(float horizontal, float vertical)
    {
        Vector3 movement = new Vector3(horizontal, 0f, vertical).normalized * moveSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
    }

    /// <summary>
    /// 玩家朝向鼠标
    /// </summary>
    private void Turning()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        int floorMask = LayerMask.GetMask("GroundForMouse");
        if (Physics.Raycast(cameraRay, out RaycastHit floorHit, 100f, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f; // 只在水平面旋转
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            rb.MoveRotation(newRotation);
        }
        else
        {
            Debug.LogWarning("PlayerMovement脚本在Turning方法中未检测到与地面的交点。请确保地面层已正确设置，并且摄像机射线可以与地面相交。");
        }
    }

    /// <summary>
    /// 切换动画
    /// </summary>
    private void Animating(float horizontal, float vertical)
    {
        bool isWalking = horizontal != 0f || vertical != 0f;
        animator.SetBool("IsWalking", isWalking);
    }
}

// 注意：此代码片段假设你已经在Unity中设置了一个名为"GroundForMouse"的层，并且摄像机可以正确地射线检测到地面。

