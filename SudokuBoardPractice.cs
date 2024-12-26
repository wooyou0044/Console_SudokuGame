using System;
using System.Collections.Generic;

namespace SudokuGame
{
    internal class SudokuBoardPractice
    {
        int[,] _sudokuArr;

        public void GenerateSudoku()
        {
            // 레벨 선택 전 임의로 생성
            _sudokuArr = new int[9, 9];
            Random rand = new Random();

            List<int> exceptNum = new List<int>();
            List<int> mustHaveNum = new List<int>();

            List<int> includeNum = new List<int>();
            List<int> tempNumList = new List<int>();


            int centerIndex = (int)Math.Sqrt(_sudokuArr.GetLength(0));
            // 박스 안에 들어갈 최대 숫자
            int maxNum = _sudokuArr.GetLength(0);

            // 채우지 않은 나머지 부분
            int restColumnNum = 0;
            int restRowNum = 0;
            int start = 0;

            // 시작할 인덱스 숫자 변수
            int startIndex = 0;
            // 만들어진 박스 개수
            int boxNum = 0;
            // 만들어진 라인 개수 변수
            int makeLineNum = 0;
            // 검사해야 하는 줄 개수
            int examineLine = 0;
            // 랜덤으로 나올 숫자에서 제외해야할 includeNum 인덱스
            int exceptIndex = 0;
            int nextExamineLine = 0;

            int changeRow = 0;
            int changeColumn = 0;

            int xIndex = 0;
            int yIndex = 0;

            int mustHaveIndex = 0;

            // 세로 줄이 남아있는지 확인
            bool isVerticalLeft = false;

            //// 열, 세로줄
            int columnIndex = rand.Next(0, maxNum);
            // 행, 가로줄
            int rowIndex = rand.Next(0, maxNum);

            while (columnIndex % centerIndex != 0 || rowIndex % centerIndex != 0)
            {
                columnIndex = rand.Next(0, _sudokuArr.GetLength(0));
                rowIndex = rand.Next(0, _sudokuArr.GetLength(1));
            }
            
            // 랜덤한 위치의 3X3에 중복되지 않은 랜덤한 숫자 생성
            for (int i = 0; i < centerIndex; i++)
            {
                for (int j = 0; j < centerIndex; j++)
                {
                    _sudokuArr[i + columnIndex, j + rowIndex] = rand.RandWithoutDuplicat(1, maxNum, exceptNum, ref mustHaveNum);
                    includeNum.Add(_sudokuArr[i + columnIndex, j + rowIndex]);
                }
            }
            // 박스 하나 만들어졌으므로
            boxNum++;

            isVerticalLeft = true; 

            changeColumn = columnIndex;
            changeRow = rowIndex;

            //===========반복 될 아이
            //while(boxNum < maxNum)
            while (boxNum < 5)
            {
                // 박스가 하나 만들어지면 실행
                if (makeLineNum % centerIndex== 0)
                {
                    // 3x3이 하나 만들어지면 반복될 아이
                    restColumnNum = 0;
                    restRowNum = 0;

                    for (int i = 0; i < _sudokuArr.GetLength(0); i++)
                    {
                        // 만들어진 3X3의 남은 세로 줄 개수
                        if (_sudokuArr[i, rowIndex + 2] == 0)
                        {
                            restColumnNum++;
                        }
                        // 만들어진 3X3의 남은 가로 줄 개수
                        if (_sudokuArr[columnIndex + 2, i] == 0)
                        {
                            restRowNum++;
                        }
                    }

                    if (restColumnNum == 0)
                    {
                        isVerticalLeft = false;
                    }
                    else
                    {
                        isVerticalLeft = true;
                    }


                    if (isVerticalLeft)
                    {
                        if (changeColumn <= centerIndex)
                        {
                            changeColumn += centerIndex;
                        }
                        else
                        {
                            changeColumn -= centerIndex;
                        }
                    }

                    else
                    {
                        if (changeRow <= centerIndex)
                        {
                            changeRow += centerIndex;
                        }
                        else
                        {
                            changeRow -= centerIndex;
                        }
                    }

                    if (isVerticalLeft)
                    {
                        if (_sudokuArr[changeColumn, rowIndex] != 0)
                        {
                            if (changeColumn == 3 || changeColumn == centerIndex * 2)
                            {
                                changeColumn = 0;
                            }
                        }
                    }
                    else
                    {
                        if (_sudokuArr[columnIndex, changeRow] != 0)
                        {
                            if (changeRow == 3 || changeRow == centerIndex * 2)
                            {
                                changeRow = 0;
                            }
                        }
                    }
                }

                // 얘는 3X3이 하나 만들어질때까지 반복
                if (makeLineNum < centerIndex)
                {
                    if (!isVerticalLeft)
                    {
                        startIndex = changeRow;
                    }
                    else
                    {
                        startIndex = changeColumn;
                    }


                    // 검사해야 하는 줄 개수
                    examineLine = 0;
                    // 랜덤으로 나올 숫자에서 제외해야할 includeNum 인덱스
                    exceptIndex = 0;
                    nextExamineLine = 0;

                    while (examineLine < 3)
                    {
                        exceptIndex = isVerticalLeft ? (examineLine * centerIndex) + makeLineNum : (makeLineNum * centerIndex) + examineLine;
                        nextExamineLine = isVerticalLeft ? 1 : centerIndex;

                        if (!exceptNum.Contains(includeNum[exceptIndex]))
                        {
                            exceptNum.Add(includeNum[exceptIndex]);
                        }
                        if (makeLineNum >= centerIndex - 2)
                        {
                            if (exceptNum.Contains(includeNum[exceptIndex - nextExamineLine]))
                            {
                                exceptNum.Remove(includeNum[exceptIndex - nextExamineLine]);
                            }
                            if (makeLineNum == centerIndex - 2)
                            {
                                mustHaveNum.Add(includeNum[exceptIndex + nextExamineLine]);
                            }
                        }
                        examineLine++;
                    }

                    if (mustHaveNum.Count > 0)
                    {
                        for (int i = 0; i < centerIndex; i++)
                        {
                            xIndex = isVerticalLeft ? i + startIndex : columnIndex + makeLineNum - 1;
                            yIndex = isVerticalLeft ? rowIndex + makeLineNum - 1 : i + startIndex;
                            if (mustHaveNum.Contains(_sudokuArr[xIndex, yIndex]))
                            {
                                mustHaveNum.Remove(_sudokuArr[xIndex, yIndex]);
                            }
                        }
                    }

                    if (makeLineNum == centerIndex - 1)
                    {
                        for (int i = 0; i < centerIndex; i++)
                        {
                            xIndex = isVerticalLeft ? i + startIndex : columnIndex;
                            yIndex = isVerticalLeft ? rowIndex : i + startIndex;
                            if (!exceptNum.Contains(_sudokuArr[xIndex, yIndex]))
                            {
                                exceptNum.Add(_sudokuArr[xIndex, yIndex]);
                            }

                        }
                    }

                    for (int i = 0; i < centerIndex; i++)
                    {
                        xIndex = isVerticalLeft ? i + startIndex : columnIndex + makeLineNum;
                        yIndex = isVerticalLeft ? rowIndex + makeLineNum : i + startIndex;
                        _sudokuArr[xIndex, yIndex] = rand.RandWithoutDuplicat(1, maxNum, exceptNum, ref mustHaveNum);
                        exceptNum.Add(_sudokuArr[xIndex, yIndex]);
                    }

                    makeLineNum++;
                    startIndex++;
                }

                else
                {
                    if (!isVerticalLeft)
                    {
                        startIndex = changeRow;
                    }
                    else
                    {
                        startIndex = changeColumn;
                    }
                    
                    if (isVerticalLeft)
                    {
                        start = (columnIndex < startIndex) ? 0 : centerIndex;
                    }
                    else
                    {
                        start = (rowIndex < startIndex) ? 0 : centerIndex;
                    }
                    if (startIndex == 0 || startIndex == centerIndex * 2)
                    {
                        for (int i = start; i < centerIndex * 2 + start; i++)
                        {
                            xIndex = isVerticalLeft ? i : columnIndex + makeLineNum - 3;
                            yIndex = isVerticalLeft ? rowIndex + makeLineNum - 3 : i;
                            exceptNum.Add(_sudokuArr[xIndex, yIndex]);
                        }
                        for (int i = 0; i < centerIndex; i++)
                        {
                            xIndex = isVerticalLeft ? i + startIndex : columnIndex + makeLineNum - 3;
                            yIndex = isVerticalLeft ? rowIndex + makeLineNum - 3 : i + startIndex;
                            _sudokuArr[xIndex, yIndex] = rand.RandWithoutDuplicat(1, maxNum, exceptNum, ref mustHaveNum);
                            exceptNum.Add(_sudokuArr[xIndex, yIndex]);
                        }
                        makeLineNum++;
                        exceptNum.Clear();
                    }
                }

                if (makeLineNum == centerIndex)
                {
                    boxNum++;
                    exceptNum.Clear();
                    mustHaveNum.Clear();
                }

                if (makeLineNum == centerIndex * 2)
                {
                    boxNum++;
                    makeLineNum = 0;
                    mustHaveNum.Clear();
                }
            }


            // =========================================================================

            if (columnIndex <= centerIndex)
            {
                columnIndex += centerIndex;
            }
            else
            {
                columnIndex -= centerIndex;
            }

            if (rowIndex <= centerIndex)
            {
                rowIndex += centerIndex;
            }
            else
            {
                rowIndex -= centerIndex;
            }

            changeRow = rowIndex;
            changeColumn = columnIndex;

            // 3x3이 하나 만들어지면 반복될 아이
            restColumnNum = 0;
            restRowNum = 0;

            for (int i = 0; i < _sudokuArr.GetLength(0); i++)
            {
                // 만들어진 3X3의 남은 세로 줄 개수
                if (_sudokuArr[i, rowIndex + 2] == 0)
                {
                    restColumnNum++;
                }
                // 만들어진 3X3의 남은 가로 줄 개수
                if (_sudokuArr[columnIndex + 2, i] == 0)
                {
                    restRowNum++;
                }
            }

            if (restColumnNum == 0)
            {
                isVerticalLeft = false;
            }
            else
            {
                isVerticalLeft = true;
            }

            Console.WriteLine("changeColumn : " + changeColumn);
            Console.WriteLine("changeRow : " + changeRow);







            includeNum.Clear();
            if (changeColumn >= centerIndex)
            {
                // 내 위에 것이 채워져 있다면 위에 것을 includeNum List에 저장
                if (_sudokuArr[changeColumn - 1, changeRow] != 0)
                {
                    for (int i = 0; i < centerIndex; i++)
                    {
                        for (int j = 0; j < centerIndex; j++)
                        {
                            includeNum.Add(_sudokuArr[(changeColumn - centerIndex) + i, changeRow + j]);
                        }
                    }
                }
                else if (_sudokuArr[changeColumn + centerIndex, changeRow] != 0)
                {
                    for (int i = 0; i < centerIndex; i++)
                    {
                        for (int j = 0; j < centerIndex; j++)
                        {
                            includeNum.Add(_sudokuArr[i + changeColumn + centerIndex, changeRow + j]);
                        }
                    }
                }
                if (_sudokuArr[changeColumn - 1, changeRow] == 0)
                {

                }
                else if (_sudokuArr[changeColumn + centerIndex, changeRow] != 0)
                {

                }
            }

            else
            {
                for (int i = 0; i < centerIndex; i++)
                {
                    for (int j = 0; j < centerIndex; j++)
                    {
                        includeNum.Add(_sudokuArr[i + centerIndex, changeRow + j]);
                    }
                }
            }



            //얘는 3X3이 하나 만들어질때까지 반복
            if (makeLineNum < centerIndex)
            {
                if (!isVerticalLeft)
                {
                    startIndex = changeRow;
                }
                else
                {
                    startIndex = changeColumn;
                }


                for (int i = 0; i < includeNum.Count; i++)
                {
                    if (i % 3 > 0)
                    {
                        mustHaveNum.Add(includeNum[i]);
                    }
                }

                for (int i = 0; i < _sudokuArr.GetLength(0); i++)
                {
                    if (_sudokuArr[changeColumn, i] != 0)
                    {
                        if (!exceptNum.Contains(_sudokuArr[changeColumn, i]))
                        {
                            exceptNum.Add(_sudokuArr[changeColumn, i]);
                        }
                    }
                }

                // 검사해야 하는 줄 개수
                examineLine = 0;
                // 랜덤으로 나올 숫자에서 제외해야할 includeNum 인덱스
                exceptIndex = 0;
                nextExamineLine = 0;

                while (examineLine < 3)
                {
                    exceptIndex = isVerticalLeft ? (examineLine * centerIndex) + makeLineNum : (makeLineNum * centerIndex) + examineLine;
                    nextExamineLine = isVerticalLeft ? 1 : centerIndex;

                    if (!exceptNum.Contains(includeNum[exceptIndex]))
                    {
                        exceptNum.Add(includeNum[exceptIndex]);
                        tempNumList.Add(includeNum[exceptIndex]);
                    }
                    examineLine++;
                }

                _sudokuArr[changeColumn, changeRow] = rand.RandWithoutDuplicat(1, maxNum, exceptNum, ref mustHaveNum);
                exceptNum.Add(_sudokuArr[changeColumn, changeRow]);

                exceptNum.Clear();

                changeColumn++;

                for (int i = 0; i < tempNumList.Count; i++)
                {
                    exceptNum.Add(tempNumList[i]);
                }

                for (int i = 0; i < _sudokuArr.GetLength(0); i++)
                {
                    if (_sudokuArr[changeColumn, i] != 0)
                    {
                        if (!exceptNum.Contains(_sudokuArr[changeColumn, i]))
                        {
                            exceptNum.Add(_sudokuArr[changeColumn, i]);
                        }
                    }
                }



                // 검사해야 하는 줄 개수
                examineLine = 0;
                // 랜덤으로 나올 숫자에서 제외해야할 includeNum 인덱스
                exceptIndex = 0;
                nextExamineLine = 0;


                while (examineLine < 3)
                {
                    exceptIndex = isVerticalLeft ? (examineLine * centerIndex) + makeLineNum : (makeLineNum * centerIndex) + examineLine;
                    nextExamineLine = isVerticalLeft ? 1 : centerIndex;

                    if (!exceptNum.Contains(includeNum[exceptIndex]))
                    {
                        exceptNum.Add(includeNum[exceptIndex]);
                    }
                    examineLine++;
                }

                for (int i = 0; i < _sudokuArr.GetLength(0); i++)
                {
                    if (_sudokuArr[changeColumn, i] != 0)
                    {
                        if (!exceptNum.Contains(_sudokuArr[changeColumn, i]))
                        {
                            exceptNum.Add(_sudokuArr[changeColumn, i]);
                        }
                    }
                }

                _sudokuArr[changeColumn, changeRow] = rand.RandWithoutDuplicat(1, maxNum, exceptNum, ref mustHaveNum);
                exceptNum.Add(_sudokuArr[changeColumn, changeRow]);


                // 제외해야 하는 숫자 List에 제거
                for (int i = 0; i < _sudokuArr.GetLength(0); i++)
                {
                    if (exceptNum.Contains(_sudokuArr[changeColumn, i]))
                    {
                        exceptNum.Remove(_sudokuArr[changeColumn, i]);
                    }
                }

                // 다음 줄 exceptNum에 다시 추가
                changeColumn++;

                for (int i = 0; i < _sudokuArr.GetLength(0); i++)
                {
                    if (_sudokuArr[changeColumn, i] != 0)
                    {
                        if (!exceptNum.Contains(_sudokuArr[changeColumn, i]))
                        {
                            exceptNum.Add(_sudokuArr[changeColumn, i]);
                        }
                    }
                }

                examineLine = 0;
                while (examineLine < 3)
                {
                    exceptIndex = isVerticalLeft ? (examineLine * centerIndex) + makeLineNum : (makeLineNum * centerIndex) + examineLine;
                    nextExamineLine = isVerticalLeft ? 1 : centerIndex;

                    if (!exceptNum.Contains(includeNum[exceptIndex]))
                    {
                        exceptNum.Add(includeNum[exceptIndex]);
                    }
                    examineLine++;
                }

                // 들어가야 하는 숫자 추가
                for (int i = 0; i < _sudokuArr.GetLength(0); i++)
                {
                    if (_sudokuArr[changeColumn + 1, i] != 0)
                    {
                        if (!mustHaveNum.Contains(_sudokuArr[changeColumn + 1, i]))
                        {
                            mustHaveNum.Add(_sudokuArr[changeColumn + 1, i]);
                        }
                    }
                }

                // 제외해야 하는 숫자에 들어가야 하는 숫자가 있으면 제외
                for (int i = 0; i < exceptNum.Count; i++)
                {
                    if (mustHaveNum.Contains(exceptNum[i]))
                    {
                        mustHaveNum.Remove(exceptNum[i]);
                    }
                }
                //Console.WriteLine(mustHaveNum.Count);

                _sudokuArr[changeColumn, changeRow] = rand.RandWithoutDuplicat(1, maxNum, exceptNum, ref mustHaveNum);
                exceptNum.Add(_sudokuArr[changeColumn, changeRow]);


                for (int i = 0; i < mustHaveNum.Count; i++)
                {
                    Console.Write("mustHaveNum : " + mustHaveNum[i] + "\t");
                }
                Console.WriteLine();

                for (int i = 0; i < exceptNum.Count; i++)
                {
                    Console.Write(exceptNum[i] + "\t");
                }
                Console.WriteLine();
                Console.WriteLine();


                makeLineNum++;
                startIndex++;
            }


































            // 잘 나왔는지 임의로 출력
            for (int i = 0; i < _sudokuArr.GetLength(0); i++)
            {
                for (int j = 0; j < _sudokuArr.GetLength(1); j++)
                {
                    Console.Write(_sudokuArr[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }

    }
}
