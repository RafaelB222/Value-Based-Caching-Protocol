using System.Security.Cryptography;

namespace Server
{
    internal class ImageFragmenter
    {
        private byte[] _imageData;
        private int _targetBlockSize;
        private uint _mask;
        private Dictionary<string, ImageBlock> _sentBlocks;


        public ImageFragmenter(byte[] image, int targetBlockSize, uint mask, Dictionary<string, ImageBlock> sentBlocks)
        {
            _imageData = image;
            _targetBlockSize = targetBlockSize;
            _mask = mask;
            _sentBlocks = sentBlocks;
        }

        public Dictionary<string, ImageBlock> FragmentImageIntoBlocks()
        {
            RabinBlockFinder rabinBlockFinder = new RabinBlockFinder(_targetBlockSize);
            Dictionary<string, ImageBlock> imageBlocks = new Dictionary<string, ImageBlock>();
            int blockStart = 0;

            for (int i = 0; i < _imageData.Length; i++)
            {
                byte currentByte = _imageData[i];
                rabinBlockFinder.Append(currentByte);

                if (i >= _targetBlockSize)
                {
                    rabinBlockFinder.Skip(_imageData[i - _targetBlockSize]);
                }

                if (i - blockStart + 1 >= _targetBlockSize || (rabinBlockFinder.GetHash() & _mask) == _mask)
                {
                    CreateAndAddBlock(imageBlocks, blockStart, i - blockStart + 1);
                    blockStart = i + 1;

                    // Create a new RabinBlockFinder instance for the new block
                    rabinBlockFinder = new RabinBlockFinder(_targetBlockSize);                    
                }
            }

            // Add the last block if it hasn't been added yet
            if (blockStart < _imageData.Length)
            {
                CreateAndAddBlock(imageBlocks, blockStart, _imageData.Length - blockStart);
            }

            return imageBlocks;
        }

        private void CreateAndAddBlock(Dictionary<string, ImageBlock> imageBlocks, int blockStart, int blockSize)
        {
            byte[] blockData = new byte[blockSize];
            Array.Copy(_imageData, blockStart, blockData, 0, blockSize);

            using (var md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(blockData);
                string hashString = BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();

                if (_sentBlocks.ContainsKey(hashString))
                {
                    imageBlocks[hashString] = _sentBlocks[hashString];
                }
                else
                {
                    ImageBlock block = new ImageBlock(blockData, blockStart, hashString);

                    if (!imageBlocks.ContainsKey(hashString))
                    {
                        imageBlocks[hashString] = block;
                    }
                }
                
            }
        }
    }
}
