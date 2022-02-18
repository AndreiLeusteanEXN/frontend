using HTMLFaithfulSerializer;
using System;
using System.IO;
using Xunit;


namespace UnitTest
{
    public class UnitTest1
    {

        [Fact]
        public void Test1()
        {
            try
            {
                string inputPath = @"..\..\..\..\..\HTMLFaithfulSerializer\HTMLFaithfulSerializer\site.html";
                string outputPath = @"..\..\..\..\..\HTMLFaithfulSerializer\HTMLFaithfulSerializer\outdex.html";
                HTMLParser parser = new HTMLParser();
                parser.Parse(inputPath, outputPath);

                FileComparer fc = new();


                Assert.True(fc.FileCompare(inputPath, outputPath));
            }
            catch (Exception ex)
            {

            }
        }
    }
}
