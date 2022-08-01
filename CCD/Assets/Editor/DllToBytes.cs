using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class DllToBytes
{
    public static string normalPath =Application.streamingAssetsPath ;
    public static string normalPathToSave =Application.dataPath + "/Model" ;
    // public static string normalPath =Application.streamingAssetsPath+ "/UnityHotFix.dll";
    // public static string normalPathToSave =Application.streamingAssetsPath+ "/UnityHotFix.pdb";
    [MenuItem("MyMenu/ILRuntime/DllToByte")]
    public static void DllToByte()
    {
        DllToByte(true);
    }
    [MenuItem("MyMenu/ILRuntime/DllToByteChoose")]
    public static void DllToByteChoose()
    {
        DllToByte(false);
    }

    private static void DllToByte(bool autoChoosePath)
    {
        string folderPath,savePath;
        if (autoChoosePath)
        {
            folderPath = normalPath;
        }
        else
        {
            folderPath = EditorUtility.OpenFolderPanel("dll所在的文件夹", Application.streamingAssetsPath  , string.Empty);
        }

        if (string.IsNullOrEmpty(folderPath))
        {
            return;
        }
        DirectoryInfo directoryInfo=new DirectoryInfo(folderPath);
        FileInfo[] fileInfos = directoryInfo.GetFiles();
        List<FileInfo> listDll=new List<FileInfo>();
        List<FileInfo> listPdb=new List<FileInfo>();

        for (int i = 0; i <fileInfos.Length; i++)
        {
            if (fileInfos[i].Extension==".dll")
            {
                listDll.Add(fileInfos[i]);
            }
            
            else  if (fileInfos[i].Extension==".pdb")
            {
                listPdb.Add(fileInfos[i]);
            }
        }

        if (listDll.Count+listPdb.Count<=0)
        {
            Debug.Log("文件夹下没有dll文件");
        }
        else
        {
            Debug.Log("路径为："+folderPath);
        }

        if (autoChoosePath)
        {
            savePath = normalPathToSave;
        }
        else
        {
            savePath= EditorUtility.OpenFolderPanel("dll要保存的文件夹", Application.persistentDataPath  , string.Empty);
        }
       
        Debug.Log("-----开始转换dll文件------");
        string path = string.Empty;
        for (int i = 0; i < listDll.Count; i++)
        {
            //$ 符号的作用：等同于string.Format(),不用写占位符了，直接拼起来就可以了
            
            path = $"{savePath}/{Path.GetFileNameWithoutExtension(listDll[i].Name)}_dll_res.bytes";
            Debug.Log(path);
            BytesToFile(path,FileToBytes(listDll[i]));
        }
        Debug.Log("------dll文件转换结束---------");
        
        Debug.Log("-----开始转换pdb文件------");
        for (int i = 0; i < listPdb.Count; i++)
        {
            //$ 符号的作用：等同于string.Format(),不用写占位符了，直接拼起来就可以了
            
            path = $"{savePath}/{Path.GetFileNameWithoutExtension(listPdb[i].Name)}_pdb_res.bytes";
            BytesToFile(path,FileToBytes(listPdb[i]));
        }
        Debug.Log("------pdb文件转换结束---------");
        AssetDatabase.Refresh();

    }
    /// <summary>
    /// file转byte
    /// </summary>
    /// <param name="fileInfo"></param>
    /// <returns></returns>
    private static byte[] FileToBytes(FileInfo fileInfo)
    {
        return File.ReadAllBytes(fileInfo.FullName);
    }
    /// <summary>
    /// byte转文件
    /// </summary>
    /// <param name="path"></param>
    /// <param name="bytes"></param>
    private static void BytesToFile(string path, byte[] bytes)
    {
        Debug.Log($"路径为：{path}\nlength:{bytes.Length}");
        File.WriteAllBytes(path,bytes);
    }
        
}

