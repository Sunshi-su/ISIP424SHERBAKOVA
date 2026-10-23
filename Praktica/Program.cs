using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization; //это для фио (лучше не спрашивать)

namespace Praktica
{
    internal class Program
    {
        

        static void Main(string[] args)
        {
            var students = new List<Student>();
            var courses = new List<Course>();
            var teachers = new List<Teacher>();

            bool k = true;
            while (k)
            {
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("1. Добавить нового студента");
                Console.WriteLine("2. Добавить нового преподавателя");
                Console.WriteLine("3. Создать новый курс");
                Console.WriteLine("4. Записать студента на курс");
                Console.WriteLine("5. Назначить преподавателя на курс");
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("6. Вывести статистику по выбранному студенту");
                Console.WriteLine("7. Вывести статистику по выбранному преподавателю");
                Console.WriteLine("8. Вывести статистику по выбранному курсу");
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("9. Вывести статистику о всех студентах");
                Console.WriteLine("10. Вывести статистику о всех преподавателях");
                Console.WriteLine("11. Вывести статистику о всех курсах");
                Console.WriteLine("0. Выход");

                string menu = Console.ReadLine();

                if (!int.TryParse(menu, out int choice))
                {
                    Console.WriteLine("Нужно ввести число!");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        AddStudent(students);
                        
                        break;

                    case 2:
                        AddTeacher(teachers);
                        break;
                    case 3:
                        AddCourse(courses, teachers);
                        break;
                    case 4:
                        EnrollStudent(students, courses);
                        break;
                    case 5:
                        AssignTeacher(teachers, courses);
                        break;
                    case 6:
                        if (students.Count == 0)
                        {
                            Console.WriteLine("Студентов пока нет!");
                            break;
                        }
                        ShowStudents(students);
                        string num = Console.ReadLine();
                        if (int.TryParse(num, out int numStudent) && numStudent >= 1 && numStudent <= students.Count)
                        {
                            ShowPeople(students[numStudent - 1]);
                            students[numStudent - 1].Info();
                        }
                        else
                        {
                            Console.WriteLine("Такого студента нет!");
                        }
                        break;
                    case 7:
                        if (teachers.Count == 0)
                        {
                            Console.WriteLine("Преподавателей пока нет!");
                            break;
                        }
                        ShowTeachers(teachers);
                        int numTeacher = ReadNumber("Введите номер преподавателя:", 1, teachers.Count);
                        ShowPeople(teachers[numTeacher - 1]);
                        teachers[numTeacher - 1].Info();
                        break;
                    case 8:
                        if (courses.Count == 0)
                        {
                            Console.WriteLine("Курсов пока нет!");
                            break;
                        }
                        ShowCourses(courses);
                        int numCourse = ReadNumber("Введите номер курса:", 1, courses.Count);
                        courses[numCourse - 1].InfoCourse();
                        courses[numCourse - 1].InfoGroupStudents();
                        break;
                    case 9:
                        if (students.Count == 0) Console.WriteLine("Студентов пока нет!");
                        foreach (var stud in students)
                        {
                            ShowPeople(stud);
                            stud.Info();
                        }
                        break;
                    case 10:
                        if (teachers.Count == 0) Console.WriteLine("Преподавателей пока нет!");
                        foreach (var teach in teachers)
                        {
                            ShowPeople(teach);
                            teach.Info();
                        }
                        break;
                    case 11:
                        if (courses.Count == 0) Console.WriteLine("Курсов пока нет!");
                        foreach (var cor in courses)
                        {
                            cor.InfoCourse();
                            cor.InfoGroupStudents();
                        }
                        break;
                    case 0:
                        k = false;
                        break;

                    default:
                        Console.WriteLine("Неверный пункт меню");
                        break;
                }
            }
        }

        enum Gend
        {
            Женский,
            Мужской
        }

        abstract class People
        {
            public  string FIO { get; private set; }
            public int age{ get; private set;  }
            public string phoneNumber { get; private set; }
            public string passport{ get; private set; }
            public Gend gender { get; private set; }

            protected People(string FIO, int Age, string PhoneNumber, string Passport, Gend gender)
            {
                this.FIO = FIO;
                age = Age;
                phoneNumber = PhoneNumber;
                passport = Passport;
                this.gender = gender;
            }

            public abstract void Info();
        }

        class Course
        {
            public string Namecourse { get; private set; }
            public string info{ get; private set; }
            public int hours { get; private set; }
            public Teacher Teacher { get; private set; }
            public List<Student> Students { get; private set; } = new List<Student>();

            public Course(Teacher Teacher,  string Namecourse, int Hours, string info)
            {
                this.hours = Hours;
                this.Namecourse = Namecourse;
                this.Teacher = Teacher;
                this.info = info;
            }

