using UnityEngine;
using UnityEngine.Networking;
public class CustomMessage : MessageBase {
    public int messageId;
    public string content;
    public Vector3 vector;
    public byte[] bytes;
    /// <summary>
    /// 序列化
    /// </summary>
    /// <param name="writer"></param>
    public override void Serialize (NetworkWriter writer) {
        writer.Write (messageId);
        writer.Write (content);
        writer.Write (vector);
        writer.WriteBytesAndSize (bytes, bytes.Length);
    }
    /// <summary>
    /// 反序列化
    /// </summary>
    /// <param name="reader"></param>
    public override void Deserialize (NetworkReader reader) {
        messageId = reader.ReadInt32 ();
        content = reader.ReadString ();
        vector = reader.ReadVector3 ();
        ushort count = reader.ReadUInt16 ();
        bytes = reader.ReadBytes (count);
    }
}