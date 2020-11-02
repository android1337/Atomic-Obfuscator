using System;
using System.Collections.Generic;
using System.Linq;

namespace AtomicProtector.Atomic.cflow
{
	// Token: 0x0200001E RID: 30
	public class Blocks
	{
		// Token: 0x060000C4 RID: 196 RVA: 0x0000A444 File Offset: 0x00008644
		public Block getBlock(int id)
		{
			return this.blocks.Single((Block block) => block.ID == id);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000A47C File Offset: 0x0000867C
		public void Scramble(out Blocks incGroups)
		{
			Blocks blocks = new Blocks();
			foreach (Block item in this.blocks)
			{
				blocks.blocks.Insert(this.generator.Next(1, blocks.blocks.Count), item);
			}
			incGroups = blocks;
		}

		// Token: 0x0400006B RID: 107
		public List<Block> blocks = new List<Block>();

		// Token: 0x0400006C RID: 108
		private Random generator = new Random();
	}
}
