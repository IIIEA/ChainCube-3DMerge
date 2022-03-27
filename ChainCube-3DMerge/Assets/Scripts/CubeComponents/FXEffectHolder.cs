using UnityEngine;

[RequireComponent(typeof(CubeRepresentor))]
public class FXEffectHolder : MonoBehaviour
{
    private CubeRepresentor _cubeRepresentor;

    private void Start()
    {
        _cubeRepresentor = GetComponent<CubeRepresentor>();
    }

    private void OnDestroy()
    {
        try
        {
            FXEffect.Instance.PlayCubeExplosionFX(transform.position, _cubeRepresentor.CubeColor);
        }
        catch { }
    }
}
