/**
 * Copyright (c) Jen-Chieh Shen. All rights reserved.
 * 
 * jcs090218@gmail.com
 */
using System;
using UnityEngine;

namespace Mx
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class InteractiveAttribute : PropertyAttribute
    {
        /* Variables */

        public static string DEFAULT_ICON = null;

        private string mIcon = null;

        private string mSummary = null;
        private string mTooltip = null;

        /* Setter & Getter */

        public string Icon { get { return this.mIcon; } }
        public string summary { get { return this.mSummary; } }
        public string tooltip { get { return mTooltip; } }
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

        public InteractiveAttribute(string Icon = null, 
            string Summary = null,
            string Tooltip = null)
        {
            this.mIcon = Icon;
            this.mSummary = Summary;
            this.mTooltip = Tooltip;
        }
    }
}
