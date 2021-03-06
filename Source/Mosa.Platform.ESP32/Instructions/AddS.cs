// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

using Mosa.Compiler.Framework;

namespace Mosa.Platform.ESP32.Instructions
{
	/// <summary>
	/// AddS - Add Single, ADDS computes the IEEE754 single-precision sum of the contents of floating-point registers fs and ft, and writes the result to floating-point register fr
	/// </summary>
	/// <seealso cref="Mosa.Platform.ESP32.ESP32Instruction" />
	public sealed class AddS : ESP32Instruction
	{
		public override int ID { get { return 695; } }

		internal AddS()
			: base(1, 3)
		{
		}

		public override void Emit(InstructionNode node, BaseCodeEmitter emitter)
		{
			System.Diagnostics.Debug.Assert(node.ResultCount == 1);
			System.Diagnostics.Debug.Assert(node.OperandCount == 3);

			emitter.OpcodeEncoder.AppendNibble(0b0000);
			emitter.OpcodeEncoder.AppendNibble(0b1010);
			emitter.OpcodeEncoder.AppendNibble(node.Result.Register.RegisterCode);
			emitter.OpcodeEncoder.AppendNibble(node.Operand1.Register.RegisterCode);
			emitter.OpcodeEncoder.AppendNibble(node.Operand2.Register.RegisterCode);
			emitter.OpcodeEncoder.AppendNibble(0b0000);
		}
	}
}
