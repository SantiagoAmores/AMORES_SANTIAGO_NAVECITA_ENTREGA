using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject monedaPrefab;  // Prefab de la moneda
    public Transform[] puntosDeAros; // Los 50 puntos vacíos (emptys) donde se generarán las monedas

    private void Start()
    {
        SpawnMonedas();
    }

    // Método para manejar el spawn de las monedas
    private void SpawnMonedas()
    {
        // Distribuir monedas en los primeros 5 aros (15 monedas aleatorias en los primeros 25 puntos)
        SpawnMonedasEnRango(0, 5, 15, 0, 25);

        // Distribuir monedas en los siguientes 3 aros (5 monedas aleatorias en los siguientes 15 puntos)
        SpawnMonedasEnRango(5, 8, 5, 25, 40);

        // No se distribuyen monedas en los últimos 2 aros (0 monedas)
        // Este paso se omite porque no se generan monedas en esa parte
    }

    // Método para spawn de monedas en un rango específico
    private void SpawnMonedasEnRango(int inicio, int fin, int cantidadMonedas, int rangoInicio, int rangoFin)
    {
        List<int> indicesDisponibles = new List<int>();

        // Rellenamos los indices disponibles según el rango
        for (int i = rangoInicio; i < rangoFin; i++)
        {
            indicesDisponibles.Add(i);
        }

        // Mezclamos los indices disponibles para aleatorizar
        for (int i = 0; i < indicesDisponibles.Count; i++)
        {
            int temp = indicesDisponibles[i];
            int randomIndex = Random.Range(i, indicesDisponibles.Count);
            indicesDisponibles[i] = indicesDisponibles[randomIndex];
            indicesDisponibles[randomIndex] = temp;
        }

        // Generamos las monedas en posiciones aleatorias dentro del rango de puntos
        for (int i = inicio; i < fin; i++)
        {
            for (int j = 0; j < cantidadMonedas; j++)
            {
                if (indicesDisponibles.Count > 0)
                {
                    int randomIndex = indicesDisponibles[Random.Range(0, indicesDisponibles.Count)];
                    Vector3 spawnPosition = puntosDeAros[randomIndex].position;

                    // Aplicar la rotación a la moneda
                    Vector3 eulerAngles = new Vector3(-90, 0, 0);  // Rotación para alinear la moneda correctamente
                    Quaternion rotation = Quaternion.Euler(eulerAngles);

                    // Instanciar la moneda con la rotación correcta
                    Instantiate(monedaPrefab, spawnPosition, rotation);

                    indicesDisponibles.Remove(randomIndex); // Evitamos duplicados
                }
            }
        }
    }
}
