using MyProject;
using System;
using System.Timers;
using Timer = System.Timers.Timer;

class Program
{
    static void Main()
    {
        Bell bell = new Bell();                                                                    
        // Create a timer object
        Timer timer = new Timer();
        timer.Interval = TimeSpan.FromSeconds(5).TotalMilliseconds; // Change day to night every 24 hours
        timer.Elapsed += Bell.Lesson;
        timer.Start();

        // Wait for user input to exit the program
        Console.WriteLine("Press any key to exit");
        Console.ReadKey();
    }

  
}