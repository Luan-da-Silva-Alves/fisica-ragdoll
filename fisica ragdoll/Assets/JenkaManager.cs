using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Rendering;

public class JenkaManager : MonoBehaviour
{
    [Header("Towe Settings")]
    public int layers = 3;
    public int piecesPerLayer = 3;
    public List<GameObject> piecePrefabs;

    [Header("Piece Size Settings")]
    public float pieceSpacing = 0f;
    public float pieceHeight = 0.5f;
    public float pieceLength = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BuildTower();
    }


    void BuildTower()
    {
        for (int layerIndex = 0; layerIndex < layers; layerIndex++)
        {
            GenerateLayer(layerIndex);
        }
    }

    void GenerateLayer(int layerIndex)
    {
        Vector3 basePosition = transform.position;
        basePosition.y += layerIndex * (pieceHeight + pieceSpacing);

        bool isOdd = IsOddLayer(layerIndex);
        Quaternion rotation = isOdd ? Quaternion.Euler(0, 90, 0) : Quaternion.identity;
        Vector3 direction = isOdd ? Vector3.right : Vector3.forward;

        //centro da camada :metade do tamanho total ocupado
        float totalWidth = (piecesPerLayer - 1) * (pieceLength + pieceSpacing);
        Vector3 startOffset = -direction * (totalWidth / 2f);
    }




    bool IsOddLayer(int layerIndex)
    {
        return layerIndex % 2 != 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
