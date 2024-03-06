// Creator: Job
using System;
using UnityEngine;
using WinterRose;

namespace ShadowUprising
{
    /// <summary>
    /// Provides a way to log to the unity console when in the editor, and a console window when in a build.
    /// </summary>
    public static class Log
    {
        /// <summary>
        /// Whether or not the console is enabled. will show a message box if trying to enable the console in the editor.
        /// </summary>
        public static bool ConsoleEnabled
        {
            get => consoleEnabled;
            set
            {
#if UNITY_EDITOR
                Windows.MessageBox("Cant use console inside the editor. use uniy's default debugger console.", "Warning", Windows.MessageBoxButtons.OK, Windows.MessageBoxIcon.Exclamation);
                return;
#endif

                consoleEnabled = value;
                if (value)
                {
                    Windows.OpenConsole();
                    Console.Title = "Shadow Uprising Log Console";
                }
                else
                {
                    Windows.CloseConsole();
                }
            }
        }
        private static bool consoleEnabled = false;

        static Log()
        {
        }

        /// <summary>
        /// Pushes the message to the log with the specified log type.
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="message"></param>
        public static void Push(LogType logType, object message)
        {
            switch (logType)
            {
                case LogType.Error:
                    WriteToConsole("Error: " + message.ToString(), ConsoleColor.Red);
                    break;
                case LogType.Assert:
                    WriteToConsole("Assert: " + message.ToString(), ConsoleColor.Cyan);
                    break;
                case LogType.Warning:
                    WriteToConsole("Warning: " + message.ToString(), ConsoleColor.Yellow);
                    break;
                case LogType.Log:
                    WriteToConsole(message.ToString(), ConsoleColor.White);
                    break;
                case LogType.Exception:
                    if (message is Exception ex)
                    {
                        WriteToConsole($"Exception '{ex.GetType().Name}': " + ex.Message + "\n" + ex.StackTrace ?? "No stack trace.", ConsoleColor.Magenta);
                    }
                    else
                    {
                        WriteToConsole("Exception: " + message.ToString(), ConsoleColor.Magenta);
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Pushes the message to the log with the specified log type.
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="message"></param>
        /// <param name="context"></param>
        public static void Push(LogType logType, object message, UnityEngine.Object context)
        {
            switch (logType)
            {
                case LogType.Error:
                    WriteToConsole($"Error from object '{context.name}': " + message.ToString(), ConsoleColor.Red);
                    break;
                case LogType.Assert:
                    WriteToConsole($"Assert from object '{context.name}': " + message.ToString(), ConsoleColor.Cyan);
                    break;
                case LogType.Warning:
                    WriteToConsole($"Warning from object '{context.name}': " + message.ToString(), ConsoleColor.Yellow);
                    break;
                case LogType.Log:
                    WriteToConsole($"Source: '{context.name}': " + message.ToString(), ConsoleColor.White);
                    break;
                case LogType.Exception:
                    if (message is Exception ex)
                    {
                        WriteToConsole($"Exception '{ex.GetType().Name}' from '{context.name}': " + ex.Message + "\n" + ex.StackTrace ?? "No stack trace.", ConsoleColor.Magenta);
                    }
                    else
                    {
                        WriteToConsole($"Exception from '{context.name}': " + message.ToString(), ConsoleColor.Magenta);
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Pushes the message to the log with the specified log type.
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="tag"></param>
        /// <param name="message"></param>
        public static void Push(LogType logType, string tag, object message)
        {
            switch (logType)
            {
                case LogType.Error:
                    WriteToConsole($"Error '{tag}': " + message.ToString(), ConsoleColor.Red);
                    break;
                case LogType.Assert:
                    WriteToConsole($"Assert  '{tag}': " + message.ToString(), ConsoleColor.Cyan);
                    break;
                case LogType.Warning:
                    WriteToConsole($"Warning '{tag}': " + message.ToString(), ConsoleColor.Yellow);
                    break;
                case LogType.Log:
                    WriteToConsole($"'{tag}': " + message.ToString(), ConsoleColor.White);
                    break;
                case LogType.Exception:
                    if (message is Exception ex)
                    {
                        WriteToConsole($"Exception '{ex.GetType().Name}' '{tag}': " + ex.Message + "\n" + ex.StackTrace ?? "No stack trace.", ConsoleColor.Magenta);
                    }
                    else
                    {
                        WriteToConsole($"Exception from '{tag}': " + message.ToString(), ConsoleColor.Magenta);
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Pushes the message to the log with the specified log type.
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="tag"></param>
        /// <param name="message"></param>
        /// <param name="context"></param>
        public static void Push(LogType logType, string tag, object message, UnityEngine.Object context)
        {
            Push(logType, message, context);
        }

        /// <summary>
        /// Pushes the message to the log.
        /// </summary>
        /// <param name="message"></param>
        public static void Push(object message)
        {
            Push(LogType.Log, message);
        }

        /// <summary>
        /// Pushes the message to the log.
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="message"></param>
        public static void Push(string tag, object message)
        {
            Push(LogType.Log, tag, message);
        }

        /// <summary>
        /// Pushes the message to the log.
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="message"></param>
        /// <param name="context"></param>
        public static void Push(string tag, object message, UnityEngine.Object context)
        {
            Push(LogType.Log, tag, message, context);
        }

        /// <summary>
        /// Pushes the error message to the log.
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="message"></param>
        public static void PushError(object message)
        {
            Push(LogType.Error, message);
        }

        /// <summary>
        /// Pushes the error message to the log.
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="message"></param>
        /// <param name="context"></param>
        public static void PushError(string tag, object message, UnityEngine.Object context)
        {
            Push(LogType.Error, tag, message, context);
        }

        /// <summary>
        /// Pushes the exception to the log.
        /// </summary>
        /// <param name="exception"></param>
        public static void PushException(Exception exception)
        {
            Push(LogType.Exception, exception);
        }

        /// <summary>
        /// Pushes the exception to the log.
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="context"></param>
        public static void PushException(Exception exception, UnityEngine.Object context)
        {
            Push(LogType.Exception, exception, context);
        }

        /// <summary>
        /// Pushes the warning message to the log.
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="message"></param>
        public static void PushWarning(object message)
        {
            Push(LogType.Warning, message);
        }

        /// <summary>
        /// Pushes the warning message to the log.
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="message"></param>
        /// <param name="context"></param>
        public static void PushWarning(string tag, object message, UnityEngine.Object context)
        {
            Push(LogType.Warning, tag, message, context);
        }

        private static void WriteToConsole(string message, ConsoleColor color)
        {
#if UNITY_EDITOR
            string colorPrefix = "";

            switch (color)
            {
                case ConsoleColor.Black:
                    colorPrefix = "<color=#000000>";
                    break;
                case ConsoleColor.DarkBlue:
                    colorPrefix = "<color=#000080>";
                    break;
                case ConsoleColor.DarkGreen:
                    colorPrefix = "<color=#008000>";
                    break;
                case ConsoleColor.DarkCyan:
                    colorPrefix = "<color=#008080>";
                    break;
                case ConsoleColor.DarkRed:
                    colorPrefix = "<color=#800000>";
                    break;
                case ConsoleColor.DarkMagenta:
                    colorPrefix = "<color=#800080>";
                    break;
                case ConsoleColor.DarkYellow:
                    colorPrefix = "<color=#808000>";
                    break;
                case ConsoleColor.Gray:
                    colorPrefix = "<color=#C0C0C0>";
                    break;
                case ConsoleColor.DarkGray:
                    colorPrefix = "<color=#808080>";
                    break;
                case ConsoleColor.Blue:
                    colorPrefix = "<color=#0000FF>";
                    break;
                case ConsoleColor.Green:
                    colorPrefix = "<color=#008000>";
                    break;
                case ConsoleColor.Cyan:
                    colorPrefix = "<color=#00FFFF>";
                    break;
                case ConsoleColor.Red:
                    colorPrefix = "<color=#FF0000>";
                    break;
                case ConsoleColor.Magenta:
                    colorPrefix = "<color=#FF00FF>";
                    break;
                case ConsoleColor.Yellow:
                    colorPrefix = "<color=#FFFF00>";
                    break;
                case ConsoleColor.White:
                    colorPrefix = "<color=#FFFFFF>";
                    break;
                default:
                    break;
            }

            string colorSuffix = "</color>";

            bool error, warning, log, exception, assert;
            error = message.StartsWith("Error:");
            warning = message.StartsWith("Warning:");
            exception = message.StartsWith("Exception:");
            assert = message.StartsWith("Assert:");

            log = !error && !warning && !exception && !assert;

            message = colorPrefix + message + colorSuffix;

            if (error)
            {
                UnityEngine.Debug.LogError(message);
            }
            else if (warning)
            {
                UnityEngine.Debug.LogWarning(message);
            }
            else if (exception)
            {
                UnityEngine.Debug.LogError(message);
            }
            else if (assert)
            {
                UnityEngine.Debug.LogAssertion(message);
            }
            else if (log)
            {
                UnityEngine.Debug.Log(message);
            }
            return;
#endif
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}