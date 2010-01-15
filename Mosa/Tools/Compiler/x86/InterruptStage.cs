﻿/*
 * (c) 2008 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Phil Garcia (tgiphil) <phil@thinkedge.com>
 */

using System;
using System.Collections.Generic;

using Mosa.Runtime.CompilerFramework;
using Mosa.Runtime.Vm;
using Mosa.Platforms.x86;
using Mosa.Runtime.Metadata;
using Mosa.Runtime.Metadata.Signatures;
using Mosa.Runtime.CompilerFramework.Operands;
using Mosa.Tools.Compiler.LinkTimeCodeGeneration;
using Mosa.Runtime.Linker;

using IR = Mosa.Runtime.CompilerFramework.IR;
using CPUx86 = Mosa.Platforms.x86.CPUx86;

namespace Mosa.Tools.Compiler.x86
{

	/// <summary>
	/// 
	/// </summary>
	public sealed class InterruptStage : IAssemblyCompilerStage, IPipelineStage
	{
		#region Data Members

		#endregion // Data Members

		#region IPipelineStage

		/// <summary>
		/// Retrieves the name of the compilation stage.
		/// </summary>
		/// <value>The name of the compilation stage.</value>
		string IPipelineStage.Name { get { return @"Interrupt Stage"; } }

		#endregion // IPipelineStage Members

		#region Methods

		#endregion // Methods

		#region IAssemblyCompilerStage Members

		/// <summary>
		/// Performs stage specific processing on the compiler context.
		/// </summary>
		/// <param name="compiler">The compiler context to perform processing in.</param>
		public void Run(AssemblyCompiler compiler)
		{
			CreateISRMethods(compiler);
			//CreateIVTMethod(compiler);
		}

		#endregion // IAssemblyCompilerStage Members

		#region Internal

		/// <summary>
		/// Creates the ISR methods.
		/// </summary>
		/// <param name="compiler">The compiler.</param>
		private void CreateISRMethods(AssemblyCompiler compiler)
		{
			// Create Interrupt Service Routines (ISR)
			RuntimeMethod InterruptMethod = null; // TODO: Look up method, if not found, don't bother with this stage

			SigType I1 = new SigType(CilElementType.I1);

			for (int i = 0; i <= 256; i++) {
				InstructionSet set = new InstructionSet(100);
				Context ctx = new Context(set, -1);

				ctx.AppendInstruction(CPUx86.Instruction.CliInstruction);
				if ((i != 8) && (i < 10 || i > 14)) // For IRQ 8, 10, 11, 12, 13, 14 the cpu automatically pushed the error code
					ctx.AppendInstruction(CPUx86.Instruction.PushInstruction, new ConstantOperand(I1, 0x0));
				ctx.AppendInstruction(CPUx86.Instruction.PushadInstruction);
				ctx.AppendInstruction(IR.Instruction.CallInstruction, InterruptMethod);
				ctx.Operand1 = new ConstantOperand(I1, i);
				// ctx.Operand2 = ; // TODO: Operand2 should be the error code, which was pushed on the stack earlier
				ctx.AppendInstruction(CPUx86.Instruction.PopInstruction);
				ctx.AppendInstruction(CPUx86.Instruction.PopadInstruction);
				ctx.AppendInstruction(CPUx86.Instruction.PopInstruction);
				ctx.AppendInstruction(CPUx86.Instruction.StiInstruction);
				ctx.AppendInstruction(CPUx86.Instruction.IRetdInstruction);

				CompilerGeneratedMethod method = LinkTimeCodeGenerator.Compile(compiler, @"InterruptISR" + i.ToString() + ">", set);
			}
		}

		/// <summary>
		/// Creates the IVT method.
		/// </summary>
		/// <param name="compiler">The compiler.</param>
		private void CreateIVTMethod(AssemblyCompiler compiler)
		{
			InstructionSet set = new InstructionSet(4048);
			Context ctx = new Context(set, -1);

			ctx.AppendInstruction(IR.Instruction.PrologueInstruction);
			ctx.Other = 0; // stacksize

			SigType I1 = new SigType(CilElementType.I1);

			// Create the IVT Table
			SigType I4 = new SigType(CilElementType.I4);
			RegisterOperand ecx = new RegisterOperand(I4, GeneralPurposeRegister.ECX);
			RegisterOperand eax = new RegisterOperand(I4, GeneralPurposeRegister.EAX);
			RegisterOperand ebx = new RegisterOperand(I4, GeneralPurposeRegister.EBX);

			//LabelOperand label = new LabelOperand(I4, "");

			ctx.AppendInstruction(CPUx86.Instruction.MovInstruction, ecx, new ConstantOperand(I4, 0x201000));
			for (int i = 0; i <= 256; i++) {
				// REMARKS:
				// Since the position of the ISRs has not been determined yet, the Operand for the MOV instruction is also indeterminate.
				// This will need to be patched during linking. So we'll leave the operand null and set the Other value to the target label.
				ctx.AppendInstruction(CPUx86.Instruction.MovInstruction, eax);
				//ctx.Other = isrs[i].Label;
				ctx.AppendInstruction(CPUx86.Instruction.MovInstruction, new MemoryOperand(I4, ecx.Register, new IntPtr(0x4)), ebx);
			}

			ctx.SetInstruction(IR.Instruction.EpilogueInstruction);
			ctx.Other = 0;

			CompilerGeneratedMethod method = LinkTimeCodeGenerator.Compile(compiler, @"InterruptInit", set);
		}

		#endregion Internal
	}
}