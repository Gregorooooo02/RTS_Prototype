using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

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
    
    public LayerMask pieceLayer;
    private GameObject selectedPiece;
    private bool isDragging;
    private Vector3 offset;
    
    /**
     * Start is called before the first frame update
     */
    private void Start()
    {
        // Initialize the list of puzzle pieces
        puzzlePieces = new List<Transform>();
        isDragging = false;
        
        // Calculate the size of each piece
        puzzleDimensions = GetDimensions(jigsawPuzzleImages[0], difficulty);
        
        // Create the jigsaw pieces from the texture
        CreateJigsawPieces(jigsawPuzzleImages[0]);
        
        // Shuffle the pieces
        ShufflePieces();
        
        // Update the border to fit the chosen puzzle
        UpdateBorder();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, pieceLayer);
            
            if (hit.collider != null)
            {
                selectedPiece = hit.collider.gameObject;
                offset = selectedPiece.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 14));
                offset += Vector3.back;
                isDragging = true;
            }
        }

        if (isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 14));
            mousePos += offset;
            selectedPiece.transform.position = mousePos;
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            selectedPiece.transform.position += Vector3.forward;
            SnapAndDisableIfCorrect();
            isDragging = false;
        }
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

    /**
     * Shuffle the pieces
     */
    private void ShufflePieces()
    {
        // Calculate the visible size of the screen
        float orthoHeight = Camera.main.fieldOfView / 8.5f;
        float screenAspect = (float)Screen.width / Screen.height;
        float orthoWidth = orthoHeight * screenAspect;
        
        // Ensure pieces are away for the edges
        var localScale = gameHolder.localScale;
        float pieceWidth = width * localScale.x;
        float pieceHeight = height * localScale.y;
        
        orthoWidth -= pieceWidth;
        orthoHeight -= pieceHeight;
        
        // Place each piece in a random location
        foreach (Transform piece in puzzlePieces)
        {
            float x = Random.Range(-orthoWidth, orthoWidth);
            float y = Random.Range(-orthoHeight, orthoHeight);
            piece.position = new Vector3(x, y, -1);
        }
    }

    /**
     * Update the border to fit the chosen puzzle
     */
    private void UpdateBorder()
    {
        LineRenderer lineRenderer = gameHolder.GetComponent<LineRenderer>();
        
        // Calculate the size of the border
        float halfWidth = (width * puzzleDimensions.x) / 2;
        float halfHeight = (height * puzzleDimensions.y) / 2;
        
        // Border behind the pieces
        float borderZ = 0f;
        
        // Set the positions of the border
        lineRenderer.SetPosition(0, new Vector3(-halfWidth, halfHeight, borderZ));
        lineRenderer.SetPosition(1, new Vector3(halfWidth, halfHeight, borderZ));
        lineRenderer.SetPosition(2, new Vector3(halfWidth, -halfHeight, borderZ));
        lineRenderer.SetPosition(3, new Vector3(-halfWidth, -halfHeight, borderZ));
        
        // Set the thickness of the border
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        
        // Show the border
        lineRenderer.enabled = true;
    }

    private void SnapAndDisableIfCorrect()
    {
        int pieceIndex = puzzlePieces.IndexOf(selectedPiece.transform);
        
        // The coordinates of the piece in the puzzle
        int col = pieceIndex % puzzleDimensions.x;
        int row = pieceIndex / puzzleDimensions.x;
        
        // The position of the piece in the puzzle
        Vector2 targetPosition = new((-width * puzzleDimensions.x / 2) + (width * col) + (width/2),
                                     (-height * puzzleDimensions.y / 2) + (height * row) + (height/2));
        
        // Check if we are in the correct position
        if (Vector2.Distance(selectedPiece.transform.localPosition, targetPosition) < (width / 2))
        {
            selectedPiece.transform.localPosition = targetPosition;
            
            // Disable the collider so we can't move the piece anymore
            selectedPiece.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}