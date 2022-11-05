using Isu.Extra.Entities;
using Isu.Extra.Models;

namespace Isu.Extra.Services.UniversityClassBuilder;

public class UniversityClassBuilder : IUniversityClassBuilder
{
    private string _name;
    private Teacher _teacher;
    private ClassTime _classTime;
    private ClassAddress _address;

    public UniversityClassBuilder()
    {
        _name = null;
        _teacher = null;
        _classTime = null;
        _address = null;
    }

    public IUniversityClassBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public IUniversityClassBuilder WithTime(ClassTime time)
    {
        _classTime = time;
        return this;
    }

    public IUniversityClassBuilder WithTeacher(Teacher teacher)
    {
        _teacher = teacher;
        return this;
    }

    public IUniversityClassBuilder WithAddress(ClassAddress address)
    {
        _address = address;
        return this;
    }

    public UniversityClass Build()
    {
        return new UniversityClass(
            _name,
            _teacher,
            _classTime,
            _address);
    }
}
