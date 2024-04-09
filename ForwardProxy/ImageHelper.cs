using Server;
using System.Text;
using System.Text.Json;

namespace ForwardProxy
{
    internal class ImageHelper
    {
        public ImageHelper()
        {

        }

        public ImageBlock DeserializeImageBlock(byte[] serializedBlockData)
        {
            if (serializedBlockData == null)
            {
                return default(ImageBlock);
            }
            else
            {
                string blockJsonString = Encoding.UTF8.GetString(serializedBlockData);
                ImageBlock block = JsonSerializer.Deserialize<ImageBlock>(blockJsonString);
                return block;
            }
            

        }

        public byte[] ReconstructImageFromBlocks(Dictionary<string, ImageBlock> receivedImageBlocks)
        {
            List<ImageBlock> sortedImageBlocks = receivedImageBlocks.Values.OrderBy(block => block.Position).ToList();
            List<byte[]> imageDataList = new List<byte[]>();

            foreach (ImageBlock block in sortedImageBlocks)
            {
                byte[] blockData = block.ImageData;
                imageDataList.Add(blockData);
            }

            byte[] reconstructedImageData = imageDataList.SelectMany(x => x).ToArray();
            return reconstructedImageData;
        }

    }
}
