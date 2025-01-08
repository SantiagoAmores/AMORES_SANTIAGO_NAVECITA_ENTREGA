using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aro : MonoBehaviour
{
    public ArosManager aroManager;  // Referencia al ArosManager para avisarle

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que atraviesa el aro es el jugador
        if (other.CompareTag("Player"))
        {
            // Avisar al ArosManager que el aro ha sido cruzado
            aroManager.CambiarAro(gameObject);  // Enviar el aro cruzado al ArosManager
        }
    }
}

