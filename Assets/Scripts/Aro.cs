using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aro : MonoBehaviour
{
    private GameManager gameManager;
    private ArosManager aroManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        aroManager = FindObjectOfType<ArosManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        // Verificamos si el objeto que colisiona tiene el tag "Player"
        if (other.gameObject.CompareTag("Player"))
        {
            Renderer colRenderer = gameObject.GetComponent<Renderer>();

            // Verificamos si el aro es de color verde
            if (colRenderer.material.color == Color.green)
            {
                // Cambiamos el tag del aro para marcarlo como completado
                gameObject.tag = "AroCompletado";

                // Cambiamos el color del aro a rojo
                colRenderer.material.color = Color.red;

                // Incrementamos el contador de aros atravesados
                gameManager.IncrementarContadorAros();

                // Actualizamos los aros en el ArosManager
                aroManager.EstablecerAros();
            }
        }
    }
}

