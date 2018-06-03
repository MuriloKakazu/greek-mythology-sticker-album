using stickeralbum.Generics;
using stickeralbum.IO;
using System;
using System.IO;

namespace stickeralbum.Debug
{
    public static class DebugUtils
    {
        static LinkedList<String> Buffer = new LinkedList<String>();

        public static void FlushBuffer() {
            String lines = "";
            Buffer.ForEach(x => lines += x + Environment.NewLine);
            try {
                File.WriteAllText(Paths.LogsDirectory + DateTime.Now + ".log", lines);
                Buffer.Clear();
            } catch (Exception e) {
                LogError($"Couldn't save log. Reason: {e.Message}");
            }
        }

        public static void Log(object message, ConsoleColor color) {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
            Buffer.Add(message.ToString());
        }

        public static void Log(object message)
            => Log($"[DEBUG] => {message.ToString()}", ConsoleColor.Cyan);

        public static void LogError(String message)
            => Log($"[ERROR] => {message}", ConsoleColor.Red);

        public static void LogWarning(String message)
            => Log($"[WARNING] => {message}", ConsoleColor.Yellow);

        public static void LogIO(String message)
            => Log($"[IO] => {message}", ConsoleColor.Green);

        public static void LogAudio(String message)
            => Log($"[AUDIO] => {message}", ConsoleColor.Magenta);

        public static void LogCache(String message)
            => Log($"[CACHE] => {message}", ConsoleColor.Magenta);

        public static Boolean Assert(Boolean condition)
            => (!condition) ? throw new Exception("Assertion Failed") : condition;
    }
}
