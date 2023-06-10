using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateColorSprites : MonoBehaviour
{
    public Material baseMaterial;
    public int numSimilarColors = 5;
    public GameObject spritePrefab;

    private void Start()
    {
        GenerateColors();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            GenerateColors();
        }
    }

    private void GenerateColors()
    {
        Color baseColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));

        Color[] similarColors = new Color[numSimilarColors];
        for (int i = 0; i < numSimilarColors; i++)
        {
            float rOffset = Random.Range(-0.2f, 0.1f);
            float gOffset = Random.Range(-0.2f, 0.1f);
            float bOffset = Random.Range(-0.2f, 0.1f);

            float r = Mathf.Clamp(baseColor.r + rOffset, 0f, 1f);
            float g = Mathf.Clamp(baseColor.g + gOffset, 0f, 1f);
            float b = Mathf.Clamp(baseColor.b + bOffset, 0f, 1f);

            similarColors[i] = new Color(r, g, b);
            GameObject spriteObject = Instantiate(spritePrefab, transform.position + new Vector3(i * 2f, 0f, 0f), Quaternion.identity);
            SpriteRenderer spriteRenderer = spriteObject.GetComponent<SpriteRenderer>();
            spriteRenderer.color = similarColors[i];
        }
    }
}