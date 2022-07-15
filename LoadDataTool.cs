using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;


    public class LoadDataTool : BaseSingleton<LoadDataTool>
    {
        private async UniTask<string> GetTextAsync(UnityWebRequest req)
        {
            var op = await req.SendWebRequest();
            return op.downloadHandler.text;
        }

        /// <summary>
        /// 得到路径下的文本数据 
        /// </summary>
        /// <param name="path">数据路径</param>
        /// <returns>文本数据</returns>
        public async UniTask<string> GetTextData(string path)
        {
            var task = GetTextAsync(UnityWebRequest.Get(path));
            var data = await task;
            return data;
        }

        async UniTask<Texture2D> GetTextureAsync(UnityWebRequest req)
        {
            req.downloadHandler = new DownloadHandlerTexture();
            var op = await req.SendWebRequest(); 
            return ((DownloadHandlerTexture) op?.downloadHandler)?.texture;
        }

        public async UniTask<Texture2D> GetTexture2D(string path)
        {
            var task = GetTextureAsync(UnityWebRequest.Get(path));
            return await task;
        }

        private async UniTask<byte[]> GetByteAsync(UnityWebRequest req)
        {
            var op = await req.SendWebRequest();
            return op.downloadHandler.data;
        }

        public async UniTask<byte[]> GetByte(string path)
        {
            var task = GetByteAsync(UnityWebRequest.Get(path));
            return await task;
        }
    }
