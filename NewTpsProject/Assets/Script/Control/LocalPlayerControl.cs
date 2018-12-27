using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(LocalPlayerCharacter))]
public class LocalPlayerControl : MonoBehaviour
{
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;

    private LocalPlayerCharacter m_Charachter;
    private Transform m_Cam;//获取主摄像机位置
    private Vector3 m_CamForward;//当前摄像机的正前方
    private Vector3 m_Move;//根据相机的正前方和用户的输入，计算世界坐标相关的移动方向
    private bool m_Roll;//翻滚
    private float m_RollStartTime;//翻滚开始时间


    private bool m_isFire;

    public float moveSpeed = 1;
    public float crouchSpeed = 0.5f;
    public float jumpSpeed = 0.5f;
    public float m_RollTime = 0.3f;//翻滚时间
    public float m_RollWhat = 0.5f;//翻滚冷却

    // Use this for initialization
    void Start()
    {
        //获取主相机
        m_Cam = Camera.main.transform;
        m_Cam.transform.position = new Vector3(transform.position.x, transform.position.y + 25, transform.position.z - 8);
        m_Cam.transform.rotation = Quaternion.Euler(70, 0, 0);

        //获取第三人称移动脚本
        m_Charachter = GetComponent<LocalPlayerCharacter>();

    }
    void Update()
    {
        if (m_Roll)
        {
            transform.Translate(transform.forward * moveSpeed * 2 * Time.deltaTime, Space.World);
            if (Time.time > m_RollStartTime + m_RollTime)
            {
                m_Roll = false;
            }
        }
    }

    public void RoleMove(Vector2 v2)
    {

        //move = transform.right * move.x + transform.forward * move.z;

        if (m_Roll) return;

        m_Move = WeathCamera(v2);

        if (!m_isFire)
        {
            RotatePoint(m_Move);
        }
        else m_Move *= 0.5f;
        //m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
        //m_Move = v2.y * m_CamForward + v2.x * m_Cam.right;
        m_Charachter.MoveMent(m_Move, m_Roll, m_isFire);
        MovePoint(m_Move);
        m_Roll = false;
    }

    public void MoveEnd()
    {
        m_Charachter.MoveMent(Vector3.zero, m_Roll, m_isFire);
    }

    public void AttackTarget(Vector2 v2)
    {
        if (m_Roll) return;

        Vector3 rt = WeathCamera(v2);

        m_isFire = true;
        RotatePoint(rt);
        m_Charachter.FireRotate();
    }

    public void AttackEnd()
    {
        m_isFire = false;
        m_Charachter.FireEnd();
    }

    public void RotatePoint(Vector3 rt)
    {
        transform.LookAt(new Vector3(transform.position.x + rt.x, transform.position.y, transform.position.z + rt.z));
    }

    public void MovePoint(Vector3 move)
    {
        //Debug.Log(v2);
        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);
    }

    private Vector3 WeathCamera(Vector2 v2)
    {
        Vector3 camForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
        return v2.x * m_Cam.right + v2.y * camForward;
    }

    public void Crouch()
    {
        Debug.Log("翻滚");
        if (m_Roll) return;

        m_Roll = true;
        m_RollStartTime = Time.time;
    }
}
