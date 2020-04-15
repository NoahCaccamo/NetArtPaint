namespace Common

{
    public class Packet
    {
        public enum pType { SubmitPainting, Bid }
        public int Type;
        public byte[] Painting;
        public string Title;
        public string Description;

    }
}
