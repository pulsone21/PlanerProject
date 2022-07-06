using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UISystem
{
    public interface IExpandable
    {
        void Expand(bool force = false);
        void Shrink(bool force = false);
    }
}
