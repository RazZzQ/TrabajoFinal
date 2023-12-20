using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LinternaTemporal : MonoBehaviour
{
    public Light LinternaPlayer;
    ParticleSystem particulas;
    public event Action OnLinternOff;
    public event Action OnLinternOn;
    // Start is called before the first frame update
    void Start()
    {
        particulas = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (Input.GetButtonDown("LinternOnOff"))
            {
                if (LinternaPlayer.enabled == true)
                {
                    particulas.Stop();
                    LinternaPlayer.enabled = false;

                    OnLinternOff?.Invoke();
                }
                else if (LinternaPlayer.enabled == false)
                {
                    particulas.Play();
                    LinternaPlayer.enabled = true;

                    OnLinternOn?.Invoke();
                }
            }
        }
    }
}
