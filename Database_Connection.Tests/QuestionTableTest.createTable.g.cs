using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatabaseConnection;
using Microsoft.Pex.Framework.Generated;
// <copyright file="QuestionTableTest.createTable.g.cs">Copyright ©  2017</copyright>
// <auto-generated>
// This file contains automatically generated tests.
// Do not modify this file manually.
// 
// If the contents of this file becomes outdated, you can delete it.
// For example, if it no longer compiles.
// </auto-generated>
using System;

namespace DatabaseConnection.Tests
{
    public partial class QuestionTableTest
    {

[TestMethod]
[PexGeneratedBy(typeof(QuestionTableTest))]
public void createTable230()
{
    using (PexTraceListenerContext.Create())
    {
      QuestionTable questionTable;
      bool b;
      questionTable = new QuestionTable();
      b = this.createTable(questionTable);
      Assert.AreEqual<bool>(false, b);
      Assert.IsNotNull((object)questionTable);
      Assert.AreEqual<string>("Failed to open database connection", 
                              ((DBTable)questionTable).errorMessage);
    }
}
    }
}
