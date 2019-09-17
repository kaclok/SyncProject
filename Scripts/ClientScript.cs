using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ClientScript : MonoBehaviour {

    NetworkClient client;
    //NetworkConnection connection;
    void Start () {
        CEventDispatcherObj.cEventDispatcher.addEventListener (EventName.NET_SERVER_COMPLETE, startClient);
    }

    void startClient (CEvent e) {
        // 创建NetworkClient对象
        client = new NetworkClient ();

        // 注册消息回调,当Client收到指定类型的消息时,会回调注册好的方法
        client.RegisterHandler (MsgType.Connect, OnConnectedServer);
        client.RegisterHandler (MsgType.Disconnect, OnDisConnectedServer);
        client.RegisterHandler (MsgType.Error, OnClientError);

        // 连接服务器
        client.Connect ("127.0.0.1", 20086); // 服务器IP 服务器端口
        //connection = client.connection;
        //sendCustomMsg ();
    }

    private void sendCustomMsg () {
        CustomMessage message = new CustomMessage ();
        message.messageId = Time.frameCount;
        message.content = "I am Client!";
        message.vector = new Vector3 (0, 5, 0);
        message.bytes = new byte[100];
        Debug.LogWarning ("客户端发送消息");
        client.Send (CustomMsgType.InGameMsg, message); // 使用Unity的NetworkConnection来发送消息

    }

    private void OnConnectedServer (NetworkMessage netMsg) {
        Debug.LogWarning ("OnConnectedServer");

        sendCustomMsg ();

    }
    private void OnDisConnectedServer (NetworkMessage netMsg) {
        Debug.LogWarning ("OnDisConnectedServer");

    }
    private void OnClientError (NetworkMessage netMsg) {
        Debug.LogWarning ("OnClientError");
    }

    private void FixedUpdate () {

    }

    // public void Simulate () {
    //     OnSimulateBefore ();

    //     if (isServer && isOwner) //如果是服务器的物体,获取指令,直接执行指令即可
    //     {
    //         Command cmd = new Command ();
    //         cmd.input = CollectCommandInput (); // 获取指令
    //         ExecuteCommand (cmd); // 执行指令
    //     }

    //     OnSimulateAfter ();
    // }

    // public override void ExecuteCommand (Command command) {
    //     float movingSpeed = 4;
    //     Vector3 movingDir = Vector3.zero;

    //     if (input.forward ^ input.backward)
    //         movingDir.z = input.forward ? +1 : -1;

    //     if (input.left ^ input.right)
    //         movingDir.x = input.right ? +1 : -1;

    //     Vector3 velocity = movingDir * movingSpeed; //通过输入计算出速度

    //     transform.position = transform.position + velocity * Time.fixedDeltaTime; //立即计算出结果

    //     command.result.position = transform.position; //将结果保存到CommandResult中

    // }
}