using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlerLinterna : MonoBehaviour
{
    public Light LinternaPlayer;
    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (LinternaPlayer.enabled == true)
                {
                    LinternaPlayer.enabled = false;
                }
                else if (LinternaPlayer.enabled == false)
                {
                    LinternaPlayer.enabled = true;
                }
            }
        }
    }
}
