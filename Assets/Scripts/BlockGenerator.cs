using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    [SerializeField] private PlayingField _playingField;

    private int _countColors = 4;
    private int _blockLengthMax = 4;
    private int _amountBlockInRowMax;

    public int[,] GenerateRow()
    {
        _amountBlockInRowMax = _playingField.GetWidthGameField();

        int lengthBlocksInRow = 0;

        List<int> row = new List<int>();
        List<int> rowColor = new List<int>();

        int[,] blocksInRow;

        do
        {
            int amountBlock = Random.Range(1, _amountBlockInRowMax + 1);

            blocksInRow = new int[amountBlock, _playingField.GetCountParametersCell()];

            lengthBlocksInRow = 0;

            for (int i = 0; i < blocksInRow.GetLength(0); i++)
            {
                int blockLength = Random.Range(1, _blockLengthMax + 1);
                int blockColor = Random.Range(1, _countColors + 1);

                blocksInRow[i, 0] = blockLength;
                blocksInRow[i, 1] = blockColor;

                lengthBlocksInRow += blockLength;
            }

        } while (lengthBlocksInRow >= _playingField.GetWidthGameField());

        int amountEmptyCellInRow = _playingField.GetWidthGameField() - lengthBlocksInRow;

        for (int i = 0; i < blocksInRow.GetLength(0); i++)
        {
            for (int j = 0; j < amountEmptyCellInRow; j++)
            {
                if (Util.GetRandomBoolean())
                {
                    row.Add(0);
                    rowColor.Add(0);
                    amountEmptyCellInRow--;
                }
            }
            
            for (int k = 0; k < blocksInRow[i, 0]; k++)
            {
                row.Add(blocksInRow[i, 0]);
                rowColor.Add(blocksInRow[i, 1]);
            }
        }

        for (int i = row.Count; i < _playingField.GetWidthGameField(); i++)
        {
            row.Add(0);
            rowColor.Add(0);
        }

        int[,] rowPreview = new int[_playingField.GetWidthGameField(), _playingField.GetCountParametersCell()];

        for (int i = 0; i < rowPreview.GetLength(0); i++)
        {
            rowPreview[i, 0] = row[i];
            rowPreview[i, 1] = rowColor[i];
        }

        return rowPreview;
    } 
}
