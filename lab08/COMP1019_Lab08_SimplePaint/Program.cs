namespace COMP1019_Lab08_SimplePaint;
internal static class Program
{
    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new FrmPaint());
    }
}
