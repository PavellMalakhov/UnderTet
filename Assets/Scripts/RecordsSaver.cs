using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class RecordsSaver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textScore;
    [SerializeField] private TextMeshProUGUI _textlog;

    [SerializeField] private Block1 _block1;
    [SerializeField] private Block2 _block2;
    [SerializeField] private Block3 _block3;
    [SerializeField] private Block4 _block4;

    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Material _material1;
    [SerializeField] private Material _material2;
    [SerializeField] private Material _material3;
    [SerializeField] private Material _material4;

    [SerializeField] private GameObject _parentPair;
    [SerializeField] private GameObject _parentSet;
    [SerializeField] private GameObject _parentCare;
    [SerializeField] private GameObject _parentFullHouse;
    [SerializeField] private GameObject _parentDoubleSet;
    [SerializeField] private GameObject _parentSetCare;
    [SerializeField] private GameObject _parentFlush;
    [SerializeField] private GameObject _parentStraight;
    [SerializeField] private GameObject _parentFlushRoyal;

    [SerializeField] private TextMeshProUGUI _textPair;
    [SerializeField] private TextMeshProUGUI _textSet;
    [SerializeField] private TextMeshProUGUI _textCare;
    [SerializeField] private TextMeshProUGUI _textFullHouse;
    [SerializeField] private TextMeshProUGUI _textDoubleSet;
    [SerializeField] private TextMeshProUGUI _textSetCare;
    [SerializeField] private TextMeshProUGUI _textFlush;
    [SerializeField] private TextMeshProUGUI _textStraight;
    [SerializeField] private TextMeshProUGUI _textFlushRoyal;

    private int[,] _delitRow;

    private Dictionary<GameObject, List<Block>> _blocks = new Dictionary<GameObject, List<Block>>();

    private int _score = 0;
    private int _pair = 0;
    private int _set = 0;
    private int _care = 0;
    private int _fullHouse = 0;
    private int _doubleSet = 0;
    private int _setCare = 0;
    private int _flush = 0;
    private int _straight = 0;
    private int _flushRoyal = 0;

    private string _stringScore = "Score:";
    private string _stringStraight = "Straight:";
    private string _stringFlush = "Flush:";
    private string _stringPair = "Pair:";
    private string _stringSet = "Set:";
    private string _stringCare = "Care:";
    private string _stringFullHouse = "FullHouse:";
    private string _stringDoubleSet = "DoubleSet:";
    private string _stringSetCare = "SetCare:";
    private string _stringFlushRoyal = "FlushRoyal:";

    //private void OnEnable()
    //{
    //    int[,] delitRow = new int[8, 2] { { 1, 4 }, { 1, 4 }, { 1, 4 }, { 1, 4 }, { 1, 4 }, { 1, 4 }, { 1, 4 }, { 1, 4 } };

    //    SaveRecord(delitRow);
    //}

    public void SaveRecord(int[,] delitRow)
    {
        _score++;
        _textScore.text = $"{_stringScore} {_score}";
        
        _delitRow = new int[delitRow.GetLength(0), delitRow.GetLength(1)];

        for (int i = 0; i < delitRow.GetLength(0); i++)
        {
            for (int j = 0; j < delitRow.GetLength(1); j++)
            {
                _delitRow[i, j] = delitRow[i, j];
            }
        }
        
        int[,] row;
        int amountBlocksInRow = 0;

        for (int i = 0; i < delitRow.GetLength(0); i++)
        {
            amountBlocksInRow++;
            i += delitRow[i, 0] - 1;
        }

        row = new int[amountBlocksInRow, 2];

        for (int k = 0, j = 0; k < row.GetLength(0); k++, j++)
        {
            row[k, 0] = delitRow[j, 0];
            row[k, 1] = delitRow[j, 1];

            j += delitRow[j, 0] - 1;
        }

        bool isStraight = true;

        if (row.GetLength(0) == 4)
        {
            for (int i = 0; i < row.GetLength(0); i++)
            {
                for (int j = i + 1; j < row.GetLength(0); j++)
                {
                    if (row[i, 1] == row[j, 1])
                    {
                        isStraight = false;
                    }
                }
            }
        }
        else
        {
            isStraight = false;
        }

        if (isStraight)
        {
            _textlog.text += $"{_stringStraight} {DateTime.Now}\n";
            _straight++;
            _textStraight.text = $"{_stringStraight} {_straight}";
            ShowDeliteRow(_delitRow, _parentStraight.transform.position.y, _parentStraight);
        }

        bool isFlus = true;

        for (int i = 0; i < row.GetLength(0); i++)
        {
            for (int j = i + 1; j < row.GetLength(0); j++)
            {
                if (row[i, 1] != row[j, 1])
                {
                    isFlus = false;
                }
            }
        }

        if (isFlus)
        {
            _textlog.text += $"{_stringFlush} {DateTime.Now}\n";
            _flush++;
            _textFlush.text = $"{_stringFlush} {_flush}";
            ShowDeliteRow(_delitRow, _parentFlush.transform.position.y, _parentFlush);
        }

        int[] array = new int[8];

        for (int i = 0; i < row.GetLength(0); i++)
        {
            int amountMatches = 0;

            for (int j = i + 1; j < row.GetLength(0); j++)
            {
                if (row[i, 0] != 0 && row[i, 0] == row[j, 0] && row[i, 1] == row[j, 1])
                {
                    amountMatches++;
                    row[j, 0] = 0;
                    row[j, 1] = 0;
                }
            }

            array[i] = amountMatches;
        }

        for (int i = 0; i < array.Length; i++)
        {
            switch (array[i])
            {
                case 1:
                    _textlog.text += $"{_stringPair} {DateTime.Now}\n";
                    _pair++;
                    _textPair.text = $"{_stringPair} {_pair}";
                    ShowDeliteRow(_delitRow, _parentPair.transform.position.y, _parentPair);
                    break;

                case 2:
                    _textlog.text += $"{_stringSet} {DateTime.Now}\n";
                    _set++;
                    _textSet.text = $"{_stringSet} {_set}";
                    ShowDeliteRow(_delitRow, _parentSet.transform.position.y, _parentSet);
                    break;

                case 3:
                    _textlog.text += $"{_stringCare} {DateTime.Now}\n";
                    _care++;
                    _textCare.text = $"{_stringCare} {_care}";
                    ShowDeliteRow(_delitRow, _parentCare.transform.position.y, _parentCare);
                    break;

                case 4:
                    _textlog.text += $"\n{_stringFullHouse}\n {DateTime.Now}\n\n";
                    _fullHouse++;
                    _textFullHouse.text = $"{_stringFullHouse} {_fullHouse}";
                    ShowDeliteRow(_delitRow, _parentFullHouse.transform.position.y, _parentFullHouse);
                    break;

                case 5:
                    _textlog.text += $"\n{_stringDoubleSet}\n {DateTime.Now}\n\n";
                    _doubleSet++;
                    _textDoubleSet.text = $"{_stringDoubleSet} {_doubleSet}";
                    ShowDeliteRow(_delitRow, _parentDoubleSet.transform.position.y, _parentDoubleSet);
                    break;

                case 6:
                    _textlog.text += $"\n{_stringSetCare}\n {DateTime.Now}\n\n";
                    _setCare++;
                    _textSetCare.text = $"{_stringSetCare} {_setCare}";
                    ShowDeliteRow(_delitRow, _parentSetCare.transform.position.y, _parentSetCare);
                    break;

                case 7:
                    _textlog.text += $"\n{_stringFlushRoyal}\n {DateTime.Now}\n\n";
                    _flushRoyal++;
                    _textFlushRoyal.text = $"{_stringFlushRoyal} {_flushRoyal}";
                    ShowDeliteRow(_delitRow, _parentFlushRoyal.transform.position.y, _parentFlushRoyal);
                    break;
            }
        }
    }

    private void ShowDeliteRow(int[,] delitRow, float positionY, GameObject parentObject)
    {
        List<Block> blocks = new List<Block>();

        if (_blocks.ContainsKey(parentObject))
        {
            foreach (var block in _blocks[parentObject])
            {
                Destroy(block.gameObject);
            }

            _blocks.Remove(parentObject);
        }

        for (int i = 0; i < delitRow.GetLength(0); i++)
        {
            int lengthBlock = delitRow[i, 0];

            if (delitRow[i, 0] == 1)
            {
                blocks.Add(Instantiate(_block1, new Vector3(i - 12, positionY, 0), Quaternion.identity, parentObject.transform));
                blocks[blocks.Count - 1].Init(delitRow[i, 1]);
                i += lengthBlock - 1;
            }

            if (delitRow[i, 0] == 2)
            {
                blocks.Add(Instantiate(_block2, new Vector3(i - 12, positionY, 0), Quaternion.identity, parentObject.transform));
                blocks[blocks.Count - 1].Init(delitRow[i, 1]);
                i += lengthBlock - 1;
            }

            if (delitRow[i, 0] == 3)
            {
                blocks.Add(Instantiate(_block3, new Vector3(i - 12, positionY, 0), Quaternion.identity, parentObject.transform));
                blocks[blocks.Count - 1].Init(delitRow[i, 1]);
                i += lengthBlock - 1;
            }

            if (delitRow[i, 0] == 4)
            {
                blocks.Add(Instantiate(_block4, new Vector3(i - 12, positionY, 0), Quaternion.identity, parentObject.transform));
                blocks[blocks.Count - 1].Init(delitRow[i, 1]);
                i += lengthBlock - 1;
            }
        }

        _blocks.Add(parentObject, blocks);
    }
}
