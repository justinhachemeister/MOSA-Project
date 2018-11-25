// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

using Mosa.Compiler.Framework;

namespace Mosa.Platform.x86.Instructions
{
	/// <summary>
	/// JmpFar
	/// </summary>
	/// <seealso cref="Mosa.Platform.x86.X86Instruction" />
	public sealed class JmpFar : X86Instruction
	{
		public override int ID { get { return 217; } }

		internal JmpFar()
			: base(0, 0)
		{
		}

		public override bool HasUnspecifiedSideEffect { get { return true; } }

		public override void Emit(InstructionNode node, BaseCodeEmitter emitter)
		{
			System.Diagnostics.Debug.Assert(node.ResultCount == 0);
			System.Diagnostics.Debug.Assert(node.OperandCount == 0);

			emitter.OpcodeEncoder.AppendByte(0xEA);
			emitter.OpcodeEncoder.EmitForward32(6);
			emitter.OpcodeEncoder.AppendByte(0x08);
			emitter.OpcodeEncoder.AppendByte(0x00);
		}
	}
}
