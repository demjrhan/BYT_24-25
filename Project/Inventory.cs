namespace Project;

public class Inventory
{
    public int TotalBooksQuantity { get; set; }
    public int TotalAccessoriesQuantity { get; set; }
    public bool LowStock { get; set; }
    public string SupplierInfo { get; set; }

    public Inventory(int totalBooksQuantity, int totalAccessoriesQuantity, bool lowStock, string supplierInfo)
    {
        TotalBooksQuantity = totalBooksQuantity;
        TotalAccessoriesQuantity = totalAccessoriesQuantity;
        LowStock = lowStock;
        SupplierInfo = supplierInfo;
    }
}
