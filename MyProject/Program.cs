using MyProject;
using System;
using System.Timers;
using Timer = System.Timers.Timer;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Bell bell = new Bell();

        Student alice = new Student("Соф'я",0 );
        Student bob = new Student ("Олександр",0 );
        Student charlie = new Student ("Юлія",0 );
        List<Student> groupa = new List<Student> { alice,bob, charlie };
        GroupOfStudents groupA = new GroupOfStudents(groupa);
        Teacher teacher = new Teacher("Сергій Адамович");
        foreach(Student student in groupA)
        {
            student.MaxNote += groupA.GraduateStudent;
        }
        bell.Lesson += teacher.GoToLesson;
        bell.Lesson += groupA.GoToLesson;
        groupA.Present += teacher.checkStudents;
        bell.Break +=teacher.GoToBreak;
        teacher.Finish += groupA.GoToBreak;

        for (int i = 0; i < 7; i++)
        {
            Console.WriteLine("\n=== === === ДЕНЬ " + (i + 1) + " РОЗПОЧАТО === === ===");
            Console.WriteLine("\nДззззззз!!!\n\n ------  Початок уроку " + (i+1) + "-------");
            bell.StartLesson();
            Console.WriteLine("\nДззззззз!!!\n\n-------  Дзвінок на перерву! ------");
            bell.StartBreak();
            Console.WriteLine("\n=== === === ДЕНЬ " + (i+1) + " ЗАВЕРШЕНО === === ===");
        }
    }

  
}