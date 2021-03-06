// <copyright file="DBTableTest.cs">Copyright ©  2017</copyright>

using System;
using DatabaseConnection;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DatabaseConnection.Tests
{
    [TestClass]
    [PexClass(typeof(DBTable))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class DBTableTest
    {

        [PexMethod]
        public bool createTable([PexAssumeNotNull]DBTable target)
        {
            bool result = target.createTable();
            return result;
            // TODO: add assertions to method DBTableTest.createTable(DBTable)
        }
    }
}
