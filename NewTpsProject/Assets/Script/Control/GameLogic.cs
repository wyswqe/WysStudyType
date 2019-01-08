using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script
{
    public class GameLogic : MonoBehaviour
    {
        public static GameLogic Instance = null;
        private bool isInit = false;

        public void Awake()
        {
            Application.targetFrameRate = 60;

            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
                Init();
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        //初始化
        public void Init()
        {
            isInit = true;

        }

        //初始化组件
        public void InitComponent()
        {

        }

        //初始化监听
        public void InitListener()
        {

        }

        public void InitData()
        {

        }

        //移除监听
        public void RemoveListener()
        {

        }

        public void Update()
        {

        }

        public void FixedUpdate()
        {

        }

        public void OnDestroy()
        {
            if (isInit == false)
            {
                return;
            }
            RemoveListener();
        }
    }
}
