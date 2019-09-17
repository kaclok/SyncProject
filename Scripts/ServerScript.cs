using UnityEngine;
using UnityEngine.Networking;
public class ServerScript : MonoBehaviour {
    private void Start () {
        startServer ();
    }
    public bool startServer () {
        // 注册消息回调,当Server收到指定类型的消息时,会回调注册好的方法
        NetworkServer.RegisterHandler (MsgType.Connect, OnClientConnected);
        NetworkServer.RegisterHandler (MsgType.Disconnect, OnClientDisConnected);
        NetworkServer.RegisterHandler (MsgType.Error, OnServerError);

        NetworkServer.RegisterHandler (CustomMsgType.InGameMsg, OnRecvCustomMsgHandler);
        // 监听20086端口
        bool succeed = NetworkServer.Listen (20086);
        if (succeed) {
            Debug.LogWarning ("服务器成功启动!");
            CEventDispatcherObj.cEventDispatcher.dispatchEvent (new CEvent (EventName.NET_SERVER_COMPLETE), this);
        } else
            Debug.LogErrorFormat ("服务器无法启动,端口为{0}", 20086);
        return succeed;
    }

    protected void OnRecvCustomMsgHandler (NetworkMessage netMsg) {
        Debug.LogWarning ("收到消息----------------------!");
        CustomMessage msg = new CustomMessage ();
        msg.Deserialize (netMsg.reader);
        Debug.LogWarning ("OnRecvCustomMsg");
        Debug.LogWarning ("messageId:" + msg.messageId);
        Debug.LogWarning ("content:" + msg.content);
        Debug.LogWarning ("vector:" + msg.vector);
        Debug.LogWarning ("bytesLength:" + msg.bytes.Length);
        // NetworkServer.connections.Add(netMsg.conn);
    }

    private void OnClientConnected (NetworkMessage netMsg) {
        Debug.LogWarning ("OnClientConnected");
    }
    private void OnClientDisConnected (NetworkMessage netMsg) {
        Debug.LogWarning ("OnClientDisConnected");

    }
    private void OnServerError (NetworkMessage netMsg) {
        Debug.LogWarning ("OnClientDisConnected");

    }
}