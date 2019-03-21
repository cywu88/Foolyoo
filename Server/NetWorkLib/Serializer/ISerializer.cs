namespace NetWorkLib
{
    public interface ISerializer
    {
        void Init();
        object Deserialize(byte[] data);
        byte[] Serialize(object proto);
    }
}