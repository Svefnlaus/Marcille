using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variables

    // public variables
    public static Vector3 position;
    public static Material color;

    // adjustable variable
    [SerializeField] private Material[] colors;

    // private variables
    private Vector3 lastOrientation;
    private Renderer body;
    private int index;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        ScoreManager.score = 0;
        index = 0;
        body = GetComponent<Renderer>();
        position = this.transform.position;
        ChangeColor();
    }

    private void OnMouseDown()
    {
        ChangeColor();
    }

    #endregion

    private void ChangeColor()
    {
        body.material = colors[index];
        color = body.material;
        index++;
        if (index == colors.Length) { index = 0; }
    }

    public void FaceEnemy(Transform enemy)
    {
        if (enemy.position == lastOrientation) return;
        transform.LookAt(enemy.position);
        lastOrientation = enemy.position;
    }
}
