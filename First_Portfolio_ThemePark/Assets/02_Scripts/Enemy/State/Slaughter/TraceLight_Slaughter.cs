using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TraceLight_Slaughter : EnemyState
{
    public TraceLight_Slaughter(Enemy _enemy) : base("TraceLight", _enemy) { }

    private Vector3 m_LightPos = Vector3.zero;
    private Transform m_FlashTr = null;




    public override void EnterState()
    {
        Debug.Log("TraceLight 입장!");

        if (m_FlashTr == null)
        {
            m_FlashTr = (m_Enemy as Enemy_Slaughter).FlashTr;
        }
        m_Enemy.Agent.speed = m_Enemy.PatrolSpeed;
        m_Enemy.Agent.destination = (m_Enemy as Enemy_Slaughter).LightPos;
    }

    public override void Action()
    {
        // 빛이 보이는지 체크 -> 빛이 보이면 빛 위치를 저장
        // 저장한 위치로 이동
        (m_Enemy as Enemy_Slaughter).FOV.IsInFovWithRayCheckDirect((m_Enemy as Enemy_Slaughter).TraceDetectRange, (m_Enemy as Enemy_Slaughter).TraceDetectAngle,
                                        "LIGHT", (m_Enemy as Enemy_Slaughter).FOV.mLayerMask, ref m_LightPos, ref m_FlashTr);
        m_Enemy.Agent.destination = new Vector3(m_LightPos.x, m_Enemy.transform.position.y, m_LightPos.z);
    }

    public override void CheckState()
    {
        // 손전등 위치로 계속 레이를 쏜다.
        RaycastHit hitInfo;
        int layerMask = (1 << (m_Enemy as Enemy_Slaughter).FOV.FlashLayer) | (1 << (m_Enemy as Enemy_Slaughter).FOV.ObstacleLayer)
                        | (1 << (m_Enemy as Enemy_Slaughter).FOV.PlayerLayer) | ~(1 << (m_Enemy as Enemy_Slaughter).FOV.LightLayer);

        // 탐지범위 안이라면
        if (Physics.Raycast(m_Enemy.transform.position, m_FlashTr.position - m_Enemy.transform.position,
            out hitInfo, /*(m_Enemy as Enemy_Slaughter).TraceDetectRange + 30f*/ 100f, layerMask))
        {
            // 가로막는게 없고 플레이어가 손전등을 들고있다면 => TracePlayer
            // 20221116 양우석:  플레이어랑 거리 실제로 맞춰보고 수정해야 함.
            if (hitInfo.collider.CompareTag("PLAYER")) //||
                //(hitInfo.collider.CompareTag("FLASH") && Vector3.Distance(m_Enemy.PlayerTr.position, m_FlashTr.position) <= 1.5f))
            {
                Debug.Log("가로막는게 없어서 쩨꼈다!!");
                m_Enemy.SetState((m_Enemy as Enemy_Slaughter).TracePlayer);
                return;
            }
        }

        Debug.DrawLine(m_Enemy.transform.position, m_LightPos, Color.blue);

        // 레이가 안맞았고 마지막 빛 위치랑 현재 위치가 같으면  => Alert
        if (Vector3.Distance(new Vector3(m_LightPos.x, m_Enemy.transform.position.y, m_LightPos.z), m_Enemy.transform.position) <= 0.3f)
        {
            Debug.Log("아무것도 없나?");
            m_Enemy.SetState((m_Enemy as Enemy_Slaughter).Concentration);
        }
    }



    public override void ExitState()
    {
        Debug.Log("TraceLight 퇴장!");
    }
}
