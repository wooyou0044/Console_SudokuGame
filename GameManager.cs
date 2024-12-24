using System;
using System.Collections.Generic;

namespace SudokuGame
{
    enum Level
    {
        Easy, Average, Hard
    }

    class GameManager
    {
        SudokuBoard _board;
        int _levelSellNum;

        //임시로 만든 스도쿠 보드
        SudokuBoardPractice _practice;

        public int LevelSellNum
        {
            get
            {
                return _levelSellNum;
            }
            set
            {
                _levelSellNum = value;
            }
        }

        public GameManager()
        {
            _board = new SudokuBoard();

            _practice = new SudokuBoardPractice();
        }

        public void Play()
        {
            // 스도쿠 보드 만들기 위해서 임의로 생성
            //LevelChoose();
            //_board.GenerateSudoku();
            _practice.GenerateSudoku();
        }

        void LevelChoose()
        {
            Console.WriteLine("▼▼▼▼▼레벨 선택▼▼▼▼▼");
            Console.WriteLine();
            Console.WriteLine("　　　　　　Easy");
            Console.WriteLine("　　　　　Average");
            Console.WriteLine("　　　　　　Hard");

            // 일단 임의로 숫자를 넣었다고 가정
            // 레벨을 선택하면
            Console.Write("레벨 선택 : ");
            int.TryParse(Console.ReadLine(), out int level);
            // 선택된 레벨에 따라 칸 수 달라짐
            SetArrAccordingLevel((Level)level);
        }


        void SetArrAccordingLevel(Level level)
        {
            switch (level)
            {
                case Level.Easy:
                case Level.Average:
                    LevelSellNum = 3;
                    break;
                case Level.Hard:
                    LevelSellNum = 4;
                    break;
            }
        }
    }
}