            public void SetTeacher(Teacher Teacher)
            {
                if (this.Teacher != null)
                {
                    this.Teacher.Courses.Remove(this);
                }
                this.Teacher = Teacher;
                if (!Teacher.Courses.Contains(this))
                {
                    Teacher.Courses.Add(this);
                }
            }

            public void InfoCourse()
            {
                Console.WriteLine($"Название курса:{Namecourse} \n Описание: {info}  \n Количество часов: {hours}  \n Преподаватель: {Teacher}  ");
            }

            public void InfoGroupStudents()
            {
                Console.WriteLine("Список студентов записанных на курс:");
                foreach (var peop in Students)
                {
                    Console.WriteLine($"{Students.IndexOf(peop) + 1}. {peop.FIO}");
                }
            }
        }

        class Teacher : People
        {
            public int Experience  { get; private set; }
            public List<Course> Courses { get; private set; } = new List<Course>();
            
            public Teacher(string FIO, int Age, string PhoneNumber, string Passport, Gend gender, int experience) : base(FIO, Age, PhoneNumber,
                Passport,  gender)
            {
                Experience = experience;
            }

            public override string ToString()
            {
                return FIO;
            }

            public override void Info()
            {
                Console.WriteLine();
                Console.WriteLine($"Курсы ведет:");
                foreach (var k in Courses)
                {
                    Console.WriteLine($"{k.Namecourse}");
                }
            }
        }

        class Student : People
        {
            public int Curs { get; private set; }
            public string Group { get; private set; }
            public List<Course> Courses { get; private set; } = new List<Course>();
            public Student(string FIO, int Age, string PhoneNumber, string Passport, Gend gender, int curs, string group) : base(FIO, Age, PhoneNumber, Passport,  gender)
            {
                Curs = curs;
                Group = group;
            }

            public void SetCourses(Course Course)
            {
                Courses.Add(Course);
            }
            
            public override void Info()
            {
                Console.WriteLine($"Курс: {Curs} \n Группа: {Group} \n Предметы(курсы):");
                foreach (var cor in Courses)
                {
                    Console.WriteLine($"{Courses.IndexOf(cor)+1}. {cor.Namecourse}");
                }
            }
        }

        static void ShowStudents( List<Student>  students)
        {
            Console.WriteLine($"Выберите студента:");
            foreach (var stud in students)
            {
                Console.WriteLine($"{students.IndexOf(stud)+1}) {stud.FIO}");
            }
        }

        static void AddStudent(List<Student> students)
        {
            Console.WriteLine($"Введите Фамилию Имя Отчество студента:");
            string fio = Console.ReadLine();
            if (fio is not null)
            {
                fio = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fio.ToLower());
            }
            else return;
            Console.WriteLine($"Введите количество полных лет студента:");
            string age = Console.ReadLine();
            if (int.TryParse(age, out int ageInt))
            {
                if (ageInt < 16 || ageInt > 100)
                {
                    Console.WriteLine("Возраст должен быть от 16 до 100 лет!");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Нужно ввести число!");
                return;
            }
            if (string.IsNullOrWhiteSpace(fio))
            {
                Console.WriteLine("ФИО не может быть пустым!");
                return;
            }

            string phoneNumber = ReadPhone();
            string passport = ReadPassport();
            Gend gender = ReadGender();
            int curs = ReadNumber("Введите курс обучения (1-6):", 1, 6);
            string group = ReadText("Введите группу студента:");
            students.Add(new Student(fio, ageInt, phoneNumber, passport, gender, curs, group));
            Console.WriteLine("Студент добавлен!");
        }

        static string ReadText(string text)
        {
            while (true)
            {
                Console.WriteLine(text);
                string value = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(value))
                {
                    return value.Trim();
                }
                Console.WriteLine("Поле не может быть пустым!");
            }
        }

        static int ReadNumber(string text, int min, int max)
        {
            while (true)
            {
                Console.WriteLine(text);
                string num = Console.ReadLine();
                if (!int.TryParse(num, out int number))
                {
                    Console.WriteLine("Нужно ввести число!");
                    continue;
                }
                if (number < min || number > max)
                {
                    Console.WriteLine($"Введите число от {min} до {max}!");
                    continue;
                }
                return number;
            }
        }

        static string ReadPhone()
        {
            while (true)
            {
                string phoneNumber = ReadText("Введите номер телефона (11 цифр, начинается с 7, 8 или +7):");
                string num = phoneNumber.StartsWith("+") ? phoneNumber.Substring(1) : phoneNumber;
                if (num.Length == 11 && num.All(c => c >= '0' && c <= '9') && (phoneNumber.StartsWith("+7") || phoneNumber.StartsWith("7") || phoneNumber.StartsWith("8")))
                {
                    return phoneNumber;
                }
                Console.WriteLine("Ошибка! Неверно введен номер");
            }
        }

