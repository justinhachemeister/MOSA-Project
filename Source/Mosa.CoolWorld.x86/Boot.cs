﻿// Copyright (c) MOSA Project. Licensed under the New BSD License.

using Mosa.AppSystem;
using Mosa.DeviceDriver.ScanCodeMap;
using Mosa.DeviceSystem;
using Mosa.FileSystem.FAT;
using Mosa.Kernel.x86;
using Mosa.Runtime.Plug;
using Mosa.Runtime.x86;

namespace Mosa.CoolWorld.x86
{
	/// <summary>
	/// Boot
	/// </summary>
	public static class Boot
	{
		public static ConsoleSession Console;

		public static ConsoleSession Debug;

		[Plug("Mosa.Runtime.StartUp::SetInitialMemory")]
		public static void SetInitialMemory()
		{
			KernelMemory.SetInitialMemory(Address.GCInitialMemory, 0x01000000);
		}

		/// <summary>
		/// Main
		/// </summary>
		public static void Main()
		{
			Kernel.x86.Kernel.Setup();

			Console = ConsoleManager.Controller.Boot;

			Console.Clear();
			Console.Goto(0, 0);

			//IDT.SetInterruptHandler(ProcessInterrupt);

			Console.ScrollRow = 23;
			Console.Color = ScreenColor.White;
			Console.BackgroundColor = ScreenColor.Green;

			Debug = ConsoleManager.Controller.Boot;

			Console.Write("                   MOSA OS Version 1.5 - Compiler Version 1.5");
			FillLine();
			Console.Color = ScreenColor.White;
			Console.BackgroundColor = ScreenColor.Black;

			Console.WriteLine("> Initializing hardware abstraction layer...");
			var hardware = new HAL.Hardware();

			var DeviceManager = Setup.Initialize(PlatformArchitecture.X86, hardware);

			Console.WriteLine("> Registering device drivers...");
			DeviceDriver.Setup.Register(DeviceManager);

			Console.WriteLine("> Starting devices...");
			DeviceDriver.Setup.Start(DeviceManager);

			Console.Write("> Probing for ISA devices...");
			var isaDevices = DeviceManager.GetAllDevices();
			Console.WriteLine("[Completed: " + isaDevices.Count.ToString() + " found]");

			foreach (var device in isaDevices)
			{
				Console.Write("  ");
				Bullet(ScreenColor.Yellow);
				Console.Write(" ");
				InBrackets(device.Name, ScreenColor.White, ScreenColor.Green);
				Console.WriteLine();
			}

			Console.Write("> Probing for PCI devices...");

			//Setup.StartPCIDevices();
			var pciDevices = DeviceManager.GetDevices<DeviceSystem.PCI.IPCIDevice>(DeviceStatus.Available);
			Console.WriteLine("[Completed: " + pciDevices.Count.ToString() + " found]");

			foreach (var device in pciDevices)
			{
				var pciDevice = device.DeviceDriver as DeviceSystem.PCI.IPCIDevice;

				Console.Write("  ");
				Bullet(ScreenColor.Yellow);
				Console.Write(" ");
				InBrackets(device.Name + ": " + pciDevice.VendorID.ToString("x") + ":" + pciDevice.DeviceID.ToString("x") + " " + pciDevice.SubSystemID.ToString("x") + ":" + pciDevice.SubVendorID.ToString("x") + " (" + pciDevice.Function.ToString("x") + ":" + pciDevice.ClassCode.ToString("x") + ":" + pciDevice.SubClassCode.ToString("x") + ":" + pciDevice.ProgIF.ToString("x") + ":" + pciDevice.RevisionID.ToString("x") + ")", ScreenColor.White, ScreenColor.Green);
				Console.WriteLine();
			}

			Console.Write("> Probing for disk controllers...");
			var diskcontrollers = DeviceManager.GetDevices<IDiskControllerDevice>();
			Console.WriteLine("[Completed: " + diskcontrollers.Count.ToString() + " found]");

			foreach (var device in diskcontrollers)
			{
				Console.Write("  ");
				Bullet(ScreenColor.Yellow);
				Console.Write(" ");
				InBrackets(device.Name, ScreenColor.White, ScreenColor.Green);
				Console.WriteLine();
			}

			Console.Write("> Probing for disks...");
			var disks = DeviceManager.GetDevices<IDiskDevice>();
			Console.WriteLine("[Completed: " + disks.Count.ToString() + " found]");

			foreach (var disk in disks)
			{
				Console.Write("  ");
				Bullet(ScreenColor.Yellow);
				Console.Write(" ");
				InBrackets(disk.Name, ScreenColor.White, ScreenColor.Green);
				Console.Write(" " + (disk.DeviceDriver as IDiskDevice).TotalBlocks.ToString() + " blocks");
				Console.WriteLine();
			}

			var partitionManager = new PartitionManager(DeviceManager);
			partitionManager.CreatePartitionDevices();

			Console.Write("> Finding partitions...");
			var partitions = DeviceManager.GetDevices<IPartitionDevice>();
			Console.WriteLine("[Completed: " + partitions.Count.ToString() + " found]");

			foreach (var partition in partitions)
			{
				Console.Write("  ");
				Bullet(ScreenColor.Yellow);
				Console.Write(" ");
				InBrackets(partition.Name, ScreenColor.White, ScreenColor.Green);
				Console.Write(" " + (partition.DeviceDriver as IPartitionDevice).BlockCount.ToString() + " blocks");
				Console.WriteLine();
			}

			Console.Write("> Finding file systems...");

			foreach (var partition in partitions)
			{
				var fat = new FatFileSystem(partition.DeviceDriver as IPartitionDevice);

				if (fat.IsValid)
				{
					Console.WriteLine("Found a FAT file system!");

					const string filename = "TEST.TXT";

					var location = fat.FindEntry(filename);

					if (location.IsValid)
					{
						Console.Write("Found: " + filename);

						var fatFileStream = new FatFileStream(fat, location);

						uint len = (uint)fatFileStream.Length;

						Console.WriteLine(" - Length: " + len.ToString());

						Console.Write("Reading File: ");

						for (; ; )
						{
							int i = fatFileStream.ReadByte();

							if (i < 0)
								break;

							Console.Write((char)i);
						}

						Console.WriteLine();
					}
				}
			}

			// Get StandardKeyboard
			var standardKeyboards = DeviceManager.GetDevices("StandardKeyboard");

			if (standardKeyboards.Count == 0)
			{
				Console.WriteLine("No Keyboard!");
				ForeverLoop();
			}

			var standardKeyboard = standardKeyboards[0].DeviceDriver as IKeyboardDevice;

			Debug = ConsoleManager.Controller.Debug;

			// setup keymap
			var keymap = new US();

			// setup keyboard (state machine)
			var keyboard = new DeviceSystem.Keyboard(standardKeyboard, keymap);

			// setup app manager
			var manager = new AppManager(Console, keyboard);

			IDT.SetInterruptHandler(manager.ProcessInterrupt);

			Logger.Log("<SELFTEST:PASSED>");

			manager.Start();
		}

