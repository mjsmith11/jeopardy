using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatabaseConnection;
using Microsoft.Pex.Framework.Generated;
// <copyright file="HighScoresTableTest.createTable.g.cs">Copyright ©  2017</copyright>
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
    public partial class HighScoresTableTest
    {

[TestMethod]
[PexGeneratedBy(typeof(HighScoresTableTest))]
public void createTable278()
{
    using (PexTraceListenerContext.Create())
    {
      HighScoresTable highScoresTable;
      bool b;
      highScoresTable = new HighScoresTable();
      b = this.createTable(highScoresTable);
      Assert.AreEqual<bool>(false, b);
      Assert.IsNotNull((object)highScoresTable);
      Assert.AreEqual<string>("Failed to open database connection", 
                              ((DBTable)highScoresTable).errorMessage);
    }
}
    }
}
