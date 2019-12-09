using System;
using static FutureFinancingTriangle.Generator.Logic.Randomiser;

namespace FutureFinancingTriangle.Generator.Logic
{
    internal interface IRandomser
    {
        bool RandomNextBool();
        int RandomNextInt(int maxNumber, OddEven oddEven);
        OddEven SwitchOddEven(OddEven oddEven);
    }

    internal class Randomiser : IRandomser
    {
        private Random _random = new Random(DateTime.Now.Millisecond);

        public enum OddEven { Odd, Even, Any };

        public bool RandomNextBool()
        {
            var i = RandomNextInt(2, OddEven.Any);
            return i % 2 == 1;
        }

        public int RandomNextInt(int maxNumber, OddEven oddEven)
        {
            while (true)
            {
                var number = _random.Next(maxNumber) + 1;
                if (oddEven == OddEven.Any 
                    || oddEven == OddEven.Even && number % 2 == 0 
                    || oddEven == OddEven.Odd && number % 2 == 1)
                    return number;
            }
        }

        public OddEven SwitchOddEven(OddEven oddEven)
        {
            if (oddEven == OddEven.Any)
                return oddEven;
            if (oddEven == OddEven.Even)
                return OddEven.Odd;
            return OddEven.Even;
        }
    }
}
