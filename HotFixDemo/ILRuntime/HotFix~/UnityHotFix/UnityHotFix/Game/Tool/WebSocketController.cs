using System;
using BestHTTP.WebSocket;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityHotFix.Properties;

namespace UnityHotFix.Game.Tool
{
    /// <summary>
    /// BestHttp插件 WebSocket功能封装
    /// </summary>
    public class WebSocketController : BaseSingleton<WebSocketController>
    {
        public Action StartSeverEvent;
        public Action OnOpenEvent;
        public Action<string> OnMessageEvent;
        public Action OnErrorEvent;
        public Action OnClosedEvent;
        private WebSocket webSocket;
        public WebSocket WebSocket=>webSocket;
        public string url = "";
        public bool isReconnection;

        public void Init(string _url)
        {
            this.url = _url;
            webSocket = new WebSocket(new Uri(url));
            webSocket.OnOpen = OnOpen;
            webSocket.OnMessage = OnMessageReceived;
            webSocket.OnError = OnError;
            webSocket.OnClosed = OnClosed;
        }

        public void StartSever()
        {
            webSocket.Open();
            StartSeverEvent?.Invoke();
        }

        public void Close() => webSocket?.Close();

        private void OnOpen(WebSocket websocket)
        {
            try
            {
                OnOpenEvent?.Invoke(); 
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        private void OnClosed(WebSocket websocket, ushort code, string message)
        {
            Debug.Log(message);
            OnClosedEvent?.Invoke();
            websocket = null;
        }

        private int errorCount = 0;

        private async void OnError(WebSocket websocket, string reason)
        {
            try
            {
                if (!isReconnection)
                {
                    return;
                }

                string errorMsg = string.Empty;
                if (websocket.InternalRequest.Response != null)
                    errorMsg = string.Format("Status Code from Server: {0} and Message: {1}", websocket.InternalRequest.Response.StatusCode,
                        websocket.InternalRequest.Response.Message);
                errorCount++;
                OnErrorEvent?.Invoke();
                Debug.Log(errorMsg);
                if (errorCount < 3)
                {
                    Debug.Log($"第{errorCount}次重连");
                    await UniTask.Delay(TimeSpan.FromSeconds(1f));
                    Init(url);
                    StartSever();
                }
                else
                {
                    Debug.Log("已重连3次，停止重连");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void OnMessageReceived(WebSocket websocket, string message)
        {
            // Debug.Log($"msg:{message}");
            OnMessageEvent?.Invoke(message);
        }

        public void Destroy()
        {
            if (webSocket != null)
            {
                webSocket.OnOpen = null;
                webSocket.OnMessage = null;
                webSocket.OnError = null;
                webSocket.OnClosed = null;
            }

            webSocket = null;
        }
    }
}