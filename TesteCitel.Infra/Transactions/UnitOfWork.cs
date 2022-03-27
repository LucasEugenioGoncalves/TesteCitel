namespace TesteCitel.Infra.Transactions
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly bd_citelContext _context;

        public UnitOfWork(bd_citelContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
