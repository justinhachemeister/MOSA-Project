﻿/*
 * (c) 2008 MOSA - The Managed Operating System Alliance
 *
 * Licensed under the terms of the New BSD License.
 *
 * Authors:
 *  Phil Garcia (tgiphil) <phil@thinkedge.com>
 */

namespace Mosa.DeviceDrivers
{
    /// <summary>
    /// 
    /// </summary>
	public class MemoryRegion : IMemoryRegion
	{
        /// <summary>
        /// 
        /// </summary>
		protected uint baseAddress;
        /// <summary>
        /// 
        /// </summary>
		protected uint size;

        /// <summary>
        /// Gets the base address.
        /// </summary>
        /// <value>The base address.</value>
		public uint BaseAddress { get { return baseAddress; } }
        /// <summary>
        /// Gets the size.
        /// </summary>
        /// <value>The size.</value>
		public uint Size { get { return size; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryRegion"/> class.
        /// </summary>
        /// <param name="baseAddress">The base address.</param>
        /// <param name="size">The size.</param>
		public MemoryRegion(uint baseAddress, uint size)
		{
			this.baseAddress = baseAddress;
			this.size = size;
		}

	}

}
