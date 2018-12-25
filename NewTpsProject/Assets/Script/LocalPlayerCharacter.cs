using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalPlayerCharacter : MonoBehaviour
{
    [SerializeField] float m_MovingTurnSpeed = 360;
    [SerializeField] float m_StationaryTurnSpeed = 180;
    [SerializeField] float m_GroundCheckDistance = 0.1f;

    Animator m_Animator;
    Vector3 m_GroundNormal;//地面法向量
    bool m_IsGrounded;
    float m_TurnAmount;
    float m_ForwardAmount;

    private void Start()
    {
        m_Animator = GetComponent<Animator>();

    }

    public void MoveMent(Vector3 move, bool crouch, bool jump, bool isFire)
    {
        if (move.magnitude > 1) move.Normalize();//向量大于1,则变为单位向量
        if (isFire)
        {
            FireMove(move);
        }
        else
        {
            Move(move);
        }



    }

    /// <summary>
    /// 瞄准旋转
    /// </summary>
    public void FireRotate(Vector3 rt)
    {

    }

    /// <summary>
    /// 锁定身体移动
    /// </summary>
    /// <param name="move"></param>
    public void FireMove(Vector3 move)
    {
        m_Animator.SetFloat("MoveY", m_ForwardAmount);
        m_Animator.SetFloat("MoveX", m_TurnAmount);
    }

    /// <summary>
    /// 带动身体移动
    /// </summary>
    /// <param name="move"></param>
    public void Move(Vector3 move)
    {
        if (move.magnitude > 1f) move.Normalize(); //向量大于1，则变为单位向量

        move = transform.InverseTransformDirection(move);//将世界坐标系的方向和位置转换为自身坐标系

        CheckGroundStatus();//检查是否在地面上 顺便获取地面的法向量;
        move = Vector3.ProjectOnPlane(move, m_GroundNormal);//根据地面的法向量,获得一个对应平面的速度
        Debug.Log("Move:" + move);


        //[Mathf.Atan2] :以弧度为单位计算并返回 y/x 的反正切值。返回值表示相对直角三角形对角的角，其中 x 是临边边长，而 y 是对边边长。
        //返回值为x轴和一个零点起始在(x,y)结束的2D向量的之间夹角。
        m_TurnAmount = Mathf.Atan2(move.x, move.z);//产生一个方位角，即与z轴的夹角，用于人物转向 
        Debug.Log("转向" + m_TurnAmount);
        //transform.rotation = Quaternion.Euler(move);//转向 
        m_ForwardAmount = move.z;//人物前进的数值

        //应用旋转
        ApplyExtraTurnRotation();

        UpdateAnim(move);
    }


    /// <summary>
    /// 帮助角色快速转向，这是动画中根旋转的附加项  
    /// </summary>
    void ApplyExtraTurnRotation()
    {
        //帮助这个角色将更快(这是除了根旋转的动画)
        // 帮助角色快速转向，这是动画中根旋转的附加项  
        float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
        transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);//转向.  
    }

    public void CheckGroundStatus()
    {
        RaycastHit hitInfo;
#if UNITY_EDITOR
        Debug.DrawLine(transform.position + (Vector3.up * 1.0f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * m_GroundCheckDistance));
#endif
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
        {
            m_GroundNormal = hitInfo.normal;//将射线触碰到的物体的法向量赋值给m_GroundNormal
            m_IsGrounded = true;//在地面上;
        }
        else
        {
            m_IsGrounded = false;//不在地面上
            m_GroundNormal = Vector3.up;
        }
    }

    public void UpdateAnim(Vector3 move)
    {
        Debug.Log(m_ForwardAmount + "    " + m_TurnAmount);
        m_Animator.SetFloat("MoveY", m_ForwardAmount);
        m_Animator.SetFloat("MoveX", m_TurnAmount);

    }
}

