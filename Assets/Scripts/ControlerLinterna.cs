using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlerLinterna : MonoBehaviour
{
    public Light LinternaPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (LinternaPlayer.enabled == true)
            {
                LinternaPlayer.enabled = false;
            }
            else if(LinternaPlayer.enabled == false)
            {
                LinternaPlayer.enabled = true;
            }
            
        }
    }
}
