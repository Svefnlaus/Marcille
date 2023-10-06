using UnityEngine;

public class Detection : MonoBehaviour
{
    #region Variables

    // public static method
    public static void ResetClosest() { shortestDistance = maxRange; }

    // adjustable variables
    [SerializeField] private Player player;
    [Space]
    [Header("Detection Ring Colors")]
    [SerializeField] private Material clear;
    [SerializeField] private Material detected;
    [Space]
    [Header("Detection Ring Size")]
    [SerializeField] private int lines;
    [SerializeField] private float radius;

    // static variables
    public static float maxRange;
    private static float shortestDistance;


    // private variable
    private LineRenderer circleRenderer;
    private bool isEnemyDetected;
    private float range;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        Enemy.GetEnemies().Clear();
        range = radius;
        maxRange = range + 3;
        circleRenderer = GetComponent<LineRenderer>();
        circleRenderer.material = clear;
        ResetClosest();
        DrawCircle();
    }

    private void Update()
    {
        FindClosestEnemy();
    }

    #endregion

    #region Private Methods

    private void FindClosestEnemy()
    {
        foreach (Enemy enemy in Enemy.GetEnemies())
        {
            if (enemy == null) return;

            float currentDistance = Vector3.Distance(transform.position, enemy.transform.position);

            shortestDistance = (shortestDistance > currentDistance) ? currentDistance : shortestDistance;
            isEnemyDetected = (shortestDistance <= range) ? true : false;
            circleRenderer.material = isEnemyDetected ? detected : clear;

            if (!isEnemyDetected || currentDistance != shortestDistance) return;
            player.FaceEnemy(enemy.transform);
        }
    }

    private void DrawCircle()
    {
        circleRenderer.positionCount = lines;
        for (int currentPoint = 0; currentPoint < lines; currentPoint++)
        {
            float thetaScale = (float)currentPoint / lines;
            float theta = thetaScale * 2 * Mathf.PI;

            float x = Mathf.Cos(theta) * radius;
            float y = Mathf.Sin(theta) * radius;

            Vector3 nextPoint = new Vector3(x, y, 0);

            circleRenderer.SetPosition(currentPoint, nextPoint);
        }
    }

    #endregion
}
