namespace vardibileBlockchain
{
    public class Blockchain
    {
        IList<Transaction> PendingTransactions = new List<Transaction>();
        public IList<Block> Chain { get; set; }
        public int Difficulty { get; set; } = 1;
        public int Reward { get; set; } = 1;

        public Blockchain()
        {
            InitializeChain();
            AddGenesisBlock();
        }

        public void InitializeChain()
        {
            Chain = new List<Block>();
        }
        public Block CreateGenesisBlock()
        {
            Block block = new Block(DateTime.Now, null, PendingTransactions);
            block.Mine(Difficulty);
            PendingTransactions = new List<Transaction>();
            return block;
        }
        public void AddGenesisBlock()
        {
            Chain.Add(CreateGenesisBlock());
        }
        public void AddBlock(Block block)
        {
            block.Index = Chain[Chain.Count - 1].Index + 1;
            block.PreviousHash = Chain[Chain.Count - 1].Hash;
            block.Hash = block.HashCalculate();
            block.Mine(this.Difficulty);
            Chain.Add(block);
        }
        public void CreateTransaction(Transaction transaction)
        {
            PendingTransactions.Add(transaction);
        }
        public void ProcessTransactions(string minerAddress)
        {
            CreateTransaction(new Transaction("Miner", minerAddress, Reward));
            AddBlock(new Block(DateTime.Now, Chain[Chain.Count - 1].Hash, PendingTransactions));
            PendingTransactions = new List<Transaction>();
        }
        public bool IsValid()
        {
            for (int i = 1; i < Chain.Count; i++)
            {
                Block currentBlock = Chain[i];
                Block previousBlock = Chain[i - 1];
                if ((currentBlock.Hash != currentBlock.HashCalculate()) && (currentBlock.PreviousHash != previousBlock.Hash)) return false;
            }
            return true;
        }

        public int GetBalance(string address)
        {
            int balance = 0;
            for (int i = 0; i < Chain.Count; i++)
            {
                for (int j = 0; j < Chain[i].Transactions.Count; j++)
                {
                    var transaction = Chain[i].Transactions[j];
                    if (transaction.From == address)
                    {
                        balance -= transaction.Amount;
                    }
                    if (transaction.To == address)
                    {
                        balance += transaction.Amount;
                    }
                }

            }
            return balance;
        }
    }
}
