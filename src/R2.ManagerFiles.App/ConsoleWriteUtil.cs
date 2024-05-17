namespace R2.ManagerFiles.App
{
    public static class ConsoleWriteUtil
    {
        public static void Write(string? text = "", ConsoleColor? color = ConsoleColor.White)
        {
            Console.ForegroundColor = color != null ? color.Value : ConsoleColor.White;
            Console.WriteLine(text);
        }
    }
}
