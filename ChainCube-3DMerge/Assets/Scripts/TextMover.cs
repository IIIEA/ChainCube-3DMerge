using UnityEngine;

public class TextMover : MonoBehaviour
{
    [Header("For change direction(-up/+down)")]
    [SerializeField] private float _speed;

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
    }
}
