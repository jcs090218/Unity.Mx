#if UNITY_EDITOR
using System;

namespace Mx
{
    [Serializable]
    public class MxData
    {
        /* Variables */

        /* Setter & Getter */

        /* Functions */

        public string FormKey(string name) 
        {
            return MxUtil.FormKey("Data.") + name; 
        }

        public void Init()
        {

        }

        public void Draw()
        {

        }

        public void SavePref()
        {

        }
    }
}
#endif
