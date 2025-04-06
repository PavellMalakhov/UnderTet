using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayingFieldView : MonoBehaviour
{
    [SerializeField] private PlayingField _playingField;
    [SerializeField] private Block1 _block1;
    [SerializeField] private Block2 _block2;
    [SerializeField] private Block3 _block3;
    [SerializeField] private Block4 _block4;
    [SerializeField] private GameObject _cellsParrent;
    [SerializeField] private Cell _cell;
    [SerializeField] private Block _blockPreview;
    
    private List<Block> _blocks = new List<Block>();
    private List<Cell> _cells = new List<Cell>();
    private int _currentCursorPositionX;
    private int _currentCursorPositionY;
    private int _cursorPositionX;
    private int _cursorPositionY;
    private int _targetCursorPositionX;
    private int _indexCellBlock;

    private void OnEnable()
    {
        _playingField.GameFieldChanged += ShowBlock;
    }

    private void OnDisable()
    {
        _playingField.GameFieldChanged -= ShowBlock;

        foreach (var item in _cells)
        {
            item.CursorEntered -= SetPositionCursor;
        }
    }

    private void Awake()
    {
        for (int i = 0; i < _playingField.GetWidthGameField(); i++)
        {
            for (int j = 1; j < _playingField.GetHeightGameField(); j++)
            {
                _cells.Add(Instantiate(_cell, new Vector3(i, j, 10), Quaternion.identity, _cellsParrent.transform));

                _cells[_cells.Count - 1].CursorEntered += SetPositionCursor;
            }
        }
    }

    public void SelectBlock()
    {
        _cursorPositionX = _currentCursorPositionX;
        _cursorPositionY = _currentCursorPositionY;

        ViewBlock(_cursorPositionX, _cursorPositionY);

        _indexCellBlock = _playingField.GetIndexCellBlock(_cursorPositionX, _cursorPositionY);
    }

    public void MoveBlock()
    {
        _targetCursorPositionX = _currentCursorPositionX;

        if (_cursorPositionX != _targetCursorPositionX)
        {  
            _playingField.MoveBlock(_cursorPositionX, _cursorPositionY, _targetCursorPositionX);
        }

        if (!_playingField.IsCoursePlayer)
        {
            StartCoroutine(_playingField.GameCourse());
        }

        if (_blockPreview != null)
        {
            Destroy(_blockPreview.gameObject);
        }
    }

    private void Update()
    {
        if (_blockPreview != null)
        {
            _blockPreview.transform.position = new Vector3(_currentCursorPositionX - _indexCellBlock, _cursorPositionY, 0);
        }
    }

    public void ViewBlock(int positionX0, int positionY0)
    {
        if (_playingField.GetLengthBlock(positionX0, positionY0) == 1)
        {
            _blockPreview = Instantiate(_block1);
        }

        if (_playingField.GetLengthBlock(positionX0, positionY0) == 2)
        {
            _blockPreview = Instantiate(_block2);
        }

        if (_playingField.GetLengthBlock(positionX0, positionY0) == 3)
        {
            _blockPreview = Instantiate(_block3);
        }

        if (_playingField.GetLengthBlock(positionX0, positionY0) == 4)
        {
            _blockPreview = Instantiate(_block4);
        }
    }

    private void SetPositionCursor(Vector3 position)
    {
        _currentCursorPositionX = Convert.ToInt32(position.x);

        _currentCursorPositionY = Convert.ToInt32(position.y);
    }

    private void ShowBlock(int[,,] gameField)
    {
        foreach (var block in _blocks)
        {
            Destroy(block.gameObject);
        }

        _blocks.Clear();

        for (int i = 0; i < gameField.GetLength(1); i++)
        {
            for (int j = 0; j < gameField.GetLength(0); j++)
            {
                int lengthBlock = gameField[j, i, 0];

                if (gameField[j, i, 0] == 1)
                {
                    _blocks.Add(Instantiate(_block1, new Vector3(j, i), Quaternion.identity));
                    _blocks[_blocks.Count - 1].Init(_playingField);
                    j += lengthBlock - 1;
                }

                if (gameField[j, i, 0] == 2)
                {
                    _blocks.Add(Instantiate(_block2, new Vector3(j, i), Quaternion.identity));
                    _blocks[_blocks.Count - 1].Init(_playingField);
                    j += lengthBlock - 1;
                }

                if (gameField[j, i, 0] == 3)
                {
                    _blocks.Add(Instantiate(_block3, new Vector3(j, i), Quaternion.identity));
                    _blocks[_blocks.Count - 1].Init(_playingField);
                    j += lengthBlock - 1;
                }

                if (gameField[j, i, 0] == 4)
                {
                    _blocks.Add(Instantiate(_block4, new Vector3(j, i), Quaternion.identity));
                    _blocks[_blocks.Count - 1].Init(_playingField);
                    j += lengthBlock - 1;
                }
            }
        }
    }
}
