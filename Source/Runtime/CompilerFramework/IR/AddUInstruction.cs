﻿/*
 * (c) 2008 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Michael Ruck (grover) <sharpos@michaelruck.de>
 */

using System;

namespace Mosa.Runtime.CompilerFramework.IR
{
	/// <summary>
	/// Intermediate representation of the unsigned add operation.
	/// </summary>
	/// <remarks>
	/// The add instruction is a three-address instruction, where the result receives
	/// the value of the first operand (index 0) added with the second operand (index 1).
	/// <para />
	/// Both the first and second operand must be the same integral type. If the second operand
	/// is statically or dynamically equal to or larger than the number of bits in the first
	/// operand, the result is undefined.
	/// </remarks>
	public sealed class AddUInstruction : ThreeOperandInstruction
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AddUInstruction"/>.
		/// </summary>
		public AddUInstruction()
		{
		}

		/// <summary>
		/// Allows visitor based dispatch for this instruction object.
		/// </summary>
		/// <param name="visitor">The visitor object.</param>
		/// <param name="context">The context.</param>
		public override void Visit(IIRVisitor visitor, Context context)
		{
			visitor.AddUInstruction(context);
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents this instance.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <returns>
		/// A <see cref="System.String"/> that represents this instance.
		/// </returns>
		public override string ToString(Context context)
		{
			return String.Format(@"IR add.u{0} {1} = {2} + {3}", context.Result.Precision, context.Result, context.Operand1, context.Operand2);
		}
	}
}
