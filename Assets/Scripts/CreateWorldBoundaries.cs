using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWorldBoundaries : MonoBehaviour
{
    public TerrainGenerationSettings generationSettings;

    public int boundariesThickness = 200;
    public int boundaryTopOffset = 20;
    public int boundaryRedMinVisibility = 5;
    public int boundaryRedMaxVisibility = 25;


    public GameObject boundaryTop;
    public GameObject boundaryRight;
    public GameObject boundaryLeft;
    public GameObject lavaBottom;
    public GameObject boundaryCannotPlaceBlocks;

    // TODO: remake wall moving
    [Header("Fast Decisition (Remake it in future)")]
    public float boundariesMoveSpeed = 2f;
    public float boundariesMinPosition = 60f;

    private GameObject player;
    private SpriteRenderer cannotPlaceBlocksRenderer;

    private float distance;

    // TODO: remake wall moving
    private float boundariesMinPositionLeft = 0;
    private float boundariesMinPositionRight = 0;

    private void Start()
    {
        cannotPlaceBlocksRenderer = boundaryCannotPlaceBlocks.GetComponent<SpriteRenderer>();

        float x = 0;
        float y = 0;

        // Top
        x = generationSettings.WorldSizeWidth / 2;
        y = boundariesThickness / 2 + generationSettings.WorldSizeHeight + boundaryTopOffset;

        SetBoundarySize(boundaryTop, x, y, generationSettings.WorldSizeWidth, boundariesThickness);

        // Right
        x = boundariesThickness / 2 + generationSettings.WorldSizeWidth;
        y = generationSettings.WorldSizeHeight / 2 + boundariesThickness / 2 + boundaryTopOffset;

        SetBoundarySize(boundaryRight, x, y,
            boundariesThickness, generationSettings.WorldSizeHeight + boundariesThickness + boundaryTopOffset * 2);

        boundariesMinPositionRight = x - boundariesMinPosition;

        // Left
        x = -boundariesThickness / 2;
        y = generationSettings.WorldSizeHeight / 2 + boundariesThickness / 2 + boundaryTopOffset;

        SetBoundarySize(boundaryLeft, x, y,
            boundariesThickness, generationSettings.WorldSizeHeight + boundariesThickness + boundaryTopOffset * 2);

        boundariesMinPositionLeft = x + boundariesMinPosition;

        // Bottom Lava
        x = generationSettings.WorldSizeWidth / 2;
        y = -boundariesThickness / 2;

        SetBoundarySize(lavaBottom, x, y,
            generationSettings.WorldSizeWidth + boundariesThickness * 2, boundariesThickness);

        // Cannot Place Block Here Boundary
        x = generationSettings.WorldSizeWidth / 2;
        y = generationSettings.WorldSizeHeight + boundaryTopOffset / 2;

        boundaryCannotPlaceBlocks.transform.position = new Vector3(x, y, 0);
        boundaryCannotPlaceBlocks.transform.localScale = new Vector3(generationSettings.WorldSizeWidth, boundaryTopOffset, 1);
    }

    private void SetBoundarySize(GameObject boundary, float x, float y, float width, float height)
    {
        SpriteRenderer spriteRenderer = boundary.GetComponent<SpriteRenderer>();
        BoxCollider2D boxCollider = boundary.GetComponent<BoxCollider2D>();

        // TODO: make another variable for this
        spriteRenderer.size = new Vector2(width, height);
        boxCollider.size = new Vector2(width, height);

        boundary.transform.position = new Vector3(x, y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (player != null)
        {
            Transform boundary = boundaryCannotPlaceBlocks.transform;
            Vector3 playerPos = player.transform.position;

            float dy = boundary.position.y - boundary.localScale.y / 2 - playerPos.y;

            //float dx = Mathf.Max(boundary.position.x - playerPos.x, 0, playerPos.x - (boundary.position.x + boundary.localScale.x));
            //float dy = Mathf.Max(boundary.position.y - playerPos.y, 0, playerPos.y - (boundary.position.y + boundary.localScale.y));

            distance = (boundaryRedMaxVisibility - dy) / (boundaryRedMaxVisibility - boundaryRedMinVisibility);
            distance = Mathf.Clamp(distance, 0.0001f, 1f);

            cannotPlaceBlocksRenderer.color = new Color(
                cannotPlaceBlocksRenderer.color.r, cannotPlaceBlocksRenderer.color.g, cannotPlaceBlocksRenderer.color.b,
                distance - 0.5f);
        }

        // TODO: remake it. Boundaries moving left and right
        if (boundaryRight.transform.position.x > boundariesMinPositionRight)
        {
            boundaryRight.transform.position += Vector3.left * boundariesMoveSpeed * Time.deltaTime;
        }

        if (boundaryLeft.transform.position.x < boundariesMinPositionLeft)
        {
            boundaryLeft.transform.position += Vector3.right * boundariesMoveSpeed * Time.deltaTime;
        }
    }
}
