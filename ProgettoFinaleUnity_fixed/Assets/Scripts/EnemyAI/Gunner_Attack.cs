using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Gunner_Attack : Enemy_Attack
{
    NavMeshAgent agent;

    private float timer = 0f;
    private float speed = 2f;
    //private AnimatorStateInfo infoAnim;
    //private AnimatorTransitionInfo infoTrans;
    //private int animAttacckStateHash = Animator.StringToHash("Chomper_Attack");
    //private int animEndTransitionStateHash = Animator.StringToHash("Chomper_Attack -> Cooldown");
    //private int animStartTransStateHash = Animator.StringToHash("Cooldown -> Chomper_Attack");

    GunnerSM sm;
    public Gunner_Attack(GunnerSM stateMachine) : base("Gunner_Attack", stateMachine)
    {
        sm = stateMachine;
    }

    public override void OnEnter()
    {
        timer = 0f;
        agent = sm.gameObject.GetComponent<NavMeshAgent>();
        sm.DetectCollider.enabled = false;
        //sm.animAct += SetAttackCollider;
    }

    public override void UpdateLogic()
    {
        Debug.Log("ATTACK");
        //infoTrans = sm.anim.GetAnimatorTransitionInfo(0);
        //infoAnim = sm.anim.GetCurrentAnimatorStateInfo(0);
        //if (infoAnim.shortNameHash == animAttacckStateHash || infoTrans.nameHash == animStartTransStateHash)
        //{
        //    if (infoTrans.nameHash == animEndTransitionStateHash)
        //    {
        //        agent.transform.position = sm.anim.transform.position;
        //        sm.anim.transform.localPosition = Vector3.zero;
        //    }
        //    else
        //    {
        //        sm.anim.applyRootMotion = true;
        //    }
        //}
        //else
        //{
        //    sm.anim.applyRootMotion = false;
        //    if (hasAttacked)
        //    {
        //        sm.ChangeState(sm.chaseState);
        //    }
        //}

        Vector3 dest = (sm.ObjToChase.position - sm.transform.position).normalized;
        sm.transform.forward = Vector3.Lerp(sm.transform.forward, new Vector3(dest.x, 0, dest.z), 0.05f);

        if (Vector3.Distance(sm.transform.position, sm.ObjToChase.position) >= (sm.AttackDistance + 1))
        {
            sm.ChangeState(sm.chaseState);
        }

        timer += Time.deltaTime;
        if (timer >= sm.AttackCooldown)
        {
            timer = 0f;
            Attack();
        }
    }
    protected override void Attack()
    {
        GameObject go = sm.BulletTransform.GetComponent<GunnerBulletPoolMgr>().SpawnObj(sm.transform.position + new Vector3(0, 2, 0), Quaternion.identity);
        if (go != null)
        {
            Vector3 velocityOffset;
            Vector3 targetVelocity;

            if (sm.UseVeloictyOffset)
            {
                targetVelocity = sm.ObjToChase.GetComponent<CharacterController>().velocity;
                velocityOffset = new Vector3(targetVelocity.x * 0.6f, targetVelocity.y * 0.2f, targetVelocity.z * 0.6f);
            }
            else
            {
                velocityOffset = Vector3.zero;
            }

            Debug.Log(velocityOffset);
            go.transform.LookAt(sm.ObjToChase.position + new Vector3(0, 1, 0) + velocityOffset, Vector3.up);
        }
    }

    public override void OnExit()
    {
        //sm.anim.SetBool("Run", false);
        agent.speed = speed;
        //sm.animAct -= SetAttackCollider;
    }
}