using System;
using System.Collections.Generic;

public class ProductService
{
    private Repository<Product> repository = new Repository<Product>();

    public event Action<Product> ProductAdded;
    public event Action<Product> ProductRemoved;

    public void AddProduct(Product product)
    {
        if (repository.FindById(product.Id) != null)
            throw new DuplicateProductException("Ma san pham " + product.Id + " da ton tai!");

        repository.Add(product);
        ProductAdded?.Invoke(product);
    }

    public void RemoveProduct(string maSP)
    {
        Product product = repository.FindById(maSP);

        if (product == null)
            throw new ProductNotFoundException("Khong tim thay san pham co ma " + maSP + "!");

        repository.Remove(product);
        ProductRemoved?.Invoke(product);
    }

    public Product FindById(string maSP)
    {
        Product product = repository.FindById(maSP);

        if (product == null)
            throw new ProductNotFoundException("Khong tim thay san pham co ma " + maSP + "!");

        return product;
    }

    public List<Product> Search(Func<Product, bool> dieuKien)
    {
        return repository.Find(dieuKien);
    }

    public List<Product> Filter(Func<Product, bool> dieuKien)
    {
        return repository.Find(dieuKien);
    }

    public List<Product> GetAll()
    {
        return repository.GetAll();
    }

    public double TinhTongGiaTriKho()
    {
        double tong = 0;

        foreach (Product product in repository.GetAll())
            tong += product.Price * product.Quantity;

        return tong;
    }
}
