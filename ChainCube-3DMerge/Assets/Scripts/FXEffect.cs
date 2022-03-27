using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class FXEffect : MonoBehaviour
{
    private ParticleSystem.MainModule _explosionFXModule;
    private ParticleSystem _explosionFX;

    public static FXEffect Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _explosionFX = GetComponent<ParticleSystem>();
        _explosionFXModule = _explosionFX.main;
    }

    public void PlayCubeExplosionFX(Vector3 position, Color color)
    {
        _explosionFXModule.startColor = new ParticleSystem.MinMaxGradient(color);

        _explosionFX.transform.position = position;

        _explosionFX.Play();
    }
}
