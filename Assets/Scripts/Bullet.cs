using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Variables

    // adjustable variable
    [SerializeField] private float speed;
    
    // private variables
    private Renderer body;
    private Rigidbody rb;
    private float distance;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        body = GetComponent<Renderer>();
    }

    private void Update()
    {
        distance = Vector3.Distance(this.gameObject.transform.position, Player.position);
        if (distance < Detection.maxRange) return;
        Despawn();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Enemy")) return;
        Despawn();
    }

    #endregion

    private void ChangeColor()
    {
        body.material = Player.color;
    }

    #region Public Methods
    public bool isActive
    {
        get { return gameObject.activeSelf; }
    }

    public void Spawn()
    {
        gameObject.SetActive(true);
        ChangeColor();
    }

    public void Despawn()
    {
        gameObject.SetActive(false);
    }

    public void Shoot(Vector3 trajectory)
    {
        rb.velocity = trajectory * speed;
    }
    #endregion
}
