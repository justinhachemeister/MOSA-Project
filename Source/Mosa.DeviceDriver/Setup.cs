﻿// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.DeviceSystem;

namespace Mosa.DeviceDriver
{
	public static class Setup
	{
		public static void Register(DeviceManager deviceManager)
		{
			deviceManager.RegisterDeviceDriver(new ISADeviceDriverRegistryEntry()
			{
				Name = "CMOS",
				Platforms = PlatformArchitecture.X86,
				AutoLoad = true,
				BasePort = 0x0070,
				PortRange = 2,
				Factory = delegate { return new ISA.CMOS(); }
			});

			deviceManager.RegisterDeviceDriver(new ISADeviceDriverRegistryEntry()
			{
				Name = "StandardKeyboard",
				Platforms = PlatformArchitecture.X86AndX64,
				AutoLoad = true,
				BasePort = 0x60,
				PortRange = 1,
				AltBasePort = 0x64,
				AltPortRange = 1,
				IRQ = 1,
				Factory = delegate { return new ISA.StandardKeyboard(); }
			});

			//deviceManager.RegisterDeviceDriver(new ISADeviceDriverRegistryEntry()
			//{
			//	Name = "PCIController",
			//	Platforms = PlatformArchitecture.X86AndX64,
			//	AutoLoad = true,
			//	BasePort = 0x0CF8,
			//	PortRange = 8,
			//	Factory = delegate { return new ISA.PCIController(); }
			//});

			deviceManager.RegisterDeviceDriver(new ISADeviceDriverRegistryEntry()
			{
				Name = "IDEController",
				Platforms = PlatformArchitecture.X86AndX64,
				AutoLoad = true,
				BasePort = 0x1F0,
				PortRange = 8,
				AltBasePort = 0x3F6,
				AltPortRange = 8,
				Factory = delegate { return new ISA.IDEController(); }
			});

			//deviceManager.RegisterDeviceDriver(new ISADeviceDriver()
			//{
			//	Name = "IDEController (Secondary)",
			//	Platforms = PlatformArchitecture.X86AndX64,
			//	AutoLoad = true,
			//	BasePort = 0x170,
			//	PortRange = 8,
			//	AltBasePort = 0x376,
			//	AltPortRange = 8,
			//	ForceOption = "ide2",
			//	Factory = delegate { return new ISA.IDEController(); }
			//});
		}

		public static void Start(DeviceManager deviceManager)
		{
			deviceManager.Initialize(new X86System(), null);
		}
	}
}
