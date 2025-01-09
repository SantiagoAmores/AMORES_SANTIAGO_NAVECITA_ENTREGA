using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI monedasText;        // Texto para el contador de monedas
    public TextMeshProUGUI arosText;           // Texto para el contador de aros
    public TextMeshProUGUI temporizadorText;   // Texto para el temporizador

    public TextMeshProUGUI mensajeFinalText;   // Texto para mostrar el mensaje final en el panel
    public GameObject panelFinal;             // Panel que aparece al final

    public float tiempoInicial = 300f;        // Tiempo inicial en segundos

    public GameObject monedaPrefab;           // Prefab de la moneda
    public List<Transform> puntosMonedas = new List<Transform>(); // Lista de posiciones para generar monedas

    private int monedasRecolectadas = 0;      // Contador de monedas recolectadas
    public int arosAtravesados = 0;           // Contador de aros atravesados
    private float tiempoRestante;             // Tiempo restante para el temporizador
    private bool juegoTerminado = false;      // Estado del juego

    private void Start()
    {
        // Inicializar panel final oculto
        panelFinal.SetActive(false);

        // Buscar todos los objetos con el tag "PosicionMoneda" y agregarlos a la lista
        GameObject[] puntos = GameObject.FindGameObjectsWithTag("PosicionMoneda");
        foreach (GameObject punto in puntos)
        {
            puntosMonedas.Add(punto.transform);
        }

        // Inicializar el temporizador
        tiempoRestante = tiempoInicial;

        // Inicializar los textos de UI
        ActualizarUI();

        // Hacer aparecer las monedas
        SpawnMonedas();
    }

    private void Update()
    {
        // Actualizar el temporizador solo si el juego no ha terminado
        if (!juegoTerminado)
        {
            if (tiempoRestante > 0)
            {
                tiempoRestante -= Time.deltaTime;
                temporizadorText.text = FormatearTiempo(tiempoRestante);
            }
            else
            {
                tiempoRestante = 0;
                temporizadorText.text = "Tiempo: 00:00";
                TerminarJuego(false); // El tiempo ha llegado a cero, derrota
            }
        }
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
                    Vector3 spawnPosition = puntosMonedas[randomIndex].position;

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

    // Método para incrementar el contador de monedas
    public void IncrementarContadorMonedas()
    {
        monedasRecolectadas++;
        ActualizarUI();
    }

    // Método para incrementar el contador de aros
    public void IncrementarContadorAros()
    {
        arosAtravesados++;
        ActualizarUI();

        // Verificar si se ha atravesado el último aro
        if (arosAtravesados > 9)
        {
            TerminarJuego(true); // Victoria
        }
    }

    // Método para terminar el juego
    private void TerminarJuego(bool victoria)
    {
        juegoTerminado = true;
        panelFinal.SetActive(true); // Mostrar el panel final

        if (victoria)
        {
            // Calcular la puntuación: tiempo sobrante + monedas recolectadas * 2
            int puntuacion = (int)tiempoRestante + (monedasRecolectadas * 2);
            mensajeFinalText.text = $"¡Victoria!\nPuntuación: {puntuacion}";
        }
        else
        {
            mensajeFinalText.text = "¡Derrota!\nEl tiempo se ha agotado.";
        }

        // Detener el tiempo
        Time.timeScale = 0f;
    }

    // Método para actualizar la UI
    private void ActualizarUI()
    {
        monedasText.text = $"Monedas: {monedasRecolectadas}";
        arosText.text = $"Aros: {arosAtravesados}";
        temporizadorText.text = FormatearTiempo(tiempoRestante);
    }

    // Método para formatear el tiempo (minutos:segundos)
    private string FormatearTiempo(float tiempo)
    {
        int minutos = (int)(tiempo / 60);
        int segundos = (int)(tiempo % 60);
        return $"Tiempo: {minutos:00}:{segundos:00}";
    }
}


