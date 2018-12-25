using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Animator Anim;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayerMove(Vector2 ts)
    {
        float absX = Mathf.Abs(ts.x);
        float absY = Mathf.Abs(ts.y);

        //判断移动
        bool isRun = Mathf.Abs(ts.x) > .8f ? true : false;
        Anim.SetBool("Walk", !isRun);
        Anim.SetBool("Run", isRun);

        //判断上跳 下蹲


        transform.localScale = ts.x > .1f ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
    }
    public void MoveEnd()
    {
        Anim.SetBool("Walk", false);
        Anim.SetBool("Run", false);
        Anim.SetBool("Idle", true);
    }

    public void PlayerShot(Vector2 ts)
    {

    }

    public void Reloading()
    {

    }

    public void Crouch()
    {

    }

    public void Jump()
    {

    }

    public void Dead()
    {

    }
}
