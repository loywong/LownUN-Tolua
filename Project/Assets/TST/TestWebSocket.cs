using System;
using UnityEngine;
using WebSocketSharp;

public class TestWebSocket : MonoBehaviour {
    private const string APIDomainWS = "wss://fury.furious.no";
    private const string APIDomain = "fury.furious.no";
    private const string APIUrl = "https://" + APIDomain;

    // public static void Main (string[] args) {
    void Start () {
        // var buffer = new ArrayBuffer (TOTAL_MEMORY);
        // var HEAPU8 = new Uint8Array (buffer);
        // // >> Uint8Array[268435456]
        // // HEAPU8[sharedArray.byteOffset]
        // // >> 5
        // // total = 0;for (i=0; i< 1024; i++){total+=HEAPU8[sharedArray.byteOffset+i];}
        // // >> 374

        string strByteArray = "ffff";
        byte[] myByteArray = Convert.FromBase64String (strByteArray);
        for (int i = 0; i < myByteArray.Length; i++) {
            Debug.Log (i);
        }

        using (var ws = new WebSocket (APIDomainWS)) {
            // 确定建立连接
            ws.OnOpen += (sender, e) => {
                UnityEngine.Debug.Log ("OnOpen");
                ws.Send ("BALUS");
                // Console.ReadKey (true);
                UnityEngine.Debug.Log ("Send true");
            };

            //收消息
            ws.OnMessage += (sender, e) =>
                UnityEngine.Debug.Log ("OnMessage: " + e.Data);

            ws.OnError += (sender, e) =>
                UnityEngine.Debug.LogError ("WebSocket OnError");

            ws.OnClose += (sender, e) =>
                UnityEngine.Debug.LogWarning ("WebSocket OnClose");

            ws.Connect ();
        }
    }
}

// using System;
// using System.Collections;
// using System.Net;
// using System.Net.WebSockets;
// using System.Security.Cryptography.X509Certificates;
// using System.Text;
// using System.Threading;
// using UnityEngine;
// using UnityEngine.Networking;

// public class TestWebSocket : MonoBehaviour {
//     public const string APIDomainWS = "wss://fury.furious.no";
//     public const string APIDomain = "fury.furious.no";
//     public const string APIUrl = "https://" + APIDomain;

//     public static string SessionToken;

//     public ClientWebSocket clientWebSocket;

//     async void Start () {
//         DontDestroyOnLoad (gameObject);

//         clientWebSocket = new ClientWebSocket ();
//         clientWebSocket.Options.AddSubProtocol ("Tls");
//         Debug.Log ("[WS]:Attempting connection.");
//         // try {
//         Uri uri = new Uri (APIDomainWS);
//         await clientWebSocket.ConnectAsync (uri, CancellationToken.None);

//         if (clientWebSocket.State == WebSocketState.Open) {
//             Debug.Log ("Input message ('exit' to exit): ");

//             ArraySegment<byte> bytesToSend = new ArraySegment<byte> (
//                 Encoding.UTF8.GetBytes ("hello fury from unity")
//             );
//             await clientWebSocket.SendAsync (
//                 bytesToSend,
//                 WebSocketMessageType.Text,
//                 true,
//                 CancellationToken.None
//             );

//             // ArraySegment<byte> bytesReceived = new ArraySegment<byte> (new byte[1024]);
//             // WebSocketReceiveResult result = await clientWebSocket.ReceiveAsync (
//             //     bytesReceived,
//             //     CancellationToken.None
//             // );
//             // Debug.Log (Encoding.UTF8.GetString (bytesReceived.Array, 0, result.Count));
//         }
//         Debug.Log ("[WS][connect]:" + "Connected");
//         // } catch (Exception e) {
//         //     Debug.Log ("[WS][exception]:" + e.Message);
//         //     if (e.InnerException != null) {
//         //         Debug.Log ("[WS][inner exception]:" + e.InnerException.Message);
//         //     }
//         // }

//         Debug.Log ("End");
//     }
// }