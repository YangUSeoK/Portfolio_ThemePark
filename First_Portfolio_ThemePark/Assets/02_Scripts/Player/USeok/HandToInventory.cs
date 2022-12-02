using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class HandToInventory : MonoBehaviour
{
    [SerializeField] private XRDirectInteractor m_DirectGrab = null;
    [SerializeField] private GameObject m_GrabbedObject = null;
    public GameObject LeftGrabbedObject
    {
        get { return m_GrabbedObject; }
    }
   

    [SerializeField] private InputActionProperty m_GribButton;
    
    [SerializeField] private GameObject m_Inventory = null; // ����׿�

    private void Update()
    {
        // ���� ������ �ٲ�� �� ������Ʈ�� �޼հ��ӿ�����Ʈ�� ����(�κ��丮��)
        if (m_DirectGrab.interactablesSelected.Count > 0)
        {
            if (m_GrabbedObject != m_DirectGrab.interactablesSelected[0].transform.gameObject)
            {
                m_GrabbedObject = m_DirectGrab.interactablesSelected[0].transform.gameObject;
                m_GrabbedObject.transform.SetParent(transform,false);
            }
        }
        else
        {
            m_GrabbedObject = null;
        }

        if (m_Inventory != null)
        {
            if (m_GribButton.action.WasPressedThisFrame()) 
            {
                m_Inventory.GetComponent<ItemSlot>().PopItem();
            }
            else if (m_GribButton.action.WasReleasedThisFrame())
            {
                m_Inventory.GetComponent<ItemSlot>().PushItem();
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("INVEN"))
        {
            if(m_Inventory == null)
            {
                m_Inventory = other.gameObject;
            }
        }   
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("INVEN"))
        {
            if(m_Inventory != null)
            {
                m_Inventory = null;
            }
        }
    }
}