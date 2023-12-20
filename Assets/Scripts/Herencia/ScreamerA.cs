using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreamerA : ScreamerBase
{
    public float factorMultiplicador = 3;

    public override void AfectarBarraMiedo(BarraDeMiedo miedoManager)
    {
        // Sobrescribe el método base para ajustar el impacto en la barra de miedo
        miedoManager.AumentarBarraMiedo(impactoEnBarraMiedo * factorMultiplicador);
    }
}
