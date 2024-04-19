using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public enum Scene
{
    MainScene,
    BattleScene,
}

public class SceneMgr : Singleton<SceneMgr>
{
    public Dictionary<Scene, string> mScene;
    public Scene mCurrent;
    
    public SceneMgr() : base()
    {
        Init();
    }

    private void Init()
    {
        mScene = new Dictionary<Scene, string>();
        mScene.Add(Scene.MainScene, "MainScene");
        mScene.Add(Scene.BattleScene, "BattleScene");
    }

    public void ChangeScene(Scene scene)
    {
        if (mScene.ContainsKey(scene))
        {
            SceneManager.LoadScene(mScene[scene]);
        }
        else {
            Debuger.Err("The scene: " + scene + " hasn't been registered!");
        }
    }
}