using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Text3D : MonoBehaviour
{
    [SerializeField] private string a;
    [SerializeField] private Material material;
    private Texture2D texture;
    public string myString = "Hello, Unity!";
    public Font myFont; // Optional: Assign a font f
    // Example string data
    private string imageData = "R0lGODlhAQABAIAAAAUEBAAAACwAAAAAAQABAAACAkQBADs=";

    // Width and height of the texture
    private int textureWidth = 2;
    private int textureHeight = 2;

    void Start()
    {
        // Apply the texture to a material or game object
        GetComponent<Renderer>().material.mainTexture = texture;
    }
    [ContextMenu("g")]
    public void DrawTextTexture()
    {
        Texture2D texture = new Texture2D(256, 256);
        texture.filterMode = FilterMode.Bilinear;

        // Optional: Set the font style and size
        GUIStyle style = new GUIStyle();
        style.font = myFont != null ? myFont : Font.CreateDynamicFontFromOSFont("Arial", 16);
        style.normal.textColor = Color.white;

        // Draw the string onto the texture
        Rect textRect = new Rect(0, 0, texture.width, texture.height);
        GUIContent content = new GUIContent(myString);
        texture.ReadPixels(textRect, 0, 0);
        texture.Apply();

        // Set the texture to the material
        material.SetTexture("_MainTex", texture);


    }
}
