// <copyright file="HighScoresTableTest.cs">Copyright ©  2017</copyright>

using System;
using DatabaseConnection;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DatabaseConnection.Tests
{
    [TestClass]
    [PexClass(typeof(HighScoresTable))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class HighScoresTableTest
    {

        [PexMethod]
        public bool createTable([PexAssumeUnderTest]HighScoresTable target)
        {
            bool result = target.createTable();
            return result;
            // TODO: add assertions to method HighScoresTableTest.createTable(HighScoresTable)
        }
    }
}
