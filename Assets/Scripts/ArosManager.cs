using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArosManager : MonoBehaviour
{
    public Material colorInicial;     // Color negro (sin cruzar)
    public Material colorSiguiente;  // Color verde (el siguiente aro a cruzar)
    public Material colorCruzado;    // Color rojo (ya cruzado)

    public List<GameObject> arosList = new List<GameObject>(); // Lista de aros en orden
    private int aroActual = 0; // Índice del aro que debe cruzarse

    private void Start()
    {
        // Buscar todos los hijos del objeto que tiene este script (empty con los aros)
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Aro"))  // Asegúrate de que cada aro tenga el tag "Aro"
            {
                arosList.Add(child.gameObject); // Añadir el aro a la lista
                child.GetComponent<Renderer>().material = colorInicial; // Inicialmente, todos los aros serán negros
            }
        }

        // Establecer el primer aro como el siguiente a cruzar y darle color verde
        if (arosList.Count > 0)
        {
            arosList[aroActual].GetComponent<Renderer>().material = colorSiguiente; // El primer aro se vuelve verde
            arosList[aroActual].tag = "AroVerde"; // Cambiar el tag al primer aro
        }
    }

    // Método que cambia el aro actual y marca el siguiente como verde
    public void CambiarAro(GameObject aroCruzado)
    {
        // Verificar que el aro cruzado es el aro correcto
        if (arosList[aroActual] == aroCruzado)
        {
            // Cambiar el color a rojo (ya cruzado)
            aroCruzado.GetComponent<Renderer>().material = colorCruzado;

            // Cambiar el tag a "AroRojo"
            aroCruzado.tag = "AroRojo";

            // Incrementar el contador de aros en el GameManager
            FindObjectOfType<GameManager>().IncrementarContadorAros();

            // Avanzar al siguiente aro
            aroActual++;

            // Cambiar el color del siguiente aro a verde (si existe)
            if (aroActual < arosList.Count)
            {
                arosList[aroActual].GetComponent<Renderer>().material = colorSiguiente; // El siguiente aro se vuelve verde
                arosList[aroActual].tag = "AroVerde"; // Establecer el siguiente aro como verde
            }
        }
    }
}





