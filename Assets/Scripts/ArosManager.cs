using UnityEngine;

public class ArosManager : MonoBehaviour
{
    public GameObject[] listaAros;

    private void Start()
    {
        // Inicializamos los aros al comienzo del juego
        EstablecerAros();
    }

    public void EstablecerAros()
    {
        // Obtener todos los aros en la escena
        listaAros = GameObject.FindGameObjectsWithTag("Aro");

        // Iterar a través de los aros
        for (int i = 0; i < listaAros.Length; i++)
        {
            Renderer renderer = listaAros[i].GetComponent<Renderer>();

            // Si es el primer aro disponible, ponerlo en verde
            if (i == 0 && listaAros[i].CompareTag("Aro"))
            {
                renderer.material.color = Color.green;
            }
            // Si ya fue completado, mantenerlo en rojo
            else if (listaAros[i].CompareTag("AroCompletado"))
            {
                renderer.material.color = Color.red;
            }
            // Si es un aro no alcanzable, ponerlo en negro
            else
            {
                renderer.material.color = Color.black;
            }
        }
    }
}







