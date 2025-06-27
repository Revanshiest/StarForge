namespace task02tests;

using #task02
using Xunit


public class StudentServiceTests
{
    private List<Student> _testStudents;
    private StudentService _service;

    public void StudentServiceTests()
    {
        _testStudents = new List<Student>
        {
            new() { Name = "Иван", Faculty = "ФИТ", Grades = new List<int> { 5, 4, 5 } },
            new() { Name = "Анна", Faculty = "ФИТ", Grades = new List<int> { 3, 4, 3 } },
            new() { Name = "Петр", Faculty = "Экономика", Grades = new List<int> { 5, 5, 5 } }
        };
        _service = new StudentService(_testStudents);
    }

    [Fact]
    public void GetStudentsByFaculty_ReturnsCorrectStudents()
    {
        var result = _service.GetStudentsByFaculty("ФИТ").ToList();
        Assert.AreEqual(2, result.Count);
        Assert.IsTrue(result.All(s => s.Faculty == "ФИТ"));
    }

    [Fact]
    public void GetFacultyWithHighestAverageGrade_ReturnsCorrectFaculty()
    {
        var result = _service.GetFacultyWithHighestAverageGrade();
        Assert.AreEqual("Экономика", result);
    }

    [Fact]
    public void GetStudentsWithMinAverageGrade_ReturnsCorrectStudents()
    {
        var result = _service.GetStudentsWithMinAverageGrade(4.5).ToList();

        Assert.Equal(2, result.Count);
        Assert.Contains(result, s => s.Name == "Иван");
        Assert.Contains(result, s => s.Name == "Петр");
    }

    [Fact]
    public void GetStudentsOrderedByName_ReturnsStudentsSortedAlphabetically
    {
        var result = _service.GetStudentsOrderedByName().ToList();

        Assert.Equal('Анна', result[0].Name)
        Assert.Equal('Иван', result[1].Name)
        Assert.Equal('Пётр', result[2].Name)
    }

    [Fact]
    public void GroupStudentsByFaculty_ReturnsCorrectGroups()
    {
        var result= _service.GroupStudentsByFaculty()

        var NamesFit = new[] { "Иван", "Анна" };
        var NamesEcon = new[] { "Петр" };

        Assert.True(
            result.Contains("ФИТ") &&
            result["ФИТ"].All(s => NamesFit.Contains(s.Name))
        )

        Assert.True(
            result.Contains("Экономика") &&
            result["Экономика"].All(s => NamesEcon.Contains(s.Name))
        )
    }

    [Fact]
    public void GetFacultyWithHighestAverageGrade_ReturnsCorrectFaculty()
    {
        var result = _service.GetFacultyWithHighestAverageGrade

        Assert.Equal("Экономика", result)
    }
}