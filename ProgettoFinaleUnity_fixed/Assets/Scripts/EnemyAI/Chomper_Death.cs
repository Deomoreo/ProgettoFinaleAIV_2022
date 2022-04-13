using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chomper_Death : Enemy_Death
{
    private float timer = 0f;
    private bool startTimer = false;

    ChomperSM sm;
    public Chomper_Death(ChomperSM stateMachine) : base("Gunner_Death", stateMachine)
    {
        sm = stateMachine;
    }

    public override void OnEnter()
    {
        timer = 0f;
        startTimer = true;

        sm.animAct += OnEndDeathAnimation;

        sm.agent.speed = 0f;
        sm.BodyCollider.enabled = false;
        sm.AttackCollider.enabled = false;
        sm.DetectCollider.enabled = false;
    }

    public override void UpdateLogic()
    {
        if (startTimer)
        {
            timer += Time.deltaTime;
            if (timer >= 1.1f)
            {
                sm.animAct -= OnEndDeathAnimation;
                sm.transform.gameObject.SetActive(false);
                
            }
        }
    }

    public override void OnEndDeathAnimation(bool value)
    {
        startTimer = true;
    }

    public override void OnExit()
    {
        //Disable this enemy
        sm.animAct -= OnEndDeathAnimation;
    }
}
