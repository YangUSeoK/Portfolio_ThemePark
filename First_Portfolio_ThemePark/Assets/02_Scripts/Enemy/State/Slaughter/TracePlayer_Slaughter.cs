using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class TracePlayer_Slaughter : EnemyState
{
    public TracePlayer_Slaughter(Enemy _enemy) : base("TracePlayer", _enemy) { }

    
    
    private Vector3 m_PlayerPos = Vector3.zero; // 마지막으로 본 플레이어 위치 저장변수
    private bool mbIsLookPlayer = false;
    private float m_Timer = 0f;

    public override void EnterState()
    {
        Debug.Log("TracePlayer 입장!");
        Debug.Log("주변 좀비를 부릅니다!");
        m_Enemy.Agent.speed = m_Enemy.TraceSpeed;
        m_PlayerPos = m_Enemy.PlayerTr.position;
        m_Enemy.Agent.destination = m_PlayerPos;

        m_Timer = 0f;
        m_Enemy.Anim.SetTrigger("IsTracePlayer");

        // 주변 좀비 부르는 함수
        (m_Enemy as Enemy_Slaughter).CallNearZombie();

        // BGM 바꾸기
        (m_Enemy as Enemy_Slaughter).EnterTracePlayerCallback();
    }

    public override void ExitState()
    {
        Debug.Log("TracePlayer 퇴장!");
    }

    public override void Action()
    {
        // 플레이어에게 레이를 쏴서 위치정보 업데이트 받음
        RaycastHit hitInfo;       
        int layerMask = ((m_Enemy as Enemy_Slaughter).FOV.PlayerLayer | (m_Enemy as Enemy_Slaughter).FOV.ObstacleLayer);

        Debug.DrawLine(m_Enemy.transform.position, m_Enemy.PlayerTr.position, Color.blue);

        if (Physics.Raycast(m_Enemy.transform.position, m_Enemy.PlayerTr.position - m_Enemy.transform.position,
            out hitInfo, (m_Enemy as Enemy_Slaughter).TraceDetectRange+ 30f, layerMask))
        {
            Debug.Log(hitInfo.transform.name);
            if (hitInfo.collider.CompareTag("PLAYER"))
            {
                Debug.Log("플레이어 직관");
                mbIsLookPlayer = true;
                m_PlayerPos = hitInfo.transform.position;
            }
            else
            {
                mbIsLookPlayer = false;
            }
        }
        else
        {
            mbIsLookPlayer = false;
        }
        m_Enemy.Agent.destination = m_PlayerPos;
    }

    public override void CheckState()
    {
        float dist = Vector3.Distance(m_Enemy.PlayerTr.position, m_Enemy.transform.position);

        if (mbIsLookPlayer)
        {
            m_Timer = 0;
            if ((m_Enemy as Enemy_Slaughter).AttackRange >= dist)
            {
                Debug.Log("죽어라 닝겐!");
                m_Enemy.SetState((m_Enemy as Enemy_Slaughter).Attack);
                return;
            }
        }
        else
        {
            m_Timer += Time.deltaTime;
            Debug.Log($"Trace Player : {m_Timer}");

            if (m_Timer >= 5f)
            {
                Debug.Log("어디갔지?");
                m_Enemy.SetState((m_Enemy as Enemy_Slaughter).Concentration);
            }
        }
    }
}
