using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreamerBase : MonoBehaviour
{
    public float impactoEnBarraMiedo = 10f;

    // Puedes agregar más propiedades o métodos comunes aquí

    public virtual void AfectarBarraMiedo(BarraDeMiedo miedoManager)
    {
        // Método base para afectar la barra de miedo
        miedoManager.AumentarBarraMiedo(impactoEnBarraMiedo);
    }
}
