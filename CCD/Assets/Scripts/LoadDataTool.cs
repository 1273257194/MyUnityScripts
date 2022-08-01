using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Slotpart.Tools
{
    public class LoadDataTool : BaseSingleton<LoadDataTool>
    {
        public   async UniTask<string> GetTextAsync(UnityWebRequest req)
        {
            var op = await req.SendWebRequest();
            return op.downloadHandler.text;
        }

        /// <summary>
        /// 得到本地路径下的文本数据 在安卓不可以用system.io 需要用unityWebRequest
        /// </summary>
        /// <param name="path">数据路径</param>
        /// <returns>文本数据</returns>
        public string GetTextData(string path)
        {
            var task = GetTextAsync(UnityWebRequest.Get(path));
            var data = task.GetAwaiter().GetResult();
            return data;
        }

        async UniTask<Texture2D> GetTextureAsync(UnityWebRequest req)
        {
            req.downloadHandler = new DownloadHandlerTexture();
            var op =   await req.SendWebRequest();
            return ((DownloadHandlerTexture) op?.downloadHandler)?.texture;
        }

        public Texture2D GetTexture2D(string path)
        {
            var task = GetTextureAsync(UnityWebRequest.Get(path));
            return task.GetAwaiter().GetResult();
        }
        
        public  async UniTask<byte[]> GetByteAsync(UnityWebRequest req)
        {
            var op = await req.SendWebRequest();
            return op.downloadHandler.data;
        }
        
        public byte[] GetByte(string path)
        {
            var task = GetByteAsync(UnityWebRequest.Get(path));
            return  task.GetAwaiter().GetResult();
        }
    }
}