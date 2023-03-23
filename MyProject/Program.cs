using MyProject;
using System;
using System.Timers;
using Timer = System.Timers.Timer;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Bell bell = new Bell(); //creating object Bell 
        // creating Students
        Student alice = new Student("Соф'я",0 ); 
        Student bob = new Student ("Олександр",0 );
        Student charlie = new Student ("Юлія",0 );
       
        List<Student> groupa = new List<Student> { alice,bob, charlie };
        GroupOfStudents groupA = new GroupOfStudents(groupa); //creating a group

        Teacher teacher = new Teacher("Сергій Адамович");//creating a teacher 

        foreach (Student student in groupA)
        {
            student.MaxNote += groupA.GraduateStudent; // for subsribing on event MaxNote
        }
        bell.Lesson += teacher.GoToLesson; // subscribe teacher on Bell event 
        bell.Lesson += groupA.GoToLesson;// subscribe group on Bell event 
        groupA.Present += teacher.checkStudents; // subscribe teacher on StudentsGroup event 
        bell.Break +=teacher.GoToBreak;// subscribe teacher on Bell event 
        teacher.Finish += groupA.GoToBreak;// subscribe group on teacher event  about ending the class

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