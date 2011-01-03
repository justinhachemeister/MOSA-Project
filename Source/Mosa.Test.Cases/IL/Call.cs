﻿/*
 * (c) 2011 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Phil Garcia (tgiphil) <phil@thinkedge.com> 
 *
 */
 

using System;
using System.Collections.Generic;
using System.Text;
using MbUnit.Framework;

using Mosa.Test.Runtime.CompilerFramework;
using Mosa.Test.Runtime.CompilerFramework.Numbers;

namespace Mosa.Test.Cases.IL
{
	[TestFixture]
	public class Call : TestCompilerAdapter
	{
		public Call()
		{
			settings.AddReference("Mosa.Test.Collection.dll");
		}
		
		[Test, Factory(typeof(U1), "Samples")]
		public void CallU1(byte a)
		{
			Assert.IsTrue(Run<bool>("Mosa.Test.Collection", "CallTests", "CallU1", a));
		}
		
		[Test, Factory(typeof(U2), "Samples")]
		public void CallU2(ushort a)
		{
			Assert.IsTrue(Run<bool>("Mosa.Test.Collection", "CallTests", "CallU2", a));
		}
		
		[Test, Factory(typeof(U4), "Samples")]
		public void CallU4(uint a)
		{
			Assert.IsTrue(Run<bool>("Mosa.Test.Collection", "CallTests", "CallU4", a));
		}
		
		[Test, Factory(typeof(U8), "Samples")]
		public void CallU8(ulong a)
		{
			Assert.IsTrue(Run<bool>("Mosa.Test.Collection", "CallTests", "CallU8", a));
		}
		
		[Test, Factory(typeof(I1), "Samples")]
		public void CallI1(sbyte a)
		{
			Assert.IsTrue(Run<bool>("Mosa.Test.Collection", "CallTests", "CallI1", a));
		}
		
		[Test, Factory(typeof(I2), "Samples")]
		public void CallI2(short a)
		{
			Assert.IsTrue(Run<bool>("Mosa.Test.Collection", "CallTests", "CallI2", a));
		}
		
		[Test, Factory(typeof(I4), "Samples")]
		public void CallI4(int a)
		{
			Assert.IsTrue(Run<bool>("Mosa.Test.Collection", "CallTests", "CallI4", a));
		}
		
		[Test, Factory(typeof(I8), "Samples")]
		public void CallI8(long a)
		{
			Assert.IsTrue(Run<bool>("Mosa.Test.Collection", "CallTests", "CallI8", a));
		}
		
		[Test, Factory(typeof(C), "Samples")]
		public void CallC(char a)
		{
			Assert.IsTrue(Run<bool>("Mosa.Test.Collection", "CallTests", "CallC", a));
		}
		
	}
}