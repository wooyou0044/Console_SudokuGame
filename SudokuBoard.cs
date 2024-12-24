using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SudokuGame
{
    class SudokuBoard
    {
        // 스도쿠 배열
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

            // 일단 숫자부터 출력(3X3칸이 중복되는 숫자 없게)
            //while(lineCount < 9)
            //{
            //    // 열의 개수가 9보다 많아지면 반복문 탈출
            //    if (rowCount >= 9)
            //    {
            //        break;
            //    }
            //    for (int j = rowCount; j < 3 + rowCount; j++)
            //    {
            //        _sudokuArr[lineCount, j] = rand.RandWithoutDuplicat(1, 9);
            //    }
            //    lineCount++;

            //    // 3x3칸이 완료되었을 때 열의 개수를 +3 증가, 행은 0으로 설정
            //    if (lineCount % 9 == 0)
            //    {
            //        rowCount += 3;
            //        lineCount = 0;
            //    }
            //}

            List<int> exceptNum = new List<int>();
            List<int> includeNum = new List<int>();
            List<int> mustHaveNum = new List<int>();

            // 중간 3X3을 기준으로 함
            // 얘를 기준으로 다음 3X3을 만들 때는 기준이 된 숫자들 제외하고 랜덤으로 나오게 하기
            for (int i=0; i<3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    _sudokuArr[i + 3, j + 3] = rand.RandWithoutDuplicat(1, 9, exceptNum, ref mustHaveNum);
                    // 넣은 숫자 List에 추가
                    includeNum.Add(_sudokuArr[i + 3, j + 3]);
                }
            }

            // ==================================
            // 일단 (0,3) ~ (2,5)까지만
            for(int i=0; i<includeNum.Count; i++)
            {
                if(i%3 == 0)
                {
                    exceptNum.Add(includeNum[i]);
                }
            }

            for (int i = 0; i < 3; i++)
            {
                _sudokuArr[i, 3] = rand.RandWithoutDuplicat(1, 9, exceptNum, ref mustHaveNum);
                exceptNum.Add(_sudokuArr[i, 3]);
            }

            for (int i = 6; i < 9; i++)
            {
                _sudokuArr[i, 3] = rand.RandWithoutDuplicat(1, 9, exceptNum, ref mustHaveNum);
                exceptNum.Add(_sudokuArr[i, 3]);
            }

            exceptNum.Clear();

            for (int i = 0; i < includeNum.Count; i++)
            {
                if (i % 3 == 1)
                {
                    if (!exceptNum.Contains(includeNum[i]))
                    {
                        exceptNum.Add(includeNum[i]);
                    }
                }
                if (i % 3 == 2)
                {
                    mustHaveNum.Add(includeNum[i]);
                }
            }

            for (int i = 0; i < 3; i++)
            {
                // 중복이 들어갈 수 있어서 중복 처리
                if (!exceptNum.Contains(_sudokuArr[i, 3]))
                {
                    exceptNum.Add(_sudokuArr[i, 3]);
                }
                if (mustHaveNum.Contains(_sudokuArr[i,3]))
                {
                    mustHaveNum.Remove(_sudokuArr[i, 3]);
                }
            }

            for (int i = 0; i < 3; i++)
            {
                _sudokuArr[i, 4] = rand.RandWithoutDuplicat(1, 9, exceptNum, ref mustHaveNum);
                exceptNum.Add(_sudokuArr[i, 4]);
            }

            exceptNum.Clear();

            for (int i = 0; i < 3; i++)
            {
                for (int j = 3; j < 5; j++)
                {
                    exceptNum.Add(_sudokuArr[i, j]);
                }
            }

            for (int i = 0; i < 3; i++)
            {
                _sudokuArr[i, 5] = rand.RandWithoutDuplicat(1, 9, exceptNum, ref mustHaveNum);
                exceptNum.Add(_sudokuArr[i, 5]);
            }

            exceptNum.Clear();

            for(int i=0; i<6; i++)
            {
                exceptNum.Add(_sudokuArr[i, 4]);
            }

            for(int i=6; i<9; i++)
            {
                _sudokuArr[i, 4] = rand.RandWithoutDuplicat(1, 9, exceptNum, ref mustHaveNum);
                exceptNum.Add(_sudokuArr[i, 4]);
            }

            exceptNum.Clear();

            for(int i=6; i<9; i++)
            {
                for(int j=3; j<5; j++)
                {
                    exceptNum.Add(_sudokuArr[i, j]);
                }
            }

            for(int i=0; i<mustHaveNum.Count; i++)
            {
                Console.WriteLine(mustHaveNum[i]);
            }

            for (int i = 6; i < 9; i++)
            {
                _sudokuArr[i, 5] = rand.RandWithoutDuplicat(1, 9, exceptNum, ref mustHaveNum);
                exceptNum.Add(_sudokuArr[i, 5]);
            }

            exceptNum.Clear();

            // 잘 나왔는지 임의로 출력
            for (int i = 0; i < _sudokuArr.GetLength(0); i++)
            {
                for (int j = 0; j < _sudokuArr.GetLength(1); j++)
                {
                    Console.Write(_sudokuArr[i, j] + "\t");
                }
                Console.WriteLine();
            }

            for(int i=0; i<exceptNum.Count; i++)
            {
                Console.Write(exceptNum[i] + "\t");
            }
        }
    }
}
