using UnityEngine;

public class CoreAudioObserver : MonoBehaviour
{
    [SerializeField] private AudioSource warningAudio;
    [SerializeField] private float warningThreshold = 0.3f;

    private void OnEnable()
    {
        CoreController.OnCoreHealthChanged += CheckHealthForWarning;
    }

    private void OnDisable()
    {
        CoreController.OnCoreHealthChanged -= CheckHealthForWarning;
    }

    private void CheckHealthForWarning(float healthPercent)
{
    if (healthPercent > 0.01f && healthPercent <= warningThreshold && !warningAudio.isPlaying)
    {
        warningAudio.Play();
    }
    
    else if (healthPercent > warningThreshold || healthPercent <= 0)
    {
        warningAudio.Stop();
    }
}
}
//test test