namespace EtappenProblem
{
    public class Algorithm
    {
        private readonly List<int> _stageLengths;
        private readonly int _amountDays;

        private int _minPossibleLength;
        private int _maxPossibleLength;

        private int _referenceValue;
        private List<int> _lengthsPerDay = new();
        private List<int> _lastLengthsPerDaySolution = new();

        public List<int> LengthsPerDaySolution => _lastLengthsPerDaySolution;
        public int MaxLength => _referenceValue;

        public Algorithm(List<int> stageLengths, int amountDays, int totalLength)
        {
            _stageLengths = stageLengths;
            _amountDays = amountDays;

            //We guarantee that the initial referenceValue is a failure
            // Solution is somewhere in ]_minPossibleLength, _maxPossibleLength]
            _minPossibleLength = (int)MathF.Ceiling((float)totalLength / (float)_amountDays) -1;
            _maxPossibleLength = totalLength;

            SetNewReferenceValue();
        }

        public void FindSolution()
        {
            // If the amount of days equals the amount of stages, the maximum length is simply the longest stage
            if (_stageLengths.Count == _amountDays)
            {
                _lastLengthsPerDaySolution = _stageLengths;
                _referenceValue = _stageLengths.Max();
                return;
            }

            // If there is only one day, the solution is simply the total length
            if (_amountDays == 1)
            {
                _lastLengthsPerDaySolution.Add(_maxPossibleLength);
                _referenceValue = _maxPossibleLength;
                return;
            }


            while (true)
            {
                var result = TryFindSolutionForCurrentReferenceValue();

                if (result == -1)
                {
                    _minPossibleLength = _referenceValue;
                }
                else
                {
                    _maxPossibleLength = result;
                    _lastLengthsPerDaySolution.Clear();
                    _lastLengthsPerDaySolution.AddRange(_lengthsPerDay);
                }

                SetNewReferenceValue();

                //Weve found our optimal solution solution already
                if (_referenceValue == _maxPossibleLength)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Run iteration for current reference value.
        /// </summary>
        /// <returns>Maximum length of found solution, -1 if no solution was found</returns>
        private int TryFindSolutionForCurrentReferenceValue()
        {
            //Initialize all values. Make sure array with length is always cleaned up
            var maxLengthForThisIteration = 0;
            int dayIndex = 0;
            _lengthsPerDay.Clear();
            _lengthsPerDay.Add(0);

            //Go through all stages and assign them to a day
            for (int i = 0; i < _stageLengths.Count; i++)
            {
                // if adding the next length would result in it going over ref value, we increment dayIndex (if possible)
                if (_lengthsPerDay[dayIndex] + _stageLengths[i] > _referenceValue && dayIndex < _amountDays - 1)
                {
                    maxLengthForThisIteration = Math.Max(maxLengthForThisIteration, _lengthsPerDay[dayIndex]);
                    dayIndex++;
                    _lengthsPerDay.Add(0);
                }

                _lengthsPerDay[dayIndex] += _stageLengths[i];
            }

            //Check if last day is longest
            maxLengthForThisIteration = Math.Max(maxLengthForThisIteration, _lengthsPerDay[dayIndex]);

            return maxLengthForThisIteration <= _referenceValue ? maxLengthForThisIteration : -1;
        }

        private void SetNewReferenceValue()
        {
            if (_minPossibleLength == _maxPossibleLength)
            {
                return;
            }
            _referenceValue = _minPossibleLength + (int)MathF.Ceiling((_maxPossibleLength - _minPossibleLength) / 2f);
        }
    }
}
