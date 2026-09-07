using InputSystem;

namespace _Project._Code._Grid
{
    public class GridData
    {
        private int[] _data;
        private int _width;
        private int _height;

        public int Count => _data?.Length ?? 0;

        public GridData(int[] data, int width, int height)
        {
            _data = data;
            _width = width;
            _height = height;
        }

        public int GetNeighbourIndex(int currentIndex, InputDirection direction)
        {
            switch (direction)
            {
                case InputDirection.Up:
                    return GetUp(currentIndex);
                case InputDirection.Right:
                    return GetRight(currentIndex);
                case InputDirection.Down:
                    return GetDown(currentIndex);
                case InputDirection.Left:
                    return GetLeft(currentIndex);
            }
            
            return currentIndex;
        }
        
        public int[] GetArea(int centerIndex, int areaSize)
        {
            var result = new int[areaSize * areaSize];
            var halfSize = areaSize / 2;
            var centerX = centerIndex % _width;
            var centerY = centerIndex / _width;

            var resultIndex = 0;
    
            for (var y = 0; y < areaSize; y++)
            {
                for (var x = 0; x < areaSize; x++)
                {
                    var dx = x - halfSize;
                    var dy = y - halfSize;

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
            return (coordinate % maxValue + maxValue) % maxValue;
        }
        
        private int GetUp(int currentIndex)
        {
            var newIndex = currentIndex - _width;
            return newIndex < 0 ? newIndex + Count : newIndex;
        }
        
        private int GetDown(int currentIndex)
        {
            var newIndex = currentIndex + _width;
            return newIndex >= Count ? newIndex - Count : newIndex;
        }
        
        private int GetRight(int currentIndex)
        {
            var currentRow = currentIndex / _width;
            var currentCol = currentIndex % _width;
            var newCol = (currentCol + 1) % _width;
            return currentRow * _width + newCol;
        }
        
        private int GetLeft(int currentIndex)
        {
            var currentRow = currentIndex / _width;
            var currentCol = currentIndex % _width;
            var newCol = currentCol == 0 ? _width - 1 : currentCol - 1;
            return currentRow * _width + newCol;
        }
    }
}