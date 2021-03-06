// <copyright file="QuestionTableTest.cs">Copyright ©  2017</copyright>

using System;
using DatabaseConnection;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DatabaseConnection.Tests
{
    [TestClass]
    [PexClass(typeof(QuestionTable))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class QuestionTableTest
    {

        [PexMethod]
        public bool createTable([PexAssumeUnderTest]QuestionTable target)
        {
            bool result = target.createTable();
            return result;
            // TODO: add assertions to method QuestionTableTest.createTable(QuestionTable)
        }

        [PexMethod]
        public bool updateRecord([PexAssumeUnderTest]QuestionTable target, object record)
        {
            bool result = target.updateRecord(record);
            return result;
            // TODO: add assertions to method QuestionTableTest.updateRecord(QuestionTable, Object)
        }
    }
}
