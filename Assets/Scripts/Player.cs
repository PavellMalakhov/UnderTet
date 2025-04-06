using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private PlayingFieldView _playingFieldView;
    [SerializeField] private PlayingField _playingField;
    [SerializeField] private ScrollerCamera _scrollerCamera;

    private void FixedUpdate()
    {
        if (_inputReader.GetSelect() && _playingField.IsCoursePlayer)
        {
            _playingFieldView.SelectBlock();
        }

        if (_inputReader.GetMoved() && _playingField.IsCoursePlayer)
        {
            _playingFieldView.MoveBlock();
        }
    }
}
