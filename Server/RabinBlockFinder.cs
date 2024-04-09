namespace Server
{
    internal class RabinBlockFinder
    {
        private const uint PrimeBase = 256;
        private const uint PrimeMod = 16777619;

        private uint _hash;
        private uint _primeBasePow;

        public RabinBlockFinder(int blockSize)
        {
            _primeBasePow = 1;
            for (int i = 0; i < blockSize - 1; i++)
            {
                _primeBasePow = (_primeBasePow * PrimeBase) % PrimeMod;
            }
        }

        public void Append(byte b)
        {
            _hash = ((_hash * PrimeBase) + b) % PrimeMod;
        }

        public void Skip(byte b)
        {
            _hash = (_hash + PrimeMod - ((_primeBasePow * b) % PrimeMod)) % PrimeMod;
        }

        public uint GetHash()
        {
            return _hash;
        }
    }
}