        static string ReadPassport()
        {
            while (true)
            {
                string passport = ReadText("Введите серию и номер паспорта (10 цифр без пробелов):");
                if (passport.Length == 10 && passport.All(c => c >= '0' && c <= '9'))
                {
                    return passport;
                }
                Console.WriteLine("Паспорт должен содержать 10 цифр!");
            }
        }

        static Gend ReadGender()
        {
            int gender = ReadNumber("Выберите пол: 1. Женский; 2. Мужской", 1, 2);
            return (Gend)(gender - 1);
        }

        static void AddTeacher(List<Teacher> teachers)
        {
            string fio = ReadText("Введите Фамилию Имя Отчество преподавателя:");
            fio = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(fio.ToLower());
            int ageInt = ReadNumber("Введите количество полных лет преподавателя (21-100):", 21, 100);
            string phoneNumber = ReadPhone();
            string passport = ReadPassport();
            Gend gender = ReadGender();
            int experience = ReadNumber("Введите стаж работы в полных годах:", 0, ageInt - 18);
            teachers.Add(new Teacher(fio, ageInt, phoneNumber, passport, gender, experience));
            Console.WriteLine("Преподаватель добавлен!");
        }

        static void AddCourse(List<Course> courses, List<Teacher> teachers)
        {
            if (teachers.Count == 0)
            {
                Console.WriteLine("Сначала добавьте преподавателя!");
                return;
            }
            string Namecourse = ReadText("Введите название курса:");
            string info = ReadText("Введите описание курса:");
            int hours = ReadNumber("Введите количество часов:", 1, int.MaxValue);
            ShowTeachers(teachers);
            int numTeacher = ReadNumber("Введите номер преподавателя:", 1, teachers.Count);
            var Teacher = teachers[numTeacher - 1];
            var Course = new Course(Teacher, Namecourse, hours, info);
            courses.Add(Course);
            Teacher.Courses.Add(Course);
            Console.WriteLine("Курс создан!");
        }

        static void EnrollStudent(List<Student> students, List<Course> courses)
        {
            if (students.Count == 0 || courses.Count == 0)
            {
                Console.WriteLine("Сначала добавьте студента и создайте курс!");
                return;
            }
            ShowStudents(students);
            int numStudent = ReadNumber("Введите номер студента:", 1, students.Count);
            ShowCourses(courses);
            int numCourse = ReadNumber("Введите номер курса:", 1, courses.Count);
            var stud = students[numStudent - 1];
            var cor = courses[numCourse - 1];
            if (stud.Courses.Contains(cor))
            {
                Console.WriteLine("Студент уже записан на этот курс!");
                return;
            }
            stud.SetCourses(cor);
            cor.Students.Add(stud);
            Console.WriteLine("Студент записан на курс!");
        }

        static void AssignTeacher(List<Teacher> teachers, List<Course> courses)
        {
            if (teachers.Count == 0 || courses.Count == 0)
            {
                Console.WriteLine("Сначала добавьте преподавателя и создайте курс!");
                return;
            }
            ShowTeachers(teachers);
            int numTeacher = ReadNumber("Введите номер преподавателя:", 1, teachers.Count);
            ShowCourses(courses);
            int numCourse = ReadNumber("Введите номер курса:", 1, courses.Count);
            courses[numCourse - 1].SetTeacher(teachers[numTeacher - 1]);
            Console.WriteLine("Преподаватель назначен на курс!");
        }

        static void ShowTeachers(List<Teacher> teachers)
        {
            Console.WriteLine("Выберите преподавателя:");
            foreach (var teach in teachers)
            {
                Console.WriteLine($"{teachers.IndexOf(teach) + 1}) {teach.FIO}");
            }
        }

        static void ShowCourses(List<Course> courses)
        {
            Console.WriteLine("Выберите курс:");
            foreach (var cor in courses)
            {
                Console.WriteLine($"{courses.IndexOf(cor) + 1}) {cor.Namecourse}");
            }
        }

        static void ShowPeople(People peop)
        {
            Console.WriteLine($"ФИО: {peop.FIO} \n Возраст: {peop.age} \n Телефон: {peop.phoneNumber} \n Паспорт: {peop.passport} \n Пол: {peop.gender}");
            if (peop is Teacher teach)
            {
                Console.WriteLine($"Стаж работы: {teach.Experience}");
            }
        }
    }
}




//S — каждый класс занимается своим делом.
// O — добавляем возможности, не переписывая работающий код.
// L — наследник заменяет родителя без поломок.
// I — интерфейсы содержат только нужные методы.
// D — важные функции зависимы от интерфейсов, а не от конкретных классов.