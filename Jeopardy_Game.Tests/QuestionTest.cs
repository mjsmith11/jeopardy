// <copyright file="QuestionTest.cs">Copyright ©  2017</copyright>

using System;
using Jeopardy_Game;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jeopardy_Game.Tests
{
    [TestClass]
    [PexClass(typeof(Question))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class QuestionTest
    {

        [PexMethod]
        public int getValueForScoring([PexAssumeUnderTest]Question target)
        {
            int result = target.getValueForScoring();
            return result;
            // TODO: add assertions to method QuestionTest.getValueForScoring(Question)
        }
    }
}
