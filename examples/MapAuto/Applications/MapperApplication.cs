
using Aspire;

using AutoMapper;

namespace MapAuto.Applications;

public class MapperApplication : ApplicationBase
{
    private readonly IAspireMapper mapper;

    public MapperApplication(IAspireMapper mapper)
    {
        this.mapper = mapper;
    }

    public object Get()
    {
        var a1 = new ClassA
        {
            Name = "Name111",
            Description = "Description222"
        };

        var b = mapper.MapTo<ClassB>(a1);

        var a2 = mapper.MapTo<ClassA>(b);

        return new
        {
            a1,
            b,
            a2
        };
    }
}

[MapTo(typeof(ClassB))]
public class ClassA
{
    public string? Name { get; set; }

    public string? Description { get; set; }
}

public class ClassB
{
    public string? Name { get; set; }

    public string? Title { get; set; }
}

public class ClassAProfile : Profile
{
    public ClassAProfile()
    {
        CreateMap<ClassA, ClassB>().ForMember(x => x.Title, x => x.MapFrom(m => m.Description));
        CreateMap<ClassB, ClassA>().ForMember(x => x.Description, x => x.MapFrom(m => m.Title));
    }
}