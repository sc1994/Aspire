using Aspire.Test.Map;

using AutoMapper;

using Xunit;

namespace Aspire.AutoMapper.Test
{
    public class AspireAutoMapper_Test : AspireMapper_Test
    {
        public AspireAutoMapper_Test() : base(new AspireMapper(new MapperConfiguration(x => x.AddProfile<Profile_Test>()).CreateMapper()))
        {

        }

        [Fact]
        public override void List_Map_To_New_Test()
           => base.List_Map_To_New_Test();
        [Fact]
        public override void Single_Map_To_Exist_Test()
            => base.Single_Map_To_Exist_Test();
        [Fact]
        public override void Single_Map_To_New_Test()
            => base.Single_Map_To_New_Test();
    }
}
