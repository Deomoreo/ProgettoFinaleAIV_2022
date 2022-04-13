using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IHittable
{
    public int Health;
    public float MeleeDamage;
    public UnityEvent<Collider> OnHitEvent;
    //public UnityEvent<float> OnAttackHitted;
    public Animator Anim;
    public NavMeshAgent Agent;
    public Slider HP_Slider;
    public UnityEvent<bool> HandleAnim;
    public UnityEvent OnDeath;
    protected EllenHealthScript ellenHealthScript;

    void Start()
    {
        OnStart();
        
    }

    protected virtual void OnStart()
    {
        GetEllen();
        HP_Slider.maxValue = Health;
        HP_Slider.value = Health;
    }

    public void GetEllen()
    {
        GameObject ellen = GameObject.Find("Ellen PLAYER");
        ellenHealthScript = ellen.GetComponent<EllenHealthScript>();
    }
    public virtual void OnHit(Collider sender)
    {
        OnHitEvent?.Invoke(sender);
        Health--;
        HP_Slider.value = Health;
        if (Health <= 0)
        {
            Agent.speed = 0;
            Anim.SetTrigger("rip");
            HP_Slider.transform.parent.gameObject.SetActive(false);
            OnDeath?.Invoke();
        }
    }
    public virtual void OnAttackStart()
    {
        HandleAnim?.Invoke(true);
    }

    public virtual void OnAttackEnd()
    {
        HandleAnim?.Invoke(false);
    }
    public virtual void OnDeathEndAnim()
    {
        HandleAnim?.Invoke(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (ellenHealthScript == null)
        {
            GetEllen();
        }
        ellenHealthScript.DamagePlayer(MeleeDamage);
    }
}