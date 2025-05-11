namespace Zoo.Common
{
    public static class ZooExtensions
    {
        // Метод розширення
        public static bool IsOld(this Animal animal)
        {
            return animal.Age > 10;
        }
    }
}
