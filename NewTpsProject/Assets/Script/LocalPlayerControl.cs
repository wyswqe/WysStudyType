using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class LocalPlayerControl : MonoBehaviour
{
    public LocalPlayerCharacter m_Charachter;
    private Transform m_Cam;//获取主摄像机位置
    private Vector3 m_CamForward;//当前摄像机的正前方
    private Vector3 m_Move;//根据相机的正前方和用户的输入，计算世界坐标相关的移动方向
    private bool m_Jump;//跳
    private bool m_crouch;//蹲

    private bool isFire;

    public float moveSpeed = 1;
    // Use this for initialization
    void Start()
    {
        //获取主相机
        m_Cam = Camera.main.transform;
        m_Cam.transform.position = new Vector3(transform.position.x, transform.position.y + 25, transform.position.z - 8);
        m_Cam.transform.rotation = Quaternion.Euler(70, 0, 0);

        //获取第三人称移动脚本
        //m_Charachter = GetComponent<LocalPlayerCharacter>();
    }

    private void FixedUpdate()
    {
        Vector3 v3 = m_Cam.transform.position;
        v3.x = transform.position.x;
        v3.z = transform.position.z - 8;
        m_Cam.transform.position = v3;
    }

    public void RoleMove(Vector2 v2)
    {
        m_Move = v2;
        isFire = true;
        if (!isFire)
        {
            m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
            m_Move = v2.y * m_CamForward + v2.x * m_Cam.right;
        }
        if (m_crouch)
        {
            m_Move *= 0.5f;
        }
        m_Charachter.MoveMent(m_Move, m_crouch, m_Jump, isFire);
        m_Jump = false;
    }

    public void MoveEnd()
    {
        RoleMove(Vector3.zero);
    }

    public void AttackTarget(Vector2 v2)
    {
        isFire = true;
    }

    public void AttackEnd()
    {
        isFire = false;
    }
}
