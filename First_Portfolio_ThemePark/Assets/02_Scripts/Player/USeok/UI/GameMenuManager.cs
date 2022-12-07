using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject m_Menu = null;
    [SerializeField] private GameObject m_MiniMap = null;
    
    [SerializeField] private InputActionProperty m_ShowMenuButton;
    [SerializeField] private InputActionProperty m_ShowMiniMapButton;



    [SerializeField] private Transform m_Head = null;
    [SerializeField] private Transform m_MenuPos = null;
    [SerializeField] private Transform m_MiniMapPos = null;

    [SerializeField] private SkinnedMeshRenderer m_LHandRenderer = null;
    [SerializeField] private SkinnedMeshRenderer m_RHandRenderer = null;

    private float spawnDistance = 1f;

    private void Update()
    {
        if (m_ShowMenuButton.action.WasPressedThisFrame())
        {
            m_Menu.SetActive(!m_Menu.activeSelf);
            if (m_LHandRenderer.enabled)
            {
                m_LHandRenderer.enabled = false;
            }
            else
            {
                m_LHandRenderer.enabled = true;
            }
        }
        m_Menu.transform.position = m_MenuPos.position;
        m_Menu.transform.LookAt(new Vector3(m_Head.position.x, m_Head.position.y, m_Head.position.z));
        m_Menu.transform.forward *= -1;



        if (m_ShowMiniMapButton.action.WasPressedThisFrame())
        {
            m_MiniMap.SetActive(!m_MiniMap.activeSelf);
            if (m_RHandRenderer.enabled)
            {
                m_RHandRenderer.enabled = false;
            }
            else
            {
                m_RHandRenderer.enabled = true;
            }
        }
        m_MiniMap.transform.position = m_MiniMapPos.position;
        m_MiniMap.transform.LookAt(new Vector3(m_Head.position.x, m_Head.position.y, m_Head.position.z));
        m_MiniMap.transform.forward *= -1;
    }

    
}
