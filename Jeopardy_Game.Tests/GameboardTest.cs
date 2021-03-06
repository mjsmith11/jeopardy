// <copyright file="GameboardTest.cs">Copyright ©  2017</copyright>

using System;
using Jeopardy_Game;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jeopardy_Game.Tests
{
    [TestClass]
    [PexClass(typeof(Gameboard))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class GameboardTest
    {

        [PexMethod]
        public Gameboard Constructor()
        {
            Gameboard target = new Gameboard();
            return target;
            // TODO: add assertions to method GameboardTest.Constructor()
        }

        [PexMethod]
        public bool SetupNextRound([PexAssumeUnderTest]Gameboard target)
        {
            bool result = target.SetupNextRound();
            return result;
            // TODO: add assertions to method GameboardTest.SetupNextRound(Gameboard)
        }
    }
}
