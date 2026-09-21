using System;
using System.Collections.Generic;
using System.Linq;

public class Repository<T> where T : IEntity
{
    private List<T> danhSach = new List<T>();

    public void Add(T item)
    {
        danhSach.Add(item);
    }

    public void Remove(T item)
    {
        danhSach.Remove(item);
    }

    public T FindById(string id)
    {
        return danhSach.FirstOrDefault(x => x.Id == id);
    }

    public List<T> Find(Func<T, bool> dieuKien)
    {
        return danhSach.Where(dieuKien).ToList();
    }

    public List<T> GetAll()
    {
        return new List<T>(danhSach);
    }
}
