/****************************************************************
 * File			: Assets\CORE\Module\Net\NetManager.cs
 * Author		: www.loywong.com
 * Company		: ??? Co.,Ltd
 * COPYRIGHT	: (C)
 * Date			: 2019/08/01
 * Description	: 网络模块的封装主类 
                Sample: NetManager.Instance.Request(code, request, response)
 * Version		: 1.0
 * Maintain		: //[date] desc
 ****************************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetManager : ManagerBase<NetManager> {


    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    // /// <summary>
    // /// 发送网络消息，数据包含两部分，一部分是头部信息，一部分是实际数据部分
    // /// 
    // /// 头部信息，比如：
    // /// type TCPHeader struct {
    // /// DateStart uint8 //数据类型        
    // /// CheckCode uint8 //校验字段        
    // /// PackSize uint8 //数据大小        
    // /// MainCmdId uint8 //主协议码        
    // /// SubCmdId uint8 //子协议码        
    // /// PackerVer uint8 //封包版本号        
    // /// }    
    // /// </summary>
    // /// <param name="mainid"></param>
    // /// <param name="subid"></param>
    // /// <param name="msg">protobuf或者json序列化之后得到的字符串</param>
    // public void SerdMessage (int mainid, int subid, string msg) {
    //     var buffer_msg = new ByteBuffer();
    //     buffer_msg.WriteString(msg2);
    //     int validDataLen = buffer_msg.ToBytes().Length;
    //     buffer_msg.Close();

    //     var buffer = new ByteBuffer();
    //     // 1 头部结构
    //     buffer.WriteByte(0);
    //     buffer.WriteByte(1);
    //     buffer.WriteShort(Convert.ToUInt16(1));
    //     buffer.WriteShort(Convert.ToUInt16(2));
    //     buffer.WriteShort(Convert.ToUInt16(3));
    //     buffer.WriteShort(0);
    //     // 2 有效数据
    //     buffer.WriteString(msg);

    //     byte[] bf = buffer.ToBytes();
    //     buffer.Close();

    //     // Log.Green();
    //     // WebSocketSharp
    //     WebSocket.Instance.OnSend(bf);
    // }
}