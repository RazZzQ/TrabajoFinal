using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreamerBase : MonoBehaviour
{
    public float impactoEnBarraMiedo = 10f;

    // Puedes agregar m�s propiedades o m�todos comunes aqu�

    public virtual void AfectarBarraMiedo(BarraDeMiedo miedoManager)
    {
        // M�todo base para afectar la barra de miedo
        miedoManager.AumentarBarraMiedo(impactoEnBarraMiedo);
    }
}
