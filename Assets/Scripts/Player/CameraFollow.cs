using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject Player; // ��Ҷ���
    public Vector3 offset; // ����������֮���ƫ����
    public float Smooting = 10f; // ƽ��������ٶ�
    private void Awake()
    {
        if (Player == null)
        {
            Debug.LogError("CameraFollow�ű���Ҫ����Player����");
        }
        Player = GameObject.FindGameObjectsWithTag("Player")[0];
    }
    private void Start()
    {
        if (Player == null)
        {
            Debug.LogError("CameraFollow�ű���Start������δ�ҵ�Player������ȷ��Player��������ȷ���á�");
        }
        // �����������λ��Ϊ���λ�ü���һ��ƫ����
        offset = transform.position - Player.transform.position;

    }
    private void FixedUpdate()
    {
        if (Player != null)
        {
            // �����������λ�ã�ʹ��������
            Vector3 newPosition = Player.transform.position + offset;
            transform.position = newPosition;
        }
        else
        {
            Debug.LogWarning("CameraFollow�ű���FixedUpdate������δ�ҵ�Player������ȷ��Player��������ȷ���á�");
        }
        transform.position = Vector3.Lerp(transform.position, Player.transform.position + offset, Smooting*Time.deltaTime); // ƽ��������� ��������ͣ�������������ǰ�ƶ�һ�ξ���

    }
}
