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

            // 3x3칸에 중복되는 숫자 없게 만들때 만들어진 행의 개수
            int lineCount = 0;
            // 3x3칸에 중복되는 숫자 없게 만들때 만들어진 열의 개수
            int rowCount = 0;
            bool isFilled = false;

            // 제외할 숫자를 검사하는 변수 선언
            int examineLine = 0;
            int centerIndex = (int)Math.Sqrt(_sudokuArr.GetLength(0));
            int startIndex = 0;

            List<int> exceptNum = new List<int>();
            List<int> includeNum = new List<int>();
            List<int> mustHaveNum = new List<int>();


            startIndex = centerIndex;

            // 중간 3X3을 기준으로 함
            // 얘를 기준으로 다음 3X3을 만들 때는 기준이 된 숫자들 제외하고 랜덤으로 나오게 하기
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _sudokuArr[i + startIndex, j + startIndex] = rand.RandWithoutDuplicat(1, 9, exceptNum, ref mustHaveNum);
                    // 넣은 숫자 List에 추가
                    includeNum.Add(_sudokuArr[i + startIndex, j + startIndex]);
                }
            }

            // ==================================
            // 일단 (0,3) ~ (2,5)까지만

            while (!isFilled)
            {
                // 제외할 숫자 List에 추가
                for (int i = 0; i < includeNum.Count; i++)
                {
                    if (i % 3 == examineLine)
                    {
                        if (!exceptNum.Contains(includeNum[i]))
                        {
                            exceptNum.Add(includeNum[i]);
                        }
                    }
                    // 3x3의 2번째줄 채울때 실행
                    if (examineLine == 1)
                    {
                        if (i % 3 == examineLine + 1)
                        {
                            mustHaveNum.Add(includeNum[i]);
                        }
                    }
                }

                if (examineLine == 1)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        // 중복이 들어갈 수 있어서 중복 처리
                        if (!exceptNum.Contains(_sudokuArr[i, startIndex]))
                        {
                            exceptNum.Add(_sudokuArr[i, startIndex]);
                        }
                        if (mustHaveNum.Contains(_sudokuArr[i, startIndex]))
                        {
                            mustHaveNum.Remove(_sudokuArr[i, startIndex]);
                        }
                    }
                }

                if (examineLine == 2)
                {
                    // startIndex = 4;
                    // 마지막 열 exceptNum 하기 위한 조건
                    for (int i = 3; i < 6; i++)
                    {
                        if (exceptNum.Contains(_sudokuArr[i, startIndex]))
                        {
                            exceptNum.Remove(_sudokuArr[i, startIndex]);
                        }
                    }

                    for (int i = 0; i < 3; i++)
                    {
                        if (!exceptNum.Contains(_sudokuArr[i, startIndex - 1]))
                        {
                            exceptNum.Add(_sudokuArr[i, startIndex - 1]);
                        }
                    }
                }


                if (examineLine >= 1)
                {
                    startIndex++;
                }


                for (int i = 0; i < 3; i++)
                {
                    _sudokuArr[i, startIndex] = rand.RandWithoutDuplicat(1, 9, exceptNum, ref mustHaveNum);
                    exceptNum.Add(_sudokuArr[i, startIndex]);
                }

                if (examineLine == 0)
                {
                    // 마지막 열 하기 바로 전에는 하면 안 됨
                    exceptNum.Clear();
                }

                // 다음 줄 검사하기 위해 증가
                examineLine++;

                if(startIndex >= 5)
                {
                    isFilled = true;
                    exceptNum.Clear();
                }
            }

            startIndex = centerIndex;

            // 다른 것도 적용하려면 일단 다른걸로 생각
            while (startIndex <= 5)
            {
                for (int i = 0; i < 6; i++)
                {
                    exceptNum.Add(_sudokuArr[i, startIndex]);
                }
                for (int i = 6; i < 9; i++)
                {
                    _sudokuArr[i, startIndex] = rand.RandWithoutDuplicat(1, 9, exceptNum, ref mustHaveNum);
                    exceptNum.Add(_sudokuArr[i, startIndex]);
                }
                startIndex++;
                exceptNum.Clear();
            }


            //=========================================================
            examineLine = 0;
            startIndex = centerIndex;

            for (int i = 0; i < includeNum.Count; i++)
            {
                // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                if (i < 3)
                {
                    if (!exceptNum.Contains(includeNum[i]))
                    {
                        exceptNum.Add(includeNum[i]);
                    }
                }
                // 3x3의 2번째줄 채울때 실행
                if (examineLine == 1)
                {
                    // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                    if (i >= 6 && i < 9)
                    {
                        mustHaveNum.Add(includeNum[i]);
                    }
                }
            }

            for (int i = 0; i < 3; i++)
            {
                _sudokuArr[startIndex, i] = rand.RandWithoutDuplicat(1, 9, exceptNum, ref mustHaveNum);
                exceptNum.Add(_sudokuArr[startIndex, i]);
            }

            exceptNum.Clear();

            examineLine++;

            for (int i = 0; i < includeNum.Count; i++)
            {
                // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                if (i >=3 && i<6)
                {
                    if (!exceptNum.Contains(includeNum[i]))
                    {
                        exceptNum.Add(includeNum[i]);
                    }
                }
                // 3x3의 2번째줄 채울때 실행
                if (examineLine == 1)
                {
                    // ▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼▼
                    if (i >= 6 && i < 9)
                    {
                        mustHaveNum.Add(includeNum[i]);
                    }
                }
            }

            if (examineLine == 1)
            {
                for (int i = 0; i < 3; i++)
                {
                    // 중복이 들어갈 수 있어서 중복 처리
                    if (!exceptNum.Contains(_sudokuArr[startIndex, i]))
                    {
                        exceptNum.Add(_sudokuArr[startIndex, i]);
                    }
                    if (mustHaveNum.Contains(_sudokuArr[startIndex, i]))
                    {
                        mustHaveNum.Remove(_sudokuArr[startIndex, i]);
                    }
                }
            }

            startIndex++;


            for (int i = 0; i < 3; i++)
            {
                _sudokuArr[startIndex, i] = rand.RandWithoutDuplicat(1, 9, exceptNum, ref mustHaveNum);
                exceptNum.Add(_sudokuArr[startIndex, i]);
            }

            examineLine++;

            if (examineLine == 2)
            {
                // startIndex = 4;
                // 마지막 열 exceptNum 하기 위한 조건
                for (int i = 3; i < 6; i++)
                {
                    if (exceptNum.Contains(_sudokuArr[startIndex, i]))
                    {
                        exceptNum.Remove(_sudokuArr[startIndex, i]);
                    }
                }

                for (int i = 0; i < 3; i++)
                {
                    if (!exceptNum.Contains(_sudokuArr[startIndex - 1, i]))
                    {
                        exceptNum.Add(_sudokuArr[startIndex - 1, i]);
                    }
                }
            }

            startIndex++;


            for (int i = 0; i < 3; i++)
            {
                _sudokuArr[startIndex, i] = rand.RandWithoutDuplicat(1, 9, exceptNum, ref mustHaveNum);
                exceptNum.Add(_sudokuArr[startIndex, i]);
            }

            exceptNum.Clear();

            startIndex = centerIndex;

            // 다른 것도 적용하려면 일단 다른걸로 생각
            while (startIndex <= 5)
            {
                for (int i = 0; i < 6; i++)
                {
                    exceptNum.Add(_sudokuArr[startIndex, i]);
                }
                for (int i = 6; i < 9; i++)
                {
                    _sudokuArr[startIndex, i] = rand.RandWithoutDuplicat(1, 9, exceptNum, ref mustHaveNum);
                    exceptNum.Add(_sudokuArr[startIndex, i]);
                }
                startIndex++;
                exceptNum.Clear();
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
