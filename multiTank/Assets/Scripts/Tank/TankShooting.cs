﻿using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour
{
    public int m_PlayerNumber = 1;       
    public Rigidbody m_Shell;            
    public Transform m_FireTransform;    
    public Slider m_AimSlider;           
    public AudioSource m_ShootingAudio;  
    public AudioClip m_ChargingClip;     
    public AudioClip m_FireClip;         
    public float m_MinLaunchForce = 15f; 
    public float m_MaxLaunchForce = 30f; 
    public float m_MaxChargeTime = 0.75f;

    private string m_FireButton;         
    private float m_CurrentLaunchForce;  
    private float m_ChargeSpeed;         
    private bool m_Fired;                


    private void OnEnable()
    {
        m_CurrentLaunchForce = m_MinLaunchForce;
        m_AimSlider.value = m_MinLaunchForce;
    }


    private void Start()
    {
        m_FireButton = "Fire" + m_PlayerNumber;

        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;
    }

    private void Update()
    {
        // Track the current state of the fire button and make decisions based on the current launch force.
        m_AimSlider.value = m_MinLaunchForce;
        if (m_CurrentLaunchForce >= m_MaxLaunchForce
            && !m_Fired)
        {
            //at max charge not yet fired
            m_CurrentLaunchForce = m_MaxLaunchForce;
            Fire();

        }
        else if (Input.GetButtonDown(m_FireButton))
        {
            // have we pressed fire fpor the first time?


        }
        else if (Input.GetButton(m_FireButton) && !m_Fired)
        {
            // holding the fire button, not yet fired

        }
        else if (Input.GetButtonUp(m_FireButton) && !m_Fired)
        {
            // we release the button, button hasnt fired yet

        }
    }


    private void Fire()
    {
        // Instantiate and launch the shell.
    }
}