using UnityEngine;

public class GameBattleState : BaseState
{
    protected override void onEnter()
    {
        Debuger.Log("GameBattleState Enter");
        SceneMgr.Instance.ChangeScene(Scene.BattleScene);
    }

    protected override void onLeave()
    {
        Debuger.Log("GameBattleState Leave");
    }
}