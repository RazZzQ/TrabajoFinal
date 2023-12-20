using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreamerB : ScreamerBase
{
    public float factorRestador = 2;

    public override void AfectarBarraMiedo(BarraDeMiedo miedoManager)
    {
        // Sobrescribe el m�todo base para ajustar el impacto en la barra de miedo
        miedoManager.AumentarBarraMiedo(impactoEnBarraMiedo - factorRestador);
    }
}
