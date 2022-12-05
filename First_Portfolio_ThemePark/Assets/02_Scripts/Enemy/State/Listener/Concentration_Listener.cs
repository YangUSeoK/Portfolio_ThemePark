using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Concentration_Listener : EnemyState
{
    public Concentration_Listener(Enemy _enemy) : base("Concentration", _enemy) { }

    private float timer = 0f;
    public override void EnterState()
    {
        Debug.Log("Concentration ����!");
        //m_Enemy.Audio[0].Stop();
        //m_Enemy.Anim.SetTrigger("IsTraceSound");
        //m_Enemy.Audio[1].Play();
        timer = 0f;
    }

    public override void ExitState()
    {
        Debug.Log("Concentration ����!");
    }

    public override void Action()
    {
        Debug.Log("Concentration �׼�!");
    }

    public override void CheckState()
    {
        Debug.Log("Concentration ����!");
        
        timer += Time.deltaTime;

        if (timer >= 2.8f)
        {
            m_Enemy.SetState((m_Enemy as Enemy_Listener).TraceTarget);
        }
    }
}
