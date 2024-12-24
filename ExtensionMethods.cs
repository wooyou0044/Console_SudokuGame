using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace SudokuGame
{
    static class ExtensionMethods
    {
        static List<int> _randList = new List<int>();

        // 열에 중복된 숫자 없게 생성

        public static int RandWithoutDuplicat(this Random rand, int startNum, int endNum, List<int> exceptNum, ref List<int> includeNum)
        {
            int randNum;

            if (includeNum.Count > 0)
            {
                randNum = includeNum[rand.Next(0, includeNum.Count)];
                includeNum.Remove(randNum);
                if(!_randList.Contains(randNum))
                {
                    _randList.Add(randNum);
                }
            }

            else
            {
                for (int i = 0; i < exceptNum.Count; i++)
                {
                    if (!_randList.Contains(exceptNum[i]))
                    {
                        _randList.Add(exceptNum[i]);
                    }
                }

                if (exceptNum.Count > 0 && _randList.Count > exceptNum.Count)
                {
                    for (int i = 0; i < _randList.Count; i++)
                    {
                        if (!exceptNum.Contains(_randList[i]))
                        {
                            _randList.Remove(_randList[i]);
                        }
                    }
                }

                randNum = rand.Next(startNum, endNum + 1);

                // List에 아무것도 안 들어있으면 그냥 랜덤한 숫자만 리턴
                if (_randList.Count > 0)
                {
                    for (int index = 0; index < _randList.Count; index++)
                    {
                        if (_randList[index] == randNum)
                        {
                            randNum = rand.Next(startNum, endNum + 1);
                            // index = 0으로 하게 되면 반복문 끝날 때 index + 1이 되기 때문에 _randList[0]을 보려면 -1로 대입
                            index = -1;
                        }
                    }
                }
                _randList.Add(randNum);
            }

            if (_randList.Count >= endNum)
            {
                _randList.Clear();
            }
            return randNum;
        }
    }
}
