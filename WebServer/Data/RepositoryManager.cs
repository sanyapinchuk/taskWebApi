using WebServer.Data.Interfaces;

namespace WebServer.Data
{
    public class RepositoryManager: IRepositoryManager
    {
        private DataContext dataContext;

        private FridgeRepository fridgeRepository;
        private FridgeModelRepository fridgeModelRepository;
        private ProductRepository productRepository;
        private FridgeProductRepository fridgeProductRepository;

        public RepositoryManager(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public FridgeRepository Fridge
        {
            get
            {
                if (fridgeRepository == null)
                    fridgeRepository = new FridgeRepository(dataContext);
                return fridgeRepository;
            }
        }

        public FridgeModelRepository FridgeModel
        {
            get
            {
                if (fridgeModelRepository == null)
                    fridgeModelRepository = new FridgeModelRepository(dataContext);
                return fridgeModelRepository;
            }
        }
        public ProductRepository Product
        {
            get
            {
                if (productRepository == null)
                    productRepository = new ProductRepository(dataContext);
                return productRepository;
            }
        }

        public FridgeProductRepository FridgeProduct
        {
            get
            {
                if (fridgeProductRepository == null)
                    fridgeProductRepository = new FridgeProductRepository(dataContext);
                return fridgeProductRepository;
            }
        }

        public async Task<bool> IsValidId(int idFridge, int idProduct)
        {
            var fridge = await Fridge.GetFridgeByIdAsync(idFridge);

            if (fridge == null)
            {
                return false;
            }

            var product = await Product.GetProductByIdAsync(idProduct);
            if (product == null)
            {
                return false;
            }
            return true;
        }

        public void Save()
        {
            dataContext.SaveChanges();
        }
    }
}