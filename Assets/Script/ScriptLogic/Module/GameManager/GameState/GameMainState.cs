using UnityEngine;

public class GameMainState : BaseState
{
    protected override void onEnter()
    {
        Debuger.Log("GameMainState Enter");
    }
    protected override void onLeave()
    {
        Debuger.Log("GameMainState Leave");
    }
}