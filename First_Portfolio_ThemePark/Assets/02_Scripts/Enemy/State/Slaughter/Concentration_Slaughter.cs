using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Concentration_Slaughter : EnemyState
{
    public Concentration_Slaughter(Enemy _enemy) : base("Concentration", _enemy) { }

    private Vector3 m_LightPos = Vector3.zero;
    private Transform m_FlashTr = null;
    private float m_Timer = 0f;

    public override void EnterState()
    {
        Debug.Log("Concentration 입장!");
        Debug.Log("좀비가 주위를 살핍니다.");
        (m_Enemy as Enemy_Slaughter).SetConcentration();
        m_Timer = 0f;
        m_Enemy.Anim.SetTrigger("IsConcentration");
    }

    public override void ExitState()
    {
        Debug.Log("Concentration 퇴장!");
    }

    public override void Action()
    {

    }

    public override void CheckState()
    {
        float dist = Vector3.Distance(m_Enemy.PlayerTr.position, m_Enemy.transform.position);

        // 플레이어가 범위안에 들어왔으면
        if (m_FOV.IsInFOV(m_Enemy.AlertDetectRange, m_Enemy.AlertDetectAngle, LayerMask.NameToLayer("PLAYER"))
            && m_FOV.IsLookDirect(m_Enemy.PlayerTr, m_Enemy.PatrolPlayerDetectRange, LayerMask.NameToLayer("PLAYER")))
        {
            Debug.Log("거기 있었구나!");

            m_Enemy.SetState((m_Enemy as Enemy_Slaughter).TracePlayer);
            m_Timer = 0f;
            return;
        }

        // 빛이 범위안에 들어왔으면
        if (m_FOV.IsInFovWithRayCheckDirect(m_Enemy.AlertDetectRange, m_Enemy.AlertDetectAngle, 
            "LIGHT", m_FOV.mLayerMask, ref m_LightPos, ref m_FlashTr))
        {
            Debug.Log("빛을 따라간다..");
            (m_Enemy as Enemy_Slaughter).SetToTraceLight(m_FlashTr, m_LightPos);
            (m_Enemy as Enemy_Slaughter).SetState((m_Enemy as Enemy_Slaughter).TraceLight);
            m_Timer = 0f;
            return;
        }
        
        AggroCheck();
    }

    private void AggroCheck()
    {
        m_Timer += Time.deltaTime;
        if (m_Timer >= 10f)
        {
            (m_Enemy as Enemy_Slaughter).SetState((m_Enemy as Enemy_Slaughter).Patrol);
        }
        Debug.Log($"Concentration : {m_Timer}");
    }
}