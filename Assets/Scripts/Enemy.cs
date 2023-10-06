using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    #region Variables

    // public variables
    private static List<Enemy> enemies = new List<Enemy>();
    public static List<Enemy> GetEnemies() { return enemies; }

    // adjustable variables
    [SerializeField] private Material[] colors;
    [SerializeField] private float speed;
    [SerializeField] private float special;

    // private variables
    private Vector3 lastOrientation;
    private Renderer body;
    private int randomIndex;
    private static int lastIndex;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        body = GetComponent<Renderer>();
        lastIndex = 0;
    }

    private void FixedUpdate()
    {
        FacePlayer();
        WalkTowardsPlayer();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (body.material == null) return;
        if (other == null) return;
        Death(other);
        Gameover(other);
    }

    #endregion

    #region Private Methods

    private void FacePlayer()
    {
        Vector3 targetOrientation = Player.position - transform.position;
        if (targetOrientation == lastOrientation) return;
        transform.rotation = Quaternion.LookRotation(targetOrientation);
        lastOrientation = targetOrientation;
    }

    private void WalkTowardsPlayer()
    {
        float motion = speed * Time.fixedDeltaTime;
        transform.position = Vector3.MoveTowards(transform.position, Player.position, motion);
    }

    private void Gameover(Collision other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        Detection.ResetClosest();
        SceneManager.LoadScene(2);
    }

    private void Death(Collision other)
    {
        if (!other.gameObject.CompareTag("Armament")) return;
        other.gameObject.TryGetComponent<Renderer>(out Renderer bullet);
        if (bullet.material.color != body.material.color) return;
        ScoreManager.score++;
        Despawn();
    }

    #endregion

    #region Public Methods
    public bool isActive
    {
        get { return gameObject.activeSelf; }
    }

    public void SetColor()
    {
        do { randomIndex = Random.Range(0, colors.Length); }
        while (randomIndex == lastIndex);
        body.material = colors[randomIndex];
        lastIndex = randomIndex;
    }

    public void Spawn()
    {
        enemies.Add(this);
        gameObject.SetActive(true);
    }

    public void Despawn()
    {
        enemies.Remove(this);
        Detection.ResetClosest();
        gameObject.SetActive(false);
    }
    #endregion

    #region Special Method

    public void SpecialOne()
    {
        StartCoroutine(SpecialSequence());
    }

    IEnumerator SpecialSequence()
    {
        while (true)
        {
            SetColor();
            yield return new WaitForSeconds(special);
        }
    }

    #endregion
}
