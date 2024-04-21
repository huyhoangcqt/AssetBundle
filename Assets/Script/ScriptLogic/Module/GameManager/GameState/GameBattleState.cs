using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameBattleState : BaseState
{
    protected override void onEnter()
    {
        Debuger.Log("GameBattleState Enter");
        SceneMgr.Instance.ChangeSceneAsync(Scene.BattleScene, () => {
            CoroutineManager.startCoroutine(_LoadMapAndMonster());
        });
    }

    IEnumerator _LoadMapAndMonster()
    {
        string mapAssetPath = "Assets/HotupdateAssets/Prefabs/Map/Map_001.prefab";
        SpawnManager.Instance.SpawnMap(mapAssetPath);
        yield return null;
        // SpawnManager.Instance.UserLineup();
        yield return null;
        // SpawnManager.Instance.LoadMonsterLineup();
    }
    
    protected override void onLeave()
    {
        Debuger.Log("GameBattleState Leave");
    }
}