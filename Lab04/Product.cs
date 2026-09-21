using System;

public class Product : IEntity
{
    public string MaSP { get; set; }
    public string TenSP { get; set; }

    private double price;
    public double Price
    {
        get { return price; }
        set
        {
            if (value < 0)
                throw new ArgumentException("Don gia khong duoc am!");
            price = value;
        }
    }

    private int quantity;
    public int Quantity
    {
        get { return quantity; }
        set
        {
            if (value < 0)
                throw new ArgumentException("So luong khong duoc am!");
            quantity = value;
        }
    }

    public string Id
    {
        get { return MaSP; }
    }

    public Product(string maSP, string tenSP, double price, int quantity)
    {
        if (string.IsNullOrWhiteSpace(maSP))
            throw new ArgumentException("Ma san pham khong duoc rong!");

        if (string.IsNullOrWhiteSpace(tenSP))
            throw new ArgumentException("Ten san pham khong duoc rong!");

        MaSP = maSP;
        TenSP = tenSP;
        Price = price;
        Quantity = quantity;
    }

    public override string ToString()
    {
        return "Ma SP: " + MaSP +
               " | Ten: " + TenSP +
               " | Don gia: " + Price.ToString("N0") +
               " | So luong: " + Quantity;
    }
}
