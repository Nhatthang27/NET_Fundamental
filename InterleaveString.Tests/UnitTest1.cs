namespace InterleaveString.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void TestInterleaveStrings_SameLength()
        {
            string a = "abcd";
            string b = "efgh";
            string expected = "aebfcgdh";
            string result = Program.InterleaveStrings(a, b);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestInterleaveStrings_DifferentLength_A()
        {
            string a = "abc";
            string b = "defgh";
            string expected = "adbecfgh";
            string result = Program.InterleaveStrings(a, b);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestInterleaveStrings_DifferentLength_B()
        {
            string a = "abcd";
            string b = "ef";
            string expected = "aebfcd";
            string result = Program.InterleaveStrings(a, b);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestInterleaveStrings_EmptyA()
        {
            string a = "";
            string b = "efgh";
            string expected = "efgh";
            string result = Program.InterleaveStrings(a, b);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestInterleaveStrings_EmptyB()
        {
            string a = "abcd";
            string b = "";
            string expected = "abcd";
            string result = Program.InterleaveStrings(a, b);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void TestInterleaveStrings_BothEmpty()
        {
            string a = "";
            string b = "";
            string expected = "";
            string result = Program.InterleaveStrings(a, b);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test1()
        {
            // Arrange
            string a = "CLinh";
            string b = "Nhatthang";
            string expected = "CNLhianththang";

            // Act
            string result = Program.InterleaveStrings(a, b);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test2()
        {
            // Arrange
            string a = "abc";
            string b = "123";
            string expected = "a1b2c3";

            // Act
            string result = Program.InterleaveStrings(a, b);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test3()
        {
            // Arrange
            string a = "short";
            string b = "longerstring";
            string expected = "slhoonrgterstring";


            // Act
            string result = Program.InterleaveStrings(a, b);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test4()
        {
            // Arrange
            string a = "";
            string b = "nonempty";
            string expected = "nonempty";

            // Act
            string result = Program.InterleaveStrings(a, b);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test5()
        {
            // Arrange
            string a = "nonempty";
            string b = "";
            string expected = "nonempty";

            // Act
            string result = Program.InterleaveStrings(a, b);

            // Assert
            Assert.Equal(expected, result);
        }
    }
}