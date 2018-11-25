// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

using Mosa.Compiler.Framework;

namespace Mosa.Platform.x86.Instructions
{
	/// <summary>
	/// BranchGreaterThan
	/// </summary>
	/// <seealso cref="Mosa.Platform.x86.X86Instruction" />
	public sealed class BranchGreaterThan : X86Instruction
	{
		public override int ID { get { return 324; } }

		internal BranchGreaterThan()
			: base(0, 0)
		{
		}

		public override string AlternativeName { get { return "JG"; } }

		public override FlowControl FlowControl { get { return FlowControl.ConditionalBranch; } }

		public override bool IsZeroFlagUsed { get { return true; } }

		public override bool IsSignFlagUsed { get { return true; } }

		public override BaseInstruction GetOpposite()
		{
			return X86.BranchLessOrEqual;
		}

		public override void Emit(InstructionNode node, BaseCodeEmitter emitter)
		{
			System.Diagnostics.Debug.Assert(node.ResultCount == 0);
			System.Diagnostics.Debug.Assert(node.OperandCount == 0);

			emitter.OpcodeEncoder.AppendByte(0x0F);
			emitter.OpcodeEncoder.AppendByte(0x8F);
			emitter.OpcodeEncoder.EmitRelative32(node.BranchTargets[0].Label);
		}
	}
}
