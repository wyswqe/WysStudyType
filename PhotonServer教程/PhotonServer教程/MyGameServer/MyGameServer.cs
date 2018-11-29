using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using System.IO;
using log4net.Config;
using MyGameServer.Manager;
using Common;
using MyGameServer.Handler;

namespace MyGameServer
{
    //所有的server端 主类都要继承自ApplicationBase
    public class MyGameServer : ApplicationBase
    {
        public static readonly ILogger log = LogManager.GetCurrentClassLogger();

        public static MyGameServer Instance { get; private set; }

        public Dictionary<OperationCode, BaseHandler> HandlerDict = new Dictionary<OperationCode, BaseHandler>();

        //当一个客户端请求链接的时候
        //我们使用peerbase,表示和一个客户端的链接
        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            log.Info("一个客户端连接过来了。。。。");
            return new ClientPeer(initRequest);
        }

        //初始化
        protected override void Setup()
        {
            Instance = this;
            // 日志的初始化
            log4net.GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(
                                                                    Path.Combine(this.ApplicationRootPath, "bin_Win64"), "log");
            FileInfo configFileInfo = new FileInfo(Path.Combine(this.BinaryPath, "log4net.config"));
            if (configFileInfo.Exists)
            {
                LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);//让photo 知道用的是哪个插件
                XmlConfigurator.ConfigureAndWatch(configFileInfo);//让log4net这个插件读取配置文件
            }

            log.Info("服务器初始化完成");

            InitHandler();
        }
        public void InitHandler()
        {
            LoginHandler loginHandler = new LoginHandler();
            HandlerDict.Add(loginHandler.OpCode, loginHandler);
            DefaultHandler defaultHandle = new DefaultHandler();
            HandlerDict.Add(defaultHandle.OpCode, defaultHandle);
        }

        //server端关闭的时候
        protected override void TearDown()
        {
            log.Info("服务器端应用关闭了");
        }
    }
}
