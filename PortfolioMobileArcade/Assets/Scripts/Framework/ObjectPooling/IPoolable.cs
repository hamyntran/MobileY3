using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable<T>
{
   T Pull();

   void Push(T t);
}
