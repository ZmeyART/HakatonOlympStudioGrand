using UnityEngine;
using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;


public class BasePool<T> where T : MonoBehaviour
{

    #region FIELDS

    public T pref { get; }

    public bool autoExpand {  get; set; }

    private List<T> pool;


    public BasePool(T pref, Transform container, int count)
    {
        this.pref = pref;
        CreatePool(count);
    }

    #endregion

    #region MAIN_METHODS

    private void CreatePool(int count)
    {
        pool = new List<T>();
        for(int i = 0; i < count; i++)
        {
            CreateEl();
        }
    }

    private T CreateEl(bool isActiveByDefault = false)
    {
        var createdObject = Object.Instantiate(pref);
        createdObject.gameObject.SetActive(isActiveByDefault);
        pool.Add(createdObject);
        return createdObject;
    }

    public T GetFreeEl()
    {
        if(IsHasFreeEl(out var freeEl))
            return freeEl;

        if (autoExpand)
            return this.CreateEl(true);

        throw new Exception("There is no free element in pool");

    }

    public bool IsHasFreeEl(out T element)
    {
        foreach (var el in pool)
        {
            if (!el.gameObject.activeInHierarchy)
            {
                element = el;
                el.gameObject.SetActive(true);
                return true;
            }
        }

        element = null;
        return false;
    }

    #endregion

}
