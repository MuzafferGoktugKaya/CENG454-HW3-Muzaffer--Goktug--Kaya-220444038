using UnityEngine;
using TMPro;

public class WaveUIObserver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI counterText;

    private void OnEnable()
    {
        WaveManager.OnWaveChanged += UpdateWaveDisplay;
    }

    private void OnDisable()
    {
        WaveManager.OnWaveChanged -= UpdateWaveDisplay;
    }

    private void UpdateWaveDisplay(int currentWaveNumber)
    {
        if (counterText != null)
        {
            counterText.text = $"Wave: {currentWaveNumber}";
        }
    }
}