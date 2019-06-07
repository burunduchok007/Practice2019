using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKTests
{
    interface ILike
    {
        void GoToURL(PersonPage personPage);
        void EnterPage(PersonPage personPage);
        void Wait();
        bool IsVisible();
        void Like(int likeCount);
        
    }
}
