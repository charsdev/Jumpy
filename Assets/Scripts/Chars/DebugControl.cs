using UnityEngine;
using System.Collections;
using System;


/// <summary>
/// Prueba el valor de la tecla del gamepad
/// </summary>
public class DebugControl : MonoBehaviour
{
    private string currentButton; // El botón actualmente presionado

    // Use this for initialization 
    void Start()
    {

    }
    // Update is called once per frame 
    void Update()
    {
        Array values = Enum.GetValues​​(typeof(KeyCode));
        for (int x = 0; x < values.Length; x++)
        {
            if (Input.GetKeyDown((KeyCode)values.GetValue(x)))
            {
                currentButton = values.GetValue(x).ToString(); // recorrer y obtener el botón actualmente presionado
            }
        }
    }
    // Show some data 
    void OnGUI()
    {
        GUI.TextArea(new Rect(0, 0, 250, 40), "Current Button:" + currentButton); // Use la GUI para imprimir el botón actualmente presionado en la pantalla en tiempo real
    }
}