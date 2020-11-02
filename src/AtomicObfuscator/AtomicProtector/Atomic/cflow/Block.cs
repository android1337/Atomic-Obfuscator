using System;
using System.Collections.Generic;
using dnlib.DotNet.Emit;

namespace AtomicProtector.Atomic.cflow
{
	// Token: 0x0200001D RID: 29
	public class Block
	{
		// Token: 0x04000068 RID: 104
		public int ID = 0;

		// Token: 0x04000069 RID: 105
		public int nextBlock = 0;

		// Token: 0x0400006A RID: 106
		public List<Instruction> instructions = new List<Instruction>();
	}
}
