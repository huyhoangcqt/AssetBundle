using System;
using UnityEngine;
using UnityEditor;
using System.Collections;

public class AssetDatabaseMgr// : Singleton<AssetDatabaseMgr>
{

    private static IEnumerator _LoadAsyncGameObject(string path, Action<GameObject> completeCb)
    {
        GameObject obj = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        AssetDatabase.TryGetGUIDAndLocalFileIdentifier(obj, out string guid, out long localId);
        AssetDatabaseLoadOperation op = AssetDatabase.LoadObjectAsync(path, localId);
        while (!op.isDone)
        {
            yield return null;
        }

        GameObject loadedObject = (GameObject)op.LoadedObject;
        if (completeCb != null){
            completeCb(loadedObject);
        }
    }

    public static void LoadAsyncGameObject(string path, Action<GameObject> completeCb)
    {
        CoroutineManager.startCoroutine(_LoadAsyncGameObject(path, completeCb));
    }

    
    // public Texture2D LoadAsyncTexture()
    // {

    // }
}