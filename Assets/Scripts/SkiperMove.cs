using UnityEngine;

public class SkiperMove : MonoBehaviour
{
    [SerializeField] private PlayingField _playingField;
    
    public void SkipMove()
    {
        _playingField.SkipMove();
    }
}
