namespace Server
{
    public class ImageBlock
    {
        public byte[] ImageData { get; private set; }
        public int Position { get; private set; }
        public string Hash { get; private set; }

        public ImageBlock(byte[] imageData, int position, string hash)
        {
            ImageData = imageData;
            Position = position;
            Hash = hash;
        }
    }
}
