using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]
public class LocalPlayerCharacter : MonoBehaviour
{
    float m_GroundCheckDistance = 0.1f;

    Animator m_Animator;
    Vector3 m_GroundNormal;//地面法向量
    Rigidbody m_Rigidbody;//刚体
    bool m_IsGrounded;
    float m_TurnAmount;
    float m_ForwardAmount;
    float m_OrigGroundCheckDistance;//地面距离检测的起始值
    CapsuleCollider m_Capsule;//胶囊体

    Vector2 m_FireAngle; //射击身体和头旋转的角度 X头 Y身体

    private void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Capsule = GetComponent<CapsuleCollider>();
        m_Rigidbody = GetComponent<Rigidbody>();

        //锁定刚体的 XYZ轴的旋转
        m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        m_OrigGroundCheckDistance = m_GroundCheckDistance;//保存一下地面检查值

        HandWeapons(WeaponsType.AutoRifle);
    }

    public void MoveMent(Vector3 move, bool isRoll, bool isFire)
    {
        if (move.magnitude > 1) move.Normalize();//向量大于1,则变为单位向量

        if (!isFire)
        {
            float dis = (Vector3.zero - move).sqrMagnitude;
            dis = dis > .8f ? 1 : dis;
            move = new Vector3(0, 0, dis);
        }
        else
        {
            move = transform.right * move.x + transform.forward * move.z;
        }

        UpdateAnim(move);//动画播放

        //if (isFire)
        //{
        //    FireMove(move);
        //}
        //else
        //{
        //    Move(move);
        //}


    }


    /// <summary>
    /// 瞄准旋转
    /// </summary>
    public void FireRotate(bool isFire = true)
    {
        m_Animator.SetBool("Shoot_b", isFire);
        m_Animator.SetFloat("Head_Horizontal_f", m_FireAngle.x);
        m_Animator.SetFloat("Body_Horizontal_f", m_FireAngle.y);
    }

    public void FireEnd(bool isReload = false)
    {
        m_Animator.SetBool("Shoot_b", false);
        m_Animator.SetBool("Reload_b", isReload);
        m_Animator.SetFloat("Head_Horizontal_f", 0);
        m_Animator.SetFloat("Body_Horizontal_f", 0);
    }

    public void HandWeapons(WeaponsType type)
    {
        m_Animator.SetInteger("WeaponType_int", (int)type);
        m_FireAngle = new Vector2(-0.8f, 0.6f);
    }

    public void UpdateAnim(Vector3 move)
    {
        m_Animator.SetFloat("MoveX", move.x);
        m_Animator.SetFloat("MoveY", move.z);
    }
}

