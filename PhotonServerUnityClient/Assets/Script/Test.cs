using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    SendReqeust();
        //}
    }

    void SendReqeust()
    {
        Dictionary<byte, object> data = new Dictionary<byte, object>();
        data.Add(1, "100");
        data.Add(2, "AWDASWDAS");
        PhotoEngine.Peer.OpCustom(1, data, true);
    }
}
