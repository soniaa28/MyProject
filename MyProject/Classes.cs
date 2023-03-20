using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyProject
{
    class Bell
    {
        public delegate void BellHandler(string msgBell);
        public event BellHandler Lesson;
        public event BellHandler Break;


        private void onLesson(string msg)
        {

            Lesson?.Invoke(msg);
        }
        private void onBreak(string msg)
        {

            Break?.Invoke(msg);
        }


    }
    class Person
    {
        public string Name { get; private set; }
        public Person(string name)
        {
            this.Name = name;
        }
        public override string ToString()
        {
            return this.GetType().Name + ' ' + Name;
        }
    }
    class Student : Person
    {
        public int knowledge;
        public Student(string name, int _knowledge) : base(name) 
        {
            this.knowledge = knowledge;
        }

        public int Knowledge
        {
            get { return this.knowledge; }
            private set { this.knowledge = value; }
        }

        public void Study() { }

    }

    class Teacher : Person
    {
        public Teacher(string name) : base(name) { }

        public void Teach() { }
        public void setGradeToStudents() { }
    }

}
