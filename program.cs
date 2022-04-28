/// <summary>
/// Try to find process by name and brings the main window to front
/// </summary>
public class Program
{
    #region Constants

    /// <summary>
    /// Restore Mode
    /// </summary>
    const int SwRestore = 9;

    #endregion

    /// <summary>
    /// Try to find process and bring to front
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        // check process name is given as argument
        if (args == null || args.Length < 1)
        {
            Console.WriteLine();
            Console.WriteLine("Syntax: pf.exe Processname");
            Console.WriteLine();
            return;
        }

        // try to find process by name
        Process[] localByName = Process.GetProcessesByName(args[0]);
        // process not found
        if (localByName.Length <= 0)
        {
            Console.WriteLine("ERROR: No process found");
            return;
        }

        // get handle of first process
        var handle = localByName[0].MainWindowHandle;

        // check iconic status
        if (IsIconic(handle))
            // show window
            ShowWindow(handle, SwRestore);
        // bring to front
        SetForegroundWindow(handle);
    }

    /// <summary>
    /// Brings window to foreground
    /// </summary>
    /// <param name="hWnd"></param>
    /// <returns></returns>
    [DllImport("User32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);

    /// <summary>
    /// Shows ionic given window
    /// </summary>
    /// <param name="handle"></param>
    /// <param name="nCmdShow"></param>
    /// <returns></returns>
    [DllImport("User32.dll")]
    private static extern bool ShowWindow(IntPtr handle, int nCmdShow);

    /// <summary>
    /// Check, given window is ionic
    /// </summary>
    /// <param name="handle"></param>
    /// <returns></returns>
    [DllImport("User32.dll")]
    private static extern bool IsIconic(IntPtr handle);
}
