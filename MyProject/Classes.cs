using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static MyProject.Student;

namespace MyProject
{
   
    class Bell //class Bell has 2 event to inform about start of lesson and start of break
    {
        public delegate void BellEventHandler(object sender, EventArgs e);

        public event BellEventHandler Lesson;
        public event BellEventHandler Break;

        public void StartLesson() //method for сalling the event Lesson
        {
            OnLesson();
        }

        public void StartBreak() //method for сalling the event Break
        {
           
            OnBreak();
        }

        //dispetchers of events
        public void OnLesson()
        {
            Lesson?.Invoke(this, EventArgs.Empty);
        }

        public void OnBreak()
        {
            Break?.Invoke(this, EventArgs.Empty);
        }

    }
    class Person //base class Person
    {
        public string Name { get; private set; } //name for identification 
        public Person(string name)
        {
            this.Name = name;
        }
    }
    class Student : Person //class Student know how to react on events , has own event MaxNote to inform that he already passed the course and shoud quit
    {
        //annonce delegate and event
        public delegate void AmountEvent(object sender, EventArgs arg);
        public event AmountEvent MaxNote;

        public int knowledge; //field for saving the current note
        public Student(string name, int _knowledge) : base(name) // using constructor of Person to implement this constructor  
        {
            this.knowledge = _knowledge;
        }
        //property
        public int Knowledge
        {
            get { return this.knowledge; }
            set
            {
                if(value >20)
                {
                   OnMaxNote(); // call of the event MaxNote, because current note of Student is already more than 20
                }
                else 
                {
                    this.knowledge = value;
                }
            }
        }
        //dispetcher
        public void OnMaxNote()
        {
            MaxNote?.Invoke(this, EventArgs.Empty);
        }
    }
    class GroupOfStudents : IEnumerable
    {
        public delegate void PresentEventHandler(object sender, EventArgs e); 

        public event PresentEventHandler Present;//event to inform Teacher that Students are present in a class

        static string[] NAMES = { "Андрій", "Світлана", "Оксана", "Олена", "Остап", "Маркіян", "Генадій", "Олексій", "Данило" }; // array of names for new students

        List<Student> students; //list of students
     
        public GroupOfStudents(List<Student> students)
        {
            this.students = students;
            
        }
        public Student this[int index]  //indexator for accesing the student from list 
        {
            get => students[index];
            set=>students[index] = value;
        }
        public int CountGroup // property to have count of students for looping
        {
            get
            {
                return students.Count;
            }
        }
        //dispetcher
        public void IsPresent()
        {
            Present?.Invoke(this, EventArgs.Empty);
        }
        public void GoToLesson(object sender, EventArgs e) ////method if the lesson event occur
        {
            foreach (Student student in students)
            {
                Console.WriteLine(student.Name + " на уроці.");

            }
            IsPresent();
        }
        public void GoToBreak(string msg) // method if the FinishLesson event of Teacher occur
        {
            foreach (Student student in students)
            {
                Console.WriteLine(student.Name + " іде відпочивати на перерву.");

            }
        }

        public IEnumerator GetEnumerator() // realised interface IEnumerable for enumerating the students in loop
        {
            return ((IEnumerable)students).GetEnumerator();
        }

        public void GraduateStudent(object sender, EventArgs e) // method if the event MaxNote occur
        {
            Student student = sender as Student;
            students.Remove(student);
            Console.WriteLine("Студент " + student.Name + " завершив курс 'Програмування'");
            NewRandomStudent();
        }

        public void NewRandomStudent() // methos for adding a new Student 
        {
            Random random = new Random();
            int name = random.Next(0, NAMES.Length);
            Student newS = new Student(NAMES[name], 0);
            students.Add(newS);
            Console.WriteLine(" У нас новий студент " + newS.Name + "!Привітаємо його!");
        }
    }


        class Teacher : Person //class Teacher, knows how to react on events of Bell, knows how to inform the group of students about the ending of lesson
        {
            public delegate void FinishEventHandler(string msg);
            public event FinishEventHandler Finish; //event to inform about the end of the lesson

            private Random random = new Random();
            public Teacher(string name) : base(name) { } // constructor 

            public int GetRandomNote() // method generate a random note for setting it to Student
            {
                return random.Next(1, 10);
            }
            public void GoToLesson(object sender, EventArgs e) // if Lesson event occur
            {
                Console.WriteLine(Name + " іде на урок..");
            }
            public void GoToBreak(object sender, EventArgs e) // if Break event occur
            {
                FinishTheLesson();
                Console.WriteLine(Name + "  закінчив пояснювати матеріал та йде пити каву у свій кабінет.");
            }
            public void setGradeToStudents(GroupOfStudents group) // method for setting a random note for each student
            {
                for(int i = 0; i < group.CountGroup; i++)
                {
                    int temp_note = GetRandomNote();
                    group[i].Knowledge += temp_note;
                    Console.WriteLine(string.Format("{0} поставив оцінку {1} студенту {2} , тепер його поточна оцінка = {3}", this.Name, temp_note, group[i].Name, group[i].Knowledge));
                }
            }
            public void checkStudents(object sender, EventArgs e) // check the presense and set notes
            {
                GroupOfStudents group = sender as GroupOfStudents;
                setGradeToStudents(group);
            }
             //dispetcher
            public void FinishTheLesson() 
            {
                Finish?.Invoke("Урок завершено!");
            }
    }

}