		public static void ForeverLoop()
		{
			while (true)
			{
				Native.Hlt();
			}
		}

		public static void FillLine()
		{
			for (uint c = 80 - Console.Column; c != 0; c--)
			{
				Console.Write(' ');
			}
		}

		public static void InBrackets(string message, byte outerColor, byte innerColor)
		{
			var restore = Console.Color;
			Console.Color = outerColor;
			Console.Write("[");
			Console.Color = innerColor;
			Console.Write(message);
			Console.Color = outerColor;
			Console.Write("]");
			Console.Color = restore;
		}

		public static void Bullet(byte color)
		{
			var restore = Console.Color;
			Console.Color = color;
			Console.Write("*");
			Console.Color = restore;
		}

		private static uint counter = 0;

		public static void ProcessInterrupt(uint interrupt, uint errorCode)
		{
			counter++;

			uint c = Console.Column;
			uint r = Console.Row;
			var col = Console.Color;
			var back = Console.BackgroundColor;

			Console.Column = 31;
			Console.Row = 0;
			Console.Color = ScreenColor.Cyan;
			Console.BackgroundColor = ScreenColor.Black;

			Console.Write(counter, 10, 7);
			Console.Write(':');
			Console.Write(interrupt, 16, 2);
			Console.Write(':');
			Console.Write(errorCode, 16, 2);

			Console.Column = c;
			Console.Row = r;
			Console.Color = col;
			Console.BackgroundColor = back;
		}

		//public static void Mandelbrot()
		//{
		//	double xmin = -2.1;
		//	double ymin = -1.3;
		//	double xmax = 1;
		//	double ymax = 1.3;

		//	int Width = 200;
		//	int Height = 200;

		//	double x, y, x1, y1, xx;

		//	int looper, s, z = 0;
		//	double intigralX, intigralY = 0.0;

		//	intigralX = (xmax - xmin) / Width; // Make it fill the whole window
		//	intigralY = (ymax - ymin) / Height;
		//	x = xmin;

		//	for (s = 1; s < Width; s++)
		//	{
		//		y = ymin;
		//		for (z = 1; z < Height; z++)
		//		{
		//			x1 = 0;
		//			y1 = 0;
		//			looper = 0;
		//			while (looper < 100 && Math.Sqrt((x1 * x1) + (y1 * y1)) < 2)
		//			{
		//				looper++;
		//				xx = (x1 * x1) - (y1 * y1) + x;
		//				y1 = 2 * x1 * y1 + y;
		//				x1 = xx;
		//			}

		//			// Get the percent of where the looper stopped
		//			double perc = looper / (100.0);
		//			// Get that part of a 255 scale
		//			int val = ((int)(perc * 255));
		//			// Use that number to set the color

		//			//map[s, z]= value;

		//			y += intigralY;
		//		}
		//		x += intigralX;
		//	}
		//}
	}
}
