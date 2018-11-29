using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.Tools;
using MyGameServer.Handler;
using Photon.SocketServer;
using PhotonHostRuntimeInterfaces;

namespace MyGameServer
{
    public class ClientPeer : Photon.SocketServer.ClientPeer
    {
        public float x, y, z;
        public string username;

        public ClientPeer(InitRequest initRequest) : base(initRequest)
        {

        }

        //处理客户端断开连接的后续工作
        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            MyGameServer.Instance.peerList.Remove(this);
        }

        //处理客户端的请求
        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            BaseHandler handler = DictTool.GetValue<OperationCode, BaseHandler>(MyGameServer.Instance.HandlerDict, (OperationCode)operationRequest.OperationCode);
            if (handler != null)
            {
                handler.OnOperationRequest(operationRequest, sendParameters, this);
                MyGameServer.log.Info("收到了一个请求");
            }
            else
            {
                BaseHandler defaultHandler = DictTool.GetValue<OperationCode, BaseHandler>(MyGameServer.Instance.HandlerDict, OperationCode.Default);
                defaultHandler.OnOperationRequest(operationRequest, sendParameters, this);
            }


            //switch (operationRequest.OperationCode)//通过OpCode区分请求
            //{
            //    case 1:
            //        MyGameServer.log.Info("收到了一个客户端的请求");
            //        Dictionary<byte, object> data = operationRequest.Parameters;
            //        object intValue;
            //        data.TryGetValue(1, out intValue);
            //        object stringValue;
            //        data.TryGetValue(2, out stringValue);
            //        MyGameServer.log.Info("得到的参数是" + intValue + stringValue);
            //        OperationResponse opResponse = new OperationResponse(1);
            //        Dictionary<byte, object> data2 = new Dictionary<byte, object>();
            //        data2.Add(1, 100);
            //        data2.Add(2, "Error啊我等哈数据库老大");
            //        opResponse.SetParameters(data2);
            //        SendOperationResponse(opResponse, sendParameters);//给客户端一个响应
            //        EventData ed = new EventData(1); ed.Parameters = data2;
            //        SendEvent(ed, new SendParameters());
            //        break;
            //    case 2:
            //        break;
            //    default:
            //        break;
            //}
        }
    }
}
