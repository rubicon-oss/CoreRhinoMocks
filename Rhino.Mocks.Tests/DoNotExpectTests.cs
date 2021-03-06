#region license
// Copyright (c) 2020 rubicon IT GmbH, www.rubicon.eu
// Copyright (c) 2005 - 2009 Ayende Rahien (ayende@ayende.com)
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without modification,
// are permitted provided that the following conditions are met:
// 
//     * Redistributions of source code must retain the above copyright notice,
//     this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright notice,
//     this list of conditions and the following disclaimer in the documentation
//     and/or other materials provided with the distribution.
//     * Neither the name of Ayende Rahien nor the names of its
//     contributors may be used to endorse or promote products derived from this
//     software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE
// FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
// CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
// OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
// THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
#endregion

using Rhino.Mocks.Exceptions;

namespace Rhino.Mocks.Tests
{
    using System;
    using NUnit.Framework;

    
    public class DoNotExpectTests
    {
        private MockRepository mocks;
        private IDemo demo;

        [SetUp]
        public void SetUp()
        {
            mocks = new MockRepository();
            demo = mocks.DynamicMock<IDemo>();
        }

        [Test]
        public void ShouldNotExpect()
        {
            DoNotExpect.Call(demo.StringArgString("Ayende"));
            mocks.ReplayAll();
            Assert.Throws<ExpectationViolationException> (
                () => demo.StringArgString ("Ayende"),
                "IDemo.StringArgString(\"Ayende\"); Expected #0, Actual #1.");
        }

        [Test]
        public void CanUseAnonymousDelegatesToCallVoidMethods()
        {
            DoNotExpect.Call(delegate { demo.VoidNoArgs(); });
            mocks.ReplayAll();
            Assert.Throws<ExpectationViolationException> (
                () => demo.VoidNoArgs(),
                "IDemo.VoidNoArgs(); Expected #0, Actual #1.");
        }

        [Test]
        public void CanUseAnonymousDelegatesToCallVoidMethods_WithStringArg()
        {
            DoNotExpect.Call(delegate { demo.VoidStringArg("Ayende"); });
            mocks.ReplayAll();
            Assert.Throws<ExpectationViolationException> (
                () => demo.VoidStringArg ("Ayende"),
                "IDemo.VoidStringArg(\"Ayende\"); Expected #0, Actual #1.");
        }

        [Test]
        public void CanUseAnonymousDelegatesToCallVoidMethods_WithoutAnonymousDelegate()
        {
            DoNotExpect.Call(demo.VoidNoArgs);
            mocks.ReplayAll();
            Assert.Throws<ExpectationViolationException> (
                () => demo.VoidNoArgs(),
                "IDemo.VoidNoArgs(); Expected #0, Actual #1.");
        }

        [Test]
        public void CanUseAnonymousDelegatesToCallStringMethods_WithoutAnonymousDelegate()
        {
            DoNotExpect.Call(demo.StringArgString("Ayende"));
            mocks.ReplayAll();
            Assert.Throws<ExpectationViolationException> (
                () => demo.StringArgString ("Ayende"),
                "IDemo.StringArgString(\"Ayende\"); Expected #0, Actual #1.");
        }

        [Test]
        public void DoNotExpectCallRespectsArguments()
        {
            DoNotExpect.Call(demo.StringArgString("Ayende"));
            mocks.ReplayAll();
            demo.StringArgString("Sneal");
            Assert.Throws<ExpectationViolationException> (
                () => demo.StringArgString ("Ayende"),
                "IDemo.StringArgString(\"Ayende\"); Expected #0, Actual #1.");
        }

        [Test]
        public void ThrowWhenCallIsNull()
        {
        	Assert.Throws<ArgumentNullException>(() => DoNotExpect.Call(null));
        }

        [Test]
        public void CanUseDoNotCallOnPropertySet()
        {
            DoNotExpect.Call(delegate { demo.Prop = "Ayende"; });
            mocks.ReplayAll();

            Assert.Throws<ExpectationViolationException> (
                () => demo.Prop = "Ayende",
                "IDemo.set_Prop(\"Ayende\"); Expected #0, Actual #1.");
        }

        [Test]
        public void CanUseDoNotCallOnPropertyGet()
        {
            DoNotExpect.Call(demo.Prop);
            mocks.ReplayAll();
            Assert.Throws<ExpectationViolationException> (
                () =>
                {
                    string soItCompiles = demo.Prop;
                },
                "IDemo.get_Prop(); Expected #0, Actual #1.");
        }
    }
}
