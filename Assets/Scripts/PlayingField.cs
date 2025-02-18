using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class PlayingField : MonoBehaviour
{
    [SerializeField] private BlockGenerator _blockGenerator;
    [SerializeField] private RecordsSaver _recordsSaver;
    [SerializeField] private TextMeshProUGUI _textlengthsBloks;
    [SerializeField] private TextMeshProUGUI _textColorsBloks;

    [SerializeField] private int positionX0;
    [SerializeField] private int positionY0;
    [SerializeField] private int positionX;

    private readonly int[,,] _gameField = new int[8, 20, 3];

    public int[,,] GameField => _gameField;
    private int[,] _delitRow;
    private bool _isCourseFinished = false;

    public event Action<int[,,]> GameFieldChanged;

    public bool IsCoursePlayer { get; private set; }

    public void Start()
    {
        int[,] rowPreview = _blockGenerator.GenerateRow();

        for (int i = 0; i < GetWidthGameField(); i++)
        {
            for (int j = 0; j < GetCountParametersCell(); j++)
            {
                _gameField[i, 0, j] = rowPreview[i, j];
            }
        }

        StartCoroutine(RaiseBlocks());
    }

    public void MoveBlock(int positionX0, int positionY0, int positionX)
    {
        int lengthBlock = _gameField[positionX0, positionY0, 0];

        positionX -= GetIndexCellBlock(positionX0, positionY0);
        
        positionX0 -= GetIndexCellBlock(positionX0, positionY0);

        for (int i = 0; i < GetWidthGameField(); i++)
        {
            if (_gameField[i, positionY0, 0] == lengthBlock)
            {
                for (int j = 0; j < lengthBlock; j++)
                {
                    if (i + j == positionX0)
                    {
                        if ((i - 1) >= 0 && positionX < positionX0 && _gameField[i - 1, positionY0, 0] == 0)
                        {
                            for (int k = 0; k < GetCountParametersCell(); k++)
                            {
                                _gameField[i - 1, positionY0, k] = _gameField[i, positionY0, k];

                                _gameField[i + lengthBlock - 1, positionY0, k] = 0;
                            }

                            positionX0--;

                            if (positionX0 != positionX)
                            {
                                MoveBlock(positionX0, positionY0, positionX);
                            }

                            _isCourseFinished = false;
                            IsCoursePlayer = false;

                            break;
                        }

                        if (positionX > positionX0 && (i + lengthBlock + 1) <= GetWidthGameField()  && _gameField[i + lengthBlock, positionY0, 0] == 0)
                        {
                            for (int k = 0; k < GetCountParametersCell(); k++)
                            {
                                _gameField[i + lengthBlock, positionY0, k] = _gameField[i, positionY0, k];

                                _gameField[i, positionY0, k] = 0;
                            }

                            positionX0++;

                            if (positionX < positionX0)
                            {
                                MoveBlock(positionX0, positionY0, positionX);
                            }

                            _isCourseFinished = false;
                            IsCoursePlayer = false;

                            break;
                        }
                    }
                }
            }
        }

        positionX0 = 0;
        positionY0 = 0;
        positionX = 0;
    }

    public void SkipMove()
    {
        StartCoroutine(RaiseBlocks());
    }

    public IEnumerator GameCourse()
    {
        var wait = new WaitForSeconds(0.5f);

        Print();
        yield return wait;

        StartCoroutine(FallDownBlocks());
        Print();
        yield return wait;
    }

    private void Print()
    {
        //_textlengthsBloks.text = "";
        //_textColorsBloks.text = "";

        //for (int i = GetHeightGameField() - 1; i >= 0; i--)
        //{
        //    for (int j = 0; j < GetWidthGameField(); j++)
        //    {
        //        _textlengthsBloks.text += Convert.ToString(_gameField[j, i, 0]);
        //        _textColorsBloks.text += Convert.ToString(_gameField[j, i, 1]);
        //    }

        //    _textlengthsBloks.text += "\n";
        //    _textColorsBloks.text += "\n";
        //}

        GameFieldChanged.Invoke(GameField);
    }

    private IEnumerator DeleteRow()
    {
        var wait = new WaitForSeconds(0.5f);

        yield return wait;

        bool wasDeletion = false;

        _delitRow = new int[_gameField.GetLength(0), _gameField.GetLength(2) - 1];

        for (int i = 1; i < GetHeightGameField(); i++)
        {
            bool isRowFull = true;
            
            for (int j = 0; j < GetWidthGameField(); j++)
            {
                if (_gameField[j, i, 0] == 0)
                {
                    isRowFull = false;
                }
            }

            if (isRowFull == true)
            {
                for (int j = 0; j < GetWidthGameField(); j++)
                {
                    _delitRow[j, 0] = _gameField[j, i, 0];
                    _delitRow[j, 1] = _gameField[j, i, 1];

                    for (int k = 0; k < GetCountParametersCell(); k++)
                    {
                        _gameField[j, i, k] = 0;
                    }
                }

                wasDeletion = true;

                _recordsSaver.SaveRecord(_delitRow);

                StartCoroutine(FallDownBlocks());
            }
        }

        Print();

        if (wasDeletion == false && _isCourseFinished == false)
        {
            StartCoroutine(RaiseBlocks());

            _isCourseFinished = true;
        }
        else
        {
            IsCoursePlayer = true;
        }
    }

    private IEnumerator FallDownBlocks()
    {
        var wait = new WaitForSeconds(0.5f);

        yield return wait;

        for (int l = 0; l < GetHeightGameField(); l++)
        {
            int indexSecondRow = 2;

            for (int i = indexSecondRow; i < GetHeightGameField(); i++)
            {
                for (int j = 0; j < GetWidthGameField(); j++)
                {
                    if (_gameField[j, i, 0] == 0)
                    {
                        continue;
                    }
                    else
                    {
                        int lengthBlock = _gameField[j, i, 0];
                        int amountEmpty = 0;

                        for (int k = 0; k < lengthBlock; k++)
                        {
                            if (_gameField[j + k, i - 1, 0] == 0)
                            {
                                amountEmpty++;
                            }
                        }

                        if (amountEmpty == lengthBlock)
                        {
                            for (int k = 0; k < lengthBlock; k++)
                            {
                                for (int s = 0; s < GetCountParametersCell(); s++)
                                {
                                    _gameField[j + k, i - 1, s] = _gameField[j + k, i, s];
                                }
                            }

                            for (int k = 0; k < lengthBlock; k++)
                            {
                                for (int s = 0; s < GetCountParametersCell(); s++)
                                {
                                    _gameField[j + k, i, s] = 0;
                                }
                            }
                        }

                        j += (lengthBlock - 1);
                    }
                }
            }
        }

        StartCoroutine(DeleteRow());

        Print();
    }

    private IEnumerator RaiseBlocks()
    {
        var wait = new WaitForSeconds(0.5f);

        yield return wait;

        int[,] rowPreview = _blockGenerator.GenerateRow();

        for (int i = GetHeightGameField() - 2; i >= 0; i--)
        {
            for (int j = 0; j < GetWidthGameField(); j++)
            {
                for (int k = 0; k < GetCountParametersCell(); k++)
                {
                    _gameField[j, i + 1, k] = _gameField[j, i, k];
                }
            }
        }

        for (int i = 0; i < GetWidthGameField(); i++)
        {
            for (int j = 0; j < GetCountParametersCell(); j++)
            {
                _gameField[i, 0, j] = rowPreview[i, j];
            }
        }

        StartCoroutine(FallDownBlocks());

        Print();
    }

    public int GetWidthGameField()
    {
        return _gameField.GetLength(0);
    }

    public int GetHeightGameField()
    {
        return _gameField.GetLength(1);
    }

    public int GetCountParametersCell()
    {
        return _gameField.GetLength(2);
    }

    public int GetLengthBlock(int positionX0, int positionY0)
    {
        return _gameField[positionX0, positionY0, 0];
    }

    public int GetColorBlock(int positionX0, int positionY0)
    {
        return _gameField[positionX0, positionY0, 1];
    }

    public int GetIndexCellBlock(int positionX0, int positionY0)
    {
        int lengthBlock = _gameField[positionX0, positionY0, 0];

        for (int i = 0; i < GetWidthGameField(); i++)
        {
            if (_gameField[i, positionY0, 0] == lengthBlock)
            {
                for (int j = 0; j < lengthBlock; j++)
                {
                    if (i + j == positionX0)
                    {
                        return j;
                    }
                }

                if (lengthBlock > 0)
                {
                    i += lengthBlock - 1;
                }
            }
        }

        return 0;
    }
}
