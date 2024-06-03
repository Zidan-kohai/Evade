using System.Collections.Generic;
using GeekplaySchool;
public static class Extension
{
    public static bool HasKey(this List<MyDictionary> collection, int key)
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

    public static MyDictionary GetByKey(this List<MyDictionary> collection, int key)
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