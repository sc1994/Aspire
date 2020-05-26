using Aspire.Map;
using System;
using System.Linq;
using Xunit;

namespace Aspire.Test.Map
{
    public class AspireMapper_Test
    {
        private readonly IAspireMapper _mapper;
        public AspireMapper_Test(IAspireMapper mapper)
        {
            _mapper = mapper;
        }

        public virtual void Single_Map_To_New_Test()
        {
            var a = new MapEntity_Test
            {
                Age = 10,
                DateOfBirth = new DateTime(2020, 5, 1, 10, 0, 0), // 2020-05-01 10:00:00
                Gender = 1,
                Name = "swzoxa"
            };
            var b = _mapper.To<MapDto_Test>(a);
            Assert.Equal(a.Age, b.Age);
            Assert.Equal(a.Name, b.Name);
            Assert.Equal(a.DateOfBirth.ToString("yyyy-MM-dd HH:mm:ss"), b.Birthday);
            Assert.Equal(a.Gender, b.Gender.GetHashCode());
        }

        public virtual void Single_Map_To_Exist_Test()
        {
            var a = new MapEntity_Test
            {
                Age = 10,
                DateOfBirth = new DateTime(2020, 5, 1, 10, 0, 0), // 2020-05-01 10:00:00
                Gender = 1,
                Name = "swzoxa"
            };
            var b = new MapDto_Test()
            {
                Age = 20
            };
            _mapper.To(a, b);
            Assert.Equal(20, b.Age);
            Assert.Equal(a.Name, b.Name);
        }

        public virtual void List_Map_To_New_Test()
        {
            var a = new[]
            {
                new MapEntity_Test
                {
                    Age = 10,
                    DateOfBirth = new DateTime(2020, 5, 1, 10, 0, 0), // 2020-05-01 10:00:00
                    Gender = 1,
                    Name = "swzoxa"
                },
                new MapEntity_Test
                {
                    Age = 20,
                    DateOfBirth = new DateTime(4040, 5, 1, 10, 0, 0), // 4040-05-01 10:00:00
                    Gender = 2,
                    Name = "swzoxaswzoxa"
                },
            };
            var b = _mapper.To<MapDto_Test>(a);
            foreach (var item in b)
            {
                Assert.Contains(a, x => x.Age == item.Age);
                Assert.Contains(a, x => x.Name == item.Name);
                Assert.Contains(a, x => x.DateOfBirth.ToString("yyyy-MM-dd HH:mm:ss") == item.Birthday);
                Assert.Contains(a, x => x.Gender == item.Gender.GetHashCode());
            }
        }

    }
}
