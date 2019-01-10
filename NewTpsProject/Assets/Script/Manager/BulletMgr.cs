/*子弹管理器
 * 从服务器获取所有子弹信息
 * 分配显示的子弹信息
*/
using System;
using System.Collections.Generic;
using UnityEngine;

public class BulletMgr : TSingleton<BulletMgr>
{
    public List<Bullet> allBullet = new List<Bullet>();

    public override void Init()
    {
        base.Init();
    }

    public void UpdateInfo(List<Bullet> s_bullet)
    {
        for (int i = 0; i < s_bullet.Count; i++)
        {

        }
    }

    public void DesSomeBullet(int iD, int demage)
    {

    }
}

