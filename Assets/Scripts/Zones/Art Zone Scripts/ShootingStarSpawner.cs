using System.Collections;
using UnityEngine;

public class ShootingStarSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ShootingStar shootingStarPrefab;
    [SerializeField] private RectTransform starLayer;

    [Header("Time Between Groups")]
    [SerializeField] private float minimumSpawnDelay = 2f;
    [SerializeField] private float maximumSpawnDelay = 6f;

    [Header("Stars Per Group")]
    [SerializeField] private int minimumStarsPerGroup = 1;
    [SerializeField] private int maximumStarsPerGroup = 3;

    [Header("Delay Within Group")]
    [SerializeField] private float minimumGroupDelay = 0.05f;
    [SerializeField] private float maximumGroupDelay = 0.3f;

    [Header("Movement")]
    [SerializeField] private float horizontalPadding = 200f;
    [SerializeField] private float minimumDrop = 100f;
    [SerializeField] private float maximumDrop = 350f;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            float spawnDelay = Random.Range(
                minimumSpawnDelay,
                maximumSpawnDelay
            );

            yield return new WaitForSeconds(spawnDelay);

            int starCount = Random.Range(
                minimumStarsPerGroup,
                maximumStarsPerGroup + 1
            );

            for (int i = 0; i < starCount; i++)
            {
                SpawnStar();

                float groupDelay = Random.Range(
                    minimumGroupDelay,
                    maximumGroupDelay
                );

                yield return new WaitForSeconds(groupDelay);
            }
        }
    }

    private void SpawnStar()
    {
        Rect rect = starLayer.rect;

        bool travelLeftToRight = Random.value > 0.5f;

        float startX = travelLeftToRight
            ? rect.xMin - horizontalPadding
            : rect.xMax + horizontalPadding;

        float endX = travelLeftToRight
            ? rect.xMax + horizontalPadding
            : rect.xMin - horizontalPadding;

        float startY = Random.Range(rect.yMin, rect.yMax);
        float drop = Random.Range(minimumDrop, maximumDrop);
        float endY = startY - drop;

        Vector2 startPosition = new Vector2(startX, startY);
        Vector2 endPosition = new Vector2(endX, endY);

        ShootingStar star = Instantiate(
            shootingStarPrefab,
            starLayer
        );

        star.Launch(startPosition, endPosition);
    }
}