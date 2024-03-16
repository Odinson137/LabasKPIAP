




var subject1 = new Subject
{
    Title = "Математика",
    StartYear = 1,
    Description = "Цираца всех наук",
};

var subject2 = new Subject
{
    Title = "Физика",
    StartYear = 7,
    Description = "Презирает математику",
};

var subject3 = new Subject
{
    Title = "Английский язык",
    StartYear = 4,
    Description = "Самый важный язык программирования",
};

var subject4 = new Subject
{
    Title = "КПИЯП",
    StartYear = 10,
    Description = "Самый важный предмет для жизни",
};

var teacher1 = new Teacher
{
    Name = "Саша",
    LastName = "Багинский",
    Qualification = Education.Looser,
    YearExperience = 0,
    Subjects = new List<Subject>()
    {
        subject1,
        subject3,
    }
};

var teacher2 = new Teacher
{
    Name = "Иван",
    LastName = "Викторович",
    Qualification = Education.RespublicationLevel,
    YearExperience = 100,
    Subjects = new List<Subject>()
    {
        subject1,
        subject2,
        subject3,
        subject4,
    }
};

var teacher3 = new Teacher
{
    Name = "Яна",
    LastName = "Болейша",
    Qualification = Education.Higher,
    YearExperience = 2,
    Subjects = new List<Subject>()
    {
        subject2,
    }
};

var teachers = new List<Teacher>()
{
    teacher1, teacher2, teacher3,
};

var mathTeacherNames = teachers
    .Where(c => c.Subjects.Select(c => c.Title).Contains("Математика"))
    .Select(c => new {Name = c.Name, LastName = c.LastName})
    .ToList();

Console.WriteLine("Преподаватели, которые ведут математику:");

foreach (var mathteacherName in mathTeacherNames)
{
    Console.WriteLine($"{mathteacherName.Name} - {mathteacherName.LastName}");
}

var allSubjects = teachers.SelectMany(c => c.Subjects).Select(c => c.Title).Distinct().ToList();

Console.WriteLine("Список всех предметов:");

foreach (var subject in allSubjects)
{
    Console.WriteLine(subject);
}

class Teacher
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string Name { get; set; }
    public required string LastName { get; set; }
    public Education Qualification { get; set; }
    public int YearExperience { get; set; }
    public ICollection<Subject> Subjects { get; set; } = null!;
}

public class Subject 
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string Title { get; set; }
    public required int StartYear { get; set; }
    public required string Description { get; set; }
}

public enum Education
{
    Looser,
    Higher,
    RespublicationLevel
}