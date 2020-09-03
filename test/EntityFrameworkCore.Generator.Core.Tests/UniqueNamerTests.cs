using Xunit;
using FluentAssertions;
using EntityFrameworkCore.Generator;

public class UniqueNamerTests
{
    [Fact]
    public void CreatesUniqueClassNameWithoutNamespace()
    {
        var uniqueNamer = new UniqueNamer();
        var name = uniqueNamer.UniqueClassName("Class");
        name.Should().Be("Class");
    }
    
    [Fact]
    public void AppendsNumbersToDuplicateClassNames()
    {
        var uniqueNamer = new UniqueNamer();
        var firstName = uniqueNamer.UniqueClassName("Class");
        var secondName = uniqueNamer.UniqueClassName("Class");
        firstName.Should().Be("Class");
        secondName.Should().Be("Class1");
    }
    
    [Fact]
    public void CreatesUniqueClassNameWithNamespace()
    {
        var uniqueNamer = new UniqueNamer();
        var name = uniqueNamer.UniqueClassName("Namespace", "Class");
        name.Should().Be("Class");
    }
    
    [Fact]
    public void UsesSameClassNamesWithDifferentNamespaces()
    {
        var uniqueNamer = new UniqueNamer();
        var firstName = uniqueNamer.UniqueClassName("FirstNamespace", "Class");
        var secondName = uniqueNamer.UniqueClassName("SecondNamespace", "Class");
        firstName.Should().Be("Class");
        secondName.Should().Be("Class");
    }
    
    [Fact]
    public void AppendsNumbersToDuplicateClassNamesWithSameNamespace()
    {
        var uniqueNamer = new UniqueNamer();
        var firstName = uniqueNamer.UniqueClassName("FirstNamespace", "Class");
        var secondName = uniqueNamer.UniqueClassName("FirstNamespace", "Class");
        firstName.Should().Be("Class");
        secondName.Should().Be("Class1");
    }

    [Fact]
    public void CreatesUniqueContextNameWithoutNamespace()
    {
        var uniqueNamer = new UniqueNamer();
        var name = uniqueNamer.UniqueContextName("Class");
        name.Should().Be("Class");
    }
    
    [Fact]
    public void AppendsNumbersToDuplicateContextNames()
    {
        var uniqueNamer = new UniqueNamer();
        var firstName = uniqueNamer.UniqueContextName("Class");
        var secondName = uniqueNamer.UniqueContextName("Class");
        firstName.Should().Be("Class");
        secondName.Should().Be("Class1");
    }
    
    [Fact]
    public void CreatesUniqueContextNameWithNamespace()
    {
        var uniqueNamer = new UniqueNamer();
        var name = uniqueNamer.UniqueContextName("Namespace", "Class");
        name.Should().Be("Class");
    }
}