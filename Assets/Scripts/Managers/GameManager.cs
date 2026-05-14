using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Defense Settings")]
    [SerializeField] private int spaceDamage = 50;
    [SerializeField] private float shieldRadius = 7f;

    [Header("UI Panels")]
    public GameObject winPanel;
    public GameObject gameOverPanel;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            ActivateDefensePulse();
        }
    }

    void ActivateDefensePulse()
    {
        Debug.Log("Space Bar Pressed: Defense Pulse Activated!");

        CoreController core = UnityEngine.Object.FindFirstObjectByType<CoreController>();
        if (core == null) return;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            // mesafe hesaplama
            float distance = Vector3.Distance(core.transform.position, enemy.transform.position);

            // sadece düşman yakındayken kalkan çalışşın
            if (distance <= shieldRadius)
            {
                BasicEnemy enemyScript = enemy.GetComponent<BasicEnemy>();
                if (enemyScript != null && enemy.activeInHierarchy)
                {
                    enemyScript.TakeDamage(spaceDamage);
                }
            }
        }
    }

    public void LevelComplete() { winPanel.SetActive(true); Time.timeScale = 0; }
    public void GameOver() { gameOverPanel.SetActive(true); Time.timeScale = 0; }
}