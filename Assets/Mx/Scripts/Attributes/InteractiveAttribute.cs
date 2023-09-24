using System;
using UnityEngine;

namespace MetaX
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class InteractiveAttribute : PropertyAttribute
    {
        /* Variables */

        public static string DEFAULT_ICON = null;

        private string mIcon = null;

        private string mTooltip = "";

        /* Setter & Getter */

        public string Icon { get { return this.mIcon; } }
        public string tooltip { get { return this.mTooltip; } }
        public Texture texture
        {
            get
            {
                if (String.IsNullOrEmpty(this.mIcon))
                {
                    if (DEFAULT_ICON == null)
                        return null;
                    return MxUtil.FindTexture(DEFAULT_ICON);
                }

                return MxUtil.FindTexture(mIcon);
            }
        }

        /* Functions */

        public InteractiveAttribute(string Icon = null, string Tooltip = null)
        {
            this.mTooltip = Tooltip;
            this.mIcon = Icon;
        }
    }
}
