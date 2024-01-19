using ProjectPP;
using UnitTestApp.Tests;
using Xunit;

namespace UnitTestApp.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void Parse()
        {
            string a = "6 + 5 / 7";
            string q = "657/+";

            string ac = Parser.ConvertToPostfix(a);

            Assert.Equal(q, ac);
        }

        [Fact]
        public void Parse2()
        {
            string a = "5 * (4- (3*(3-1)))";
            string q = "54331-*-*";

            string ac = Parser.ConvertToPostfix(a);

            Assert.Equal(q, ac);
        }

        [Fact]
        public void Parse3()
        {
            string a = "32/(-2+1)/2/2";
            string q = "322-1+/2/2/";

            string ac = Parser.ConvertToPostfix(a);

            Assert.Equal(q, ac);
        }

    }
}