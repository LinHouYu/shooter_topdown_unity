using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //玩家移动速度
    public float moveSpeed = 6f;
    private Rigidbody rb;
    private Animator animator;

    private void Awake()
    {
        //获取玩家的刚体组件
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        if (rb == null)
        {
            Debug.LogError("PlayerMovement脚本需要附加在一个带有Rigidbody组件的GameObject上。");
        }
    }
    private void FixedUpdate()
    {
        //让玩家可以通过键盘的方向键或WASD键来移动
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        //按住shift键加速移动
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            moveSpeed = 10f; // 按住Shift键时加速
        }
        else
        {
            moveSpeed = 6f; // 恢复正常速度
        }
        // 通过摄像机的方向来计算移动方向 这里设置了摄像机的y轴为0，避免玩家在y轴上移动
        Vector3 movement = new Vector3(horizontal, 0f, vertical);
        movement = movement.normalized * moveSpeed * Time.deltaTime;

        rb.MovePosition(transform.position + movement);

        //移动
        Move(horizontal, vertical);

        //旋转
        Turning();

        //切换动画
        Animating(horizontal, vertical);


        //右键按住相机缩小一点视野，右键松开相机恢复
        if (Input.GetMouseButton(1))
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60f, Time.deltaTime * 5f);
        }
        else
        {
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 70f, Time.deltaTime * 5f);
        }



        void Move(float horizontal, float vertical)
        {
            Vector3 movement = new Vector3(horizontal, 0f, vertical);
            movement = movement.normalized * moveSpeed * Time.deltaTime;

            rb.MovePosition(transform.position + movement);
        }

        void Turning()
        {
            //通关相机获取鼠标位置
            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit floorHit;
            int floorMask = LayerMask.GetMask("GroundForMouse");
            //检测射线是否与地面相交
            bool isThouchFloor = Physics.Raycast(cameraRay, out floorHit, 100f, floorMask);
            if (isThouchFloor)
            {
                Vector3 playerToMouse = floorHit.point - transform.position;
                playerToMouse.y = 0f; // 确保只在水平面上旋转

                Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
                rb.MoveRotation(newRotation);
            }
            else
            {
                Debug.LogWarning("PlayerMovement脚本在Turning方法中未检测到与地面的交点。请确保地面层已正确设置，并且摄像机射线可以与地面相交。");

            }

        }

        void Animating(float horizontal, float vertical) 
        {
            //根据玩家的移动方向来切换动画IsWalking参数
            bool isWalking = horizontal != 0f || vertical != 0f;
            animator.SetBool("IsWalking", isWalking);
            ////如果玩家正在移动，则播放行走动画
            //if (isWalking)
            //{
            //    animator.SetFloat("Horizontal", horizontal);
            //    animator.SetFloat("Vertical", vertical);
            //}
            //else
            //{
            //    animator.SetFloat("Horizontal", 0f);
            //    animator.SetFloat("Vertical", 0f);
            //}

        }
    }
}

// 注意：此代码片段假设你已经在Unity中设置了一个名为"GroundForMouse"的层，并且摄像机可以正确地射线检测到地面。

