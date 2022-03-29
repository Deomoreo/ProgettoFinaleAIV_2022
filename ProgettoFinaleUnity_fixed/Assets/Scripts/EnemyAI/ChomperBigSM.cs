using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChomperBigSM : ChomperSM
{
    public List<ChomperSmallSM> Followers;
    public List<Transform> FollowerDestinations;

    public List<Transform> patrolPoints;
    public float TimeBetweenPatrolPoints = 2f;
    public bool Loop;

    protected override BaseState GetInitialState()
    {
        return patrolState;
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        patrolState = new Enemy_PatrolBig(this);
    }
}