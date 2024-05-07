using System.Collections.Generic;

public static class Extension
{
    public static bool HasKey(this List<MyDictinary> collection, int key)
    {
        for (int i = 0; i < collection.Count; i++)
        {
            if (collection[i].key == key)
            {
                return true;
            }
        }

        return false;
    }

    public static MyDictinary GetByKey(this List<MyDictinary> collection, int key)
    {
        for (int i = 0; i < collection.Count; i++)
        {
            if (collection[i].key == key)
            {
                return collection[i];
            }
        }

        return null;
    }
}