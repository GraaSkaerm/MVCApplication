namespace MVCApplication.Extensions
{
    public static class BatchExtension
    {
        // New example
        public static List<T>[] CreatBatches<T>(List<T> target, int batchLength)
        {
            int amountOfBatches = target.Count / batchLength;
            int lastBatchIndex = amountOfBatches * batchLength;
            int finalAmountOfBatches = amountOfBatches;
            if (lastBatchIndex != target.Count) finalAmountOfBatches++;

            List<T>[] list = new List<T>[finalAmountOfBatches];

            for (int batchIndex = 0; batchIndex < amountOfBatches; batchIndex++)
            {
                int batchOffset = batchIndex * batchLength;
                list[batchIndex] = target.GetRange(batchOffset, batchLength);
            }

            if (lastBatchIndex == target.Count) return list;

            int remaining = target.Count - lastBatchIndex;
            list[amountOfBatches] = target.GetRange(lastBatchIndex, remaining);


            return list;
        }
    }
}
