using UnityEngine;
using TMPro;

public class WaveUIObserver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI counterText;

    // for tomorrow's wave system
    public void UpdateEnemyCount(int remaining)
    {
        counterText.text = $"Enemies Left: {remaining}";
    }
}