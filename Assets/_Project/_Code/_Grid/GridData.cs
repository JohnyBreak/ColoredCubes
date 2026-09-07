namespace _Project._Code._Grid
{
    public class GridData
    {
        private int[] _data;
        private int _width;
        private int _height;
        
        public GridData(int[] data, int width, int height)
        {
            _data = data;
            _width = width;
            _height = height;
        }
        
        public int[] GetArea(int centerIndex, int areaSize)
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