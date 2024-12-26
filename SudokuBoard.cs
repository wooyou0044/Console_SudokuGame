using System;
using System.Collections.Generic;

namespace SudokuGame
{
    class SudokuBoard
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

            // 제외할 숫자를 검사하는 변수 선언
            Stack<int> notIncludedNum = new Stack<int>();
            Stack<int> includedNum = new Stack<int>();


            //==================================================================================





















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
