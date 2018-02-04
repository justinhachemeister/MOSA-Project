﻿// Copyright (c) MOSA Project. Licensed under the New BSD License.

// This code was generated by an automated template.

using System.Collections.Generic;

namespace Mosa.Platform.x86
{
	/// <summary>
	/// X86 Instruction Map
	/// </summary>
	public static class X86InstructionMap
	{
		public static readonly Dictionary<string, X86Instruction> Map = new Dictionary<string, X86Instruction>() {
			{ "Adc32", X86.Adc32 },
			{ "AdcConst32", X86.AdcConst32 },
			{ "Add32", X86.Add32 },
			{ "AddConst32", X86.AddConst32 },
			{ "Addsd", X86.Addsd },
			{ "Addss", X86.Addss },
			{ "And32", X86.And32 },
			{ "AndConst32", X86.AndConst32 },
			{ "Break", X86.Break },
			{ "Btr32", X86.Btr32 },
			{ "BtrConst32", X86.BtrConst32 },
			{ "Bts32", X86.Bts32 },
			{ "BtsConst32", X86.BtsConst32 },
			{ "Call", X86.Call },
			{ "Cdq", X86.Cdq },
			{ "Cli", X86.Cli },
			{ "Cmp32", X86.Cmp32 },
			{ "CmpConst32", X86.CmpConst32 },
			{ "CmpXchgLoad32", X86.CmpXchgLoad32 },
			{ "Comisd", X86.Comisd },
			{ "Comiss", X86.Comiss },
			{ "CpuId", X86.CpuId },
			{ "Cvtsd2ss", X86.Cvtsd2ss },
			{ "Cvtsi2sd", X86.Cvtsi2sd },
			{ "Cvtsi2ss", X86.Cvtsi2ss },
			{ "Cvtss2sd", X86.Cvtss2sd },
			{ "Cvttsd2si", X86.Cvttsd2si },
			{ "Cvttss2si", X86.Cvttss2si },
			{ "Dec32", X86.Dec32 },
			{ "Div32", X86.Div32 },
			{ "Divsd", X86.Divsd },
			{ "Divss", X86.Divss },
			{ "FarJmp", X86.FarJmp },
			{ "Hlt", X86.Hlt },
			{ "IDiv32", X86.IDiv32 },
			{ "IMul", X86.IMul },
			{ "In8", X86.In8 },
			{ "In16", X86.In16 },
			{ "In32", X86.In32 },
			{ "InConst8", X86.InConst8 },
			{ "InConst16", X86.InConst16 },
			{ "InConst32", X86.InConst32 },
			{ "Inc32", X86.Inc32 },
			{ "Int", X86.Int },
			{ "Invlpg", X86.Invlpg },
			{ "IRetd", X86.IRetd },
			{ "Jmp", X86.Jmp },
			{ "Lea", X86.Lea },
			{ "Leave", X86.Leave },
			{ "Lgdt", X86.Lgdt },
			{ "Lidt", X86.Lidt },
			{ "Lock", X86.Lock },
			{ "Mov", X86.Mov },
			{ "Movaps", X86.Movaps },
			{ "MovapsLoad", X86.MovapsLoad },
			{ "MovCRLoad", X86.MovCRLoad },
			{ "MovCRStore", X86.MovCRStore },
			{ "Movd", X86.Movd },
			{ "MovLoad", X86.MovLoad },
			{ "Movsd", X86.Movsd },
			{ "MovsdLoad", X86.MovsdLoad },
			{ "MovsdStore", X86.MovsdStore },
			{ "Movss", X86.Movss },
			{ "MovssLoad", X86.MovssLoad },
			{ "MovssStore", X86.MovssStore },
			{ "MovStore", X86.MovStore },
			{ "Movsx8To32", X86.Movsx8To32 },
			{ "Movsx16To32", X86.Movsx16To32 },
			{ "MovsxLoad8", X86.MovsxLoad8 },
			{ "MovsxLoad16", X86.MovsxLoad16 },
			{ "Movups", X86.Movups },
			{ "MovupsLoad", X86.MovupsLoad },
			{ "MovupsStore", X86.MovupsStore },
			{ "Movzx8To32", X86.Movzx8To32 },
			{ "Movzx16To32", X86.Movzx16To32 },
			{ "MovzxLoad8", X86.MovzxLoad8 },
			{ "MovzxLoad16", X86.MovzxLoad16 },
			{ "Mul32", X86.Mul32 },
			{ "Mulsd", X86.Mulsd },
			{ "Mulss", X86.Mulss },
			{ "Neg", X86.Neg },
			{ "Nop", X86.Nop },
			{ "Not32", X86.Not32 },
			{ "Or32", X86.Or32 },
			{ "OrConst32", X86.OrConst32 },
			{ "Out8", X86.Out8 },
			{ "Out16", X86.Out16 },
			{ "Out32", X86.Out32 },
			{ "OutConst8", X86.OutConst8 },
			{ "OutConst16", X86.OutConst16 },
			{ "OutConst32", X86.OutConst32 },
			{ "Pause", X86.Pause },
			{ "Pextrd", X86.Pextrd },
			{ "Pop32", X86.Pop32 },
			{ "Popad", X86.Popad },
			{ "Push32", X86.Push32 },
			{ "PushConst32", X86.PushConst32 },
			{ "Pushad", X86.Pushad },
			{ "PXor", X86.PXor },
			{ "Rcr32", X86.Rcr32 },
			{ "RcrConst32", X86.RcrConst32 },
			{ "RcrConstOne32", X86.RcrConstOne32 },
			{ "Rep", X86.Rep },
			{ "Ret", X86.Ret },
			{ "Roundsd", X86.Roundsd },
			{ "Roundss", X86.Roundss },
			{ "Sar32", X86.Sar32 },
			{ "SarConst32", X86.SarConst32 },
			{ "SarConstOne32", X86.SarConstOne32 },
			{ "Sbb32", X86.Sbb32 },
			{ "SbbConst32", X86.SbbConst32 },
			{ "Shl32", X86.Shl32 },
			{ "ShlConst32", X86.ShlConst32 },
			{ "ShlConstOne32", X86.ShlConstOne32 },
			{ "Shld32", X86.Shld32 },
			{ "ShldConst32", X86.ShldConst32 },
			{ "Shr32", X86.Shr32 },
			{ "ShrConst32", X86.ShrConst32 },
			{ "ShrConstOne32", X86.ShrConstOne32 },
			{ "Shrd32", X86.Shrd32 },
			{ "ShrdConst32", X86.ShrdConst32 },
			{ "Sti", X86.Sti },
			{ "Stos", X86.Stos },
			{ "Sub32", X86.Sub32 },
			{ "SubConst32", X86.SubConst32 },
			{ "Subsd", X86.Subsd },
			{ "Subss", X86.Subss },
			{ "Test32", X86.Test32 },
			{ "TestConst32", X86.TestConst32 },
			{ "Ucomisd", X86.Ucomisd },
			{ "Ucomiss", X86.Ucomiss },
			{ "Xchg32", X86.Xchg32 },
			{ "Xor32", X86.Xor32 },
			{ "XorConst32", X86.XorConst32 },
			{ "BranchOverflow", X86.BranchOverflow },
			{ "BranchNoOverflow", X86.BranchNoOverflow },
			{ "BranchCarry", X86.BranchCarry },
			{ "BranchUnsignedLessThan", X86.BranchUnsignedLessThan },
			{ "BranchUnsignedGreaterOrEqual", X86.BranchUnsignedGreaterOrEqual },
			{ "BranchNoCarry", X86.BranchNoCarry },
			{ "BranchEqual", X86.BranchEqual },
			{ "BranchZero", X86.BranchZero },
			{ "BranchNotEqual", X86.BranchNotEqual },
			{ "BranchNotZero", X86.BranchNotZero },
			{ "BranchUnsignedLessOrEqual", X86.BranchUnsignedLessOrEqual },
			{ "BranchUnsignedGreaterThan", X86.BranchUnsignedGreaterThan },
			{ "BranchSigned", X86.BranchSigned },
			{ "BranchNotSigned", X86.BranchNotSigned },
			{ "BranchParity", X86.BranchParity },
			{ "BranchNoParity", X86.BranchNoParity },
			{ "BranchLessThan", X86.BranchLessThan },
			{ "BranchGreaterOrEqual", X86.BranchGreaterOrEqual },
			{ "BranchLessOrEqual", X86.BranchLessOrEqual },
			{ "BranchGreaterThan", X86.BranchGreaterThan },
			{ "SetOverflow", X86.SetOverflow },
			{ "SetNoOverflow", X86.SetNoOverflow },
			{ "SetCarry", X86.SetCarry },
			{ "SetUnsignedLessThan", X86.SetUnsignedLessThan },
			{ "SetUnsignedGreaterOrEqual", X86.SetUnsignedGreaterOrEqual },
			{ "SetNoCarry", X86.SetNoCarry },
			{ "SetEqual", X86.SetEqual },
			{ "SetZero", X86.SetZero },
			{ "SetNotEqual", X86.SetNotEqual },
			{ "SetNotZero", X86.SetNotZero },
			{ "SetUnsignedLessOrEqual", X86.SetUnsignedLessOrEqual },
			{ "SetUnsignedGreaterThan", X86.SetUnsignedGreaterThan },
			{ "SetSigned", X86.SetSigned },
			{ "SetNotSigned", X86.SetNotSigned },
			{ "SetParity", X86.SetParity },
			{ "SetNoParity", X86.SetNoParity },
			{ "SetLessThan", X86.SetLessThan },
			{ "SetGreaterOrEqual", X86.SetGreaterOrEqual },
			{ "SetLessOrEqual", X86.SetLessOrEqual },
			{ "SetGreaterThan", X86.SetGreaterThan },
			{ "CmovOverflow32", X86.CmovOverflow32 },
			{ "CmovNoOverflow32", X86.CmovNoOverflow32 },
			{ "CmovCarry32", X86.CmovCarry32 },
			{ "CmovUnsignedLessThan32", X86.CmovUnsignedLessThan32 },
			{ "CmovUnsignedGreaterOrEqual32", X86.CmovUnsignedGreaterOrEqual32 },
			{ "CmovNoCarry32", X86.CmovNoCarry32 },
			{ "CmovEqual32", X86.CmovEqual32 },
			{ "CmovZero32", X86.CmovZero32 },
			{ "CmovNotEqual32", X86.CmovNotEqual32 },
			{ "CmovNotZero32", X86.CmovNotZero32 },
			{ "CmovUnsignedLessOrEqual32", X86.CmovUnsignedLessOrEqual32 },
			{ "CmovUnsignedGreaterThan32", X86.CmovUnsignedGreaterThan32 },
			{ "CmovSigned32", X86.CmovSigned32 },
			{ "CmovNotSigned32", X86.CmovNotSigned32 },
			{ "CmovParity32", X86.CmovParity32 },
			{ "CmovNoParity32", X86.CmovNoParity32 },
			{ "CmovLessThan32", X86.CmovLessThan32 },
			{ "CmovGreaterOrEqual32", X86.CmovGreaterOrEqual32 },
			{ "CmovLessOrEqual32", X86.CmovLessOrEqual32 },
			{ "CmovGreaterThan32", X86.CmovGreaterThan32 },
		};
	}
}
