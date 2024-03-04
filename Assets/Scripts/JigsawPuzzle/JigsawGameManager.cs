using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JigsawGameManager : MonoBehaviour
{
    [Header("Jigsaw Puzzle Settings")]
    [SerializeField] private List<Texture2D> jigsawPuzzleImages;
    
    [Range(2, 6)]
    [SerializeField] private int difficulty = 4;
    [SerializeField] private Transform piecePrefab;
    [SerializeField] private Transform gameHolder;

    private List<Transform> puzzlePieces;
    private Vector2Int puzzleDimensions;
    private float width;
    private float height;
    
    /**
     * Start is called before the first frame update
     */
    private void Start()
    {
        // Initialize the list of puzzle pieces
        puzzlePieces = new List<Transform>();
        
        // Calculate the size of each piece
        puzzleDimensions = GetDimensions(jigsawPuzzleImages[0], difficulty);
        
        // Create the jigsaw pieces from the texture
        CreateJigsawPieces(jigsawPuzzleImages[0]);
    }
    
    /**
     * Get the dimensions of the puzzle based on the difficulty and the texture
     */
    private Vector2Int GetDimensions(Texture2D jigsawTexture, int difficulty)
    {
        Vector2Int dimensions = Vector2Int.zero;
        
        // Difficulty is the number of pieces on the smallest texture dimension.
        // This helps ensure the pieces are as square as possible.

        if (jigsawTexture.width < jigsawTexture.height)
        {
            dimensions.x = difficulty;
            dimensions.y = (difficulty * jigsawTexture.height) / jigsawTexture.width;
        }
        else
        {
            dimensions.x = (difficulty * jigsawTexture.width) / jigsawTexture.height;
            dimensions.y = difficulty;
        }
        
        return dimensions;
    }
    /**
     * Create the jigsaw pieces from the texture
     */
    private void CreateJigsawPieces(Texture2D jigsawTexture)
    {
        // Calculate the size of each piece based on the dimensions of the texture
        height = 1f / puzzleDimensions.y;
        float aspect = (float)jigsawTexture.width / jigsawTexture.height;
        width = aspect / puzzleDimensions.x;
        
        // Create the pieces
        for (int row = 0; row < puzzleDimensions.y; ++row)
        {
            for (int col = 0; col < puzzleDimensions.x; ++col)
            {
                // Create the piece in the right location of the right size
                Transform piece = Instantiate(piecePrefab, gameHolder);
                piece.localPosition = new Vector3(
                    (-width * puzzleDimensions.x / 2) + (width * col) + (width / 2), 
                    (-height * puzzleDimensions.y / 2) + (height * row) + (height / 2), 
                    -1);
                piece.localScale = new Vector3(width, height, 1f);
                
                piece.name = "Piece " + row + " " + col;
                puzzlePieces.Add(piece);
                
                // Set the texture of the piece
                float width1 = 1f / puzzleDimensions.x;
                float height1 = 1f / puzzleDimensions.y;
                
                // UV coordinates for the piece: (0, 0), (1, 0), (0, 1), (1, 1)
                Vector2[] uvs = new Vector2[4];
                uvs[0] = new Vector2(width1 * col, height1 * row);
                uvs[1] = new Vector2(width1 * (col + 1), height1 * row);
                uvs[2] = new Vector2(width1 * col, height1 * (row + 1));
                uvs[3] = new Vector2(width1 * (col + 1), height1 * (row + 1));
                
                // Assign the UVs to the mesh
                Mesh mesh = piece.GetComponent<MeshFilter>().mesh;
                mesh.uv = uvs;
                
                piece.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", jigsawTexture);
            }
        }
    }
}