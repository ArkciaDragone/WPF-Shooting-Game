using System;
using System.Windows;
using tanmak.Game;

namespace tanmak
{
    /// <summary>
    /// MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            bool on = false;

            while (on)
            {
                Console.Write(">>> ");

                string line = Console.ReadLine();

                string low = line.ToLower();

                if (low == "exit")
                {
                    on = false;
                }
                else if (low == "suiside")
                {
                    Convert.ToInt32("aaaaaaaaaaaaaa");
                }
                else if (low == "quit")
                {
                    Environment.Exit(0);
                }
                else if (low == "hello")
                {
                    Console.WriteLine("Hello, world");
                }
                else if (low == "fortest")
                {
                    ForTest();
                }
                else
                {
                    Console.WriteLine("Unknown Command : " + line);
                }
            }

            grid.Children.Add(new InGamePlane());
        }

        private void ForTest()
        {
            try
            {
                Console.Write("Set Count >>> ");

                int count = Convert.ToInt32(Console.ReadLine());

                //for ([];[];[])
                for (int i = 0; i < count; i++)
                {
                    string output = i.ToString();

                    Console.WriteLine(output);
                }
            }
            catch
            {
                Console.WriteLine("Errored");
            }
        }
    }
}
