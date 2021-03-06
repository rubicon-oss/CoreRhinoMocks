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

using System;
using System.IO;
using Castle.DynamicProxy;
using Rhino.Mocks.Interfaces;
using Rhino.Mocks.Utilities;

namespace Rhino.Mocks.Impl
{
	/// <summary>
	/// Rudimetry implementation that simply logs methods calls as text.
	/// </summary>
	public class TextWriterExpectationLogger : IExpectationLogger
	{
		private readonly TextWriter writer;

		/// <summary>
		/// Initializes a new instance of the <see cref="TextWriterExpectationLogger"/> class.
		/// </summary>
		/// <param name="writer">The writer.</param>
		public TextWriterExpectationLogger(TextWriter writer)
		{
			this.writer = writer;
		}
		/// <summary>
		/// Logs the expectation as it was recorded
		/// </summary>
		/// <param name="invocation">The invocation.</param>
		/// <param name="expectation">The expectation.</param>
		public void LogRecordedExpectation(IInvocation invocation, IExpectation expectation)
		{
			string methodCall = MethodCallUtil.StringPresentation(invocation, invocation.Method, invocation.Arguments);
			writer.WriteLine("Recorded expectation: {0}", methodCall);
		}

		/// <summary>
		/// Logs the expectation as it was recorded
		/// </summary>
		/// <param name="invocation">The invocation.</param>
		/// <param name="expectation">The expectation.</param>
		public void LogReplayedExpectation(IInvocation invocation, IExpectation expectation)
		{
			string methodCall = MethodCallUtil.StringPresentation(invocation, invocation.Method, invocation.Arguments);
			writer.WriteLine("Replayed expectation: {0}", methodCall);
		}

		/// <summary>
		/// Logs the unexpected method call.
		/// </summary>
		/// <param name="invocation">The invocation.</param>
		/// <param name="message">The message.</param>
		public void LogUnexpectedMethodCall(IInvocation invocation, string message)
		{
			string methodCall = MethodCallUtil.StringPresentation(invocation, invocation.Method, invocation.Arguments);
			writer.WriteLine("{1}: {0}", methodCall, message);
		}
	}
}
