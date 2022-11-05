using Isu.Extra.Entities;
using Isu.Extra.Models;

namespace Isu.Extra.Services.UniversityClassBuilder;

public interface IUniversityClassBuilder
{
    public IUniversityClassBuilder WithName(string name);
    public IUniversityClassBuilder WithTime(ClassTime time);
    public IUniversityClassBuilder WithTeacher(Teacher teacher);
    public IUniversityClassBuilder WithAddress(ClassAddress address);
    public UniversityClass Build();
}