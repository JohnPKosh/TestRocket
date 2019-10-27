using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Abstractions;

using SRF.Core;


namespace XunitZipTests
{
    public class SRFTests
    {
        private readonly ITestOutputHelper output;

        public SRFTests(ITestOutputHelper output)
        {
            this.output = output;
        }


        [Fact]
        public void CanGetLogDirectoryName_True()
        {
            var got = LibraryInfo.LogDirectoryName;
            output.WriteLine(got);
            Assert.NotNull(got);
        }


        [Fact]
        public void CanGetLogDirectoryExists_True()
        {
            var got = LibraryInfo.LogDirectoryExists;
            output.WriteLine(got.ToString());
            Assert.NotNull(got.ToString());
        }
    }
}
