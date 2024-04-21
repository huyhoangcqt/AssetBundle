using System.Collections;
using UnityEngine;
using UnityEditor;
using System;

public class SpawnManager : MonoBehaviour
{
    private static SpawnManager _instance;
    public static SpawnManager Instance => _instance;

    void Awake()
    {
        _instance = this;
    }

    public void SpawnMap(string path)
    {   
        GameObject mapNode = GameObject.Find(GameNode.BattleSceneNode.MapNode);
        AssetDatabaseMgr.LoadAsyncGameObject(path, (mapAsset) => {
            GameObject mapClone = Instantiate(mapAsset, mapNode.transform);
            mapClone.transform.parent = mapNode.transform;
        });


        // UnityEngine.Object loadedObject = null;
        // StartCoroutine(_LoadAsync(path, out loadedObject));
    }

    // private IEnumerator _SpawnMapAsync(string path)
    // {
        
    // }

    // private IEnumerator _LoadAsync(string path, out UnityEngine.Object loadedObject)
    // {
    //     yield return null;
    //     mapNode = GameObject.Find(GameNode.BattleSceneNode.MapNode);

    //     //Load
    //     // This will load all objects in the fbx and return a single Mesh object.
    //     var obj = AssetDatabase.LoadAssetAtPath(path, );

    //     AssetDatabase.TryGetGUIDAndLocalFileIdentifier(obj, out string guid, out long localId);

    //     // GameObject mapPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(path) as GameObject;
    //     AssetDatabaseLoadOperation op = AssetDatabase.LoadObjectAsync(path, localId);

    //     // yield until the operation is completed
    //     while (!op.isDone)
    //         yield return null;

    //     loadedObject = op.LoadedObject;

    //     GameObject mapClone = Instantiate(mapPrefab, mapNode.transform);
    //     mapClone.transform.parent = transform;
    // }

}
