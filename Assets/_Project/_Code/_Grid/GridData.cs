using System;
using _Project._Code._Common;
using UnityEngine;

namespace _Project._Code._Grid
{
    public class GridData
    {
        private const string LogKey = "GridData";
        
        private int[] _data;
        private int _width;
        private int _height;
        
        public GridData(int[] data, int width, int height)
        {
            _data = data;
            _width = width;
            _height = height;
        }

        public Result<int[]> GetIndexes(int currentIndex, int gridSize)
        {
            if (currentIndex < 0)
            {
                Debug.LogError($"[{LogKey}] pivotIndex < 0");
                return Result<int[]>.Fail();
            }

            int size = gridSize * gridSize;
            var indexes = new int[size];

            for (int i = 0; i < size; i++)
            {
                indexes[i] = _data[i];
            }
            
            return Result<int[]>.Success(indexes);
        }
        
        public int[] GetAreaAround(int centerIndex, int areaSize)
        {
            var result = new int[areaSize * areaSize];
            var halfSize = areaSize / 2;
            var centerX = centerIndex % _width;
            var centerY = centerIndex / _width;

            var resultIndex = 0;
            for (var dy = -halfSize; dy <= halfSize; dy++)
            {
                for (var dx = -halfSize; dx <= halfSize; dx++)
                {
                    var targetX = WrapCoordinate(centerX + dx, _width);
                    var targetY = WrapCoordinate(centerY + dy, _height);
                    var targetIndex = targetY * _width + targetX;

                    result[resultIndex] = _data[targetIndex];
                    resultIndex++;
                }
            }

            return result;
        }
        
        private int WrapCoordinate(int coordinate, int maxValue)
        {
            if (coordinate < 0)
            {
                return coordinate + maxValue;
            }

            if (coordinate >= maxValue)
            {
                return coordinate - maxValue;
            }
            
            return coordinate;
        }
    }
}