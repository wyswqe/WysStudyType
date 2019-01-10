using UnityEngine;


public class Bullet : MonoBehaviour
{
    public int id;
    private bool s_Flay = false;
    private float moveSpeed = 1.0f;

    private void Update()
    {
        if (s_Flay)
        {
            transform.Translate(transform.forward * moveSpeed * Time.deltaTime);
        }
    }


    public void BuilletStartPosition(Vector3 s_Position, Vector3 s_Rotation, float speed)
    {
        transform.position = s_Position;
        transform.rotation = Quaternion.Euler(s_Rotation);
        this.moveSpeed = speed;
        s_Flay = true;
    }

    public void OnCollisionEnter(Collision col)
    {
        //击中物体
        GameObject target = col.gameObject;
        RoleMgr role = target.GetComponent<RoleMgr>();
        //发送服务器受伤状况

    }

    public void OnDisable()
    {
        s_Flay = false;
    }

}

