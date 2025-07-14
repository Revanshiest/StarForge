using Xunit;
using System.Text.Json;
using System.IO;
using libraryClass;
using task13;

public class StudentJsonTests
{
    [Fact]
    public void SerializeStudent_IgnoresNulls_AndFormatsDate()
    {
        var student = new Student
        {
            FirstName = "Egor",
            BirthDate = new DateTime(2006, 11, 1)
        };
        
        var options = JsonStudentService.GetOptions();

        var json = JsonSerializer.Serialize(student, options);

        Assert.DoesNotContain("LastName", json);
        Assert.DoesNotContain("Grades", json);
        Assert.Contains("2006-11-01", json);
    }

    [Fact]
    public void DeserializeStudent_ValidJson_ReturnsCorrectObject()
    {
        var json = "{\"FirstName\":\"Egor\",\"LastName\":\"Mezhenov\",\"BirthDate\":\"2006-11-01\",\"Grades\":[{\"Name\":\"Math\",\"Grade\":5}]}";
        var options = JsonStudentService.GetOptions();
        var student = JsonSerializer.Deserialize<Student>(json, options);

        Assert.NotNull(student);
        Assert.Equal("Egor", student.FirstName);
        Assert.Equal("Mezhenov", student.LastName);
        Assert.Equal(new DateTime(2006, 11, 1), student.BirthDate);
        Assert.Single(student.Grades);
        Assert.Equal("Math", student.Grades[0].Name);
        Assert.Equal(5, student.Grades[0].Grade);
    }

    [Fact]
    public void SaveAndLoadStudentJson_FileRoundtrip_PreservesData()
    {
        var student = new Student
        {
            FirstName = "Egor",
            LastName = "Mezhenov",
            BirthDate = new DateTime(2006, 11, 1),
            Grades = new List<Subject> { new Subject { Name = "Math", Grade = 5 } }
        };
        var options = JsonStudentService.GetOptions();
        var json = JsonSerializer.Serialize(student, options);
        var file = Path.GetTempFileName();
        File.WriteAllText(file, json);
        var loadedJson = File.ReadAllText(file);
        var loadedStudent = JsonSerializer.Deserialize<Student>(loadedJson, options);

        Assert.NotNull(loadedStudent);
        Assert.Equal(student.FirstName, loadedStudent.FirstName);
        Assert.Equal(student.LastName, loadedStudent.LastName);
        Assert.Equal(student.BirthDate, loadedStudent.BirthDate);
        Assert.Single(loadedStudent.Grades);
        Assert.Equal("Math", loadedStudent.Grades[0].Name);
        Assert.Equal(5, loadedStudent.Grades[0].Grade);
        File.Delete(file);
    }

    [Fact]
    public void SerializeAndDeserializeStudent_WithAdditionalField_WorksCorrectly()
    {
        var student = new Student
        {
            FirstName = "Egor",
            LastName = "Mezhenov",
            Pogonyalo = "laser engraver",
            BirthDate = new DateTime(2006, 11, 1),
            Grades = new List<Subject> { new Subject { Name = "Math", Grade = 5 } }
        };

        var options = JsonStudentService.GetOptions();
        var json = JsonSerializer.Serialize(student, options);
        Assert.Contains("Pogonyalo", json);
        Assert.Contains("Mezhenov", json);
        
        var deserialized = JsonSerializer.Deserialize<Student>(json, options);
        Assert.NotNull(deserialized);
        Assert.Equal("Egor", deserialized.FirstName);
        Assert.Equal("Mezhenov", deserialized.LastName);
        Assert.Equal("laser engraver", deserialized.Pogonyalo);
        Assert.Equal(new DateTime(2006, 11, 1), deserialized.BirthDate);
        Assert.Single(deserialized.Grades);
        Assert.Equal("Math", deserialized.Grades[0].Name);
        Assert.Equal(5, deserialized.Grades[0].Grade);
    }
}

