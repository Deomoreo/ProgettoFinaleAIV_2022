using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperSmallSM : ChomperSM
{
    public ChomperBigSM Leader;
    public int ID;

    protected override BaseState GetInitialState()
    {
        return patrolState;
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        patrolState = new Enemy_PatrolSmall(this);
    }
}