using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneda : MonoBehaviour
{
    public float balanceoAltura = 0.5f;   // Altura máxima del balanceo en el eje Y
    public float balanceoVelocidad = 2f; // Velocidad del balanceo
    public float rotacionBase = 50f;     // Velocidad base de rotación
    public float maxRotacion = 400f;     // Velocidad máxima de rotación
    public Transform nave;               // Referencia a la nave para calcular la distancia
    public float distanciaMaxima = 50f;

    private Vector3 posicionInicial;     // Posición inicial de la moneda

    private void Start()
    {
        // Guardar la posición inicial de la moneda
        posicionInicial = transform.position;

        // Buscar la nave si no se ha asignado
        if (nave == null)
        {
            nave = GameObject.FindWithTag("Nave").transform;
        }
    }

    private void Update()
    {
        // Balanceo en el eje Y
        float nuevoY = posicionInicial.y - Mathf.Sin(Time.time * balanceoVelocidad) * balanceoAltura;
        transform.position = new Vector3(posicionInicial.x, nuevoY, posicionInicial.z);

        // Rotación basada en la distancia a la nave
        if (nave != null)
        {
            float distancia = Vector3.Distance(transform.position, nave.position);
            Debug.Log(distancia);
            float factor = Mathf.Clamp01((distanciaMaxima - distancia) / distanciaMaxima);
            float velocidadRotacion = Mathf.Lerp(rotacionBase, maxRotacion, factor); // Aumenta la velocidad al acercarse
            transform.Rotate(Vector3.forward, velocidadRotacion * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si colisiona con la nave (etiquetada como "Player"), desaparece
        if (other.CompareTag("Nave"))
        {
            // Incrementar el contador de monedas en el GameManager
            FindObjectOfType<GameManager>().IncrementarContadorMonedas();

            // Destruir la moneda
            Destroy(gameObject);
        }
    }
}
