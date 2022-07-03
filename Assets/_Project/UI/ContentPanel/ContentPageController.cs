using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UISystem
{
    public class ContentPageController : MonoBehaviour
    {
        private CanvasFader currentPage;
        public void SetContentPage(CanvasFader newPage)
        {
            if (currentPage != null) currentPage.SetInactive();
            currentPage = newPage;
            currentPage.SetActive();
        }
    }
}
