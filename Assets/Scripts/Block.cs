using System;
using UnityEngine;

public class Block : MonoBehaviour
{
    private PlayingField _playingField;
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Material _material1;
    [SerializeField] private Material _material2;
    [SerializeField] private Material _material3;
    [SerializeField] private Material _material4;

    private int _indexColor;

    public void Init(PlayingField playingField)
    {
        _playingField = playingField;

        _indexColor = _playingField.GetColorBlock(Convert.ToInt32(transform.position.x), Convert.ToInt32(transform.position.y));

        switch (_indexColor)
        {
            case 1:
                _meshRenderer.material = _material1;
                break;

            case 2:
                _meshRenderer.material = _material2;
                break;

            case 3:
                _meshRenderer.material = _material3;
                break;

            case 4:
                _meshRenderer.material = _material4;
                break;
        }
    }

    public void Init(int indexColor)
    {
        switch (indexColor)
        {
            case 1:
                _meshRenderer.material = _material1;
                break;

            case 2:
                _meshRenderer.material = _material2;
                break;

            case 3:
                _meshRenderer.material = _material3;
                break;

            case 4:
                _meshRenderer.material = _material4;
                break;
        }
    }
}
