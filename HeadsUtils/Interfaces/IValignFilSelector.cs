using System;
using System.Collections.Generic;
using System.Text;
using HeadsUtils;

namespace HeadsUtils.Interfaces
{
    public interface IValignFilSelector
    {
        System.Windows.Forms.Form DialogInstance { get;}
        CValignInfo SelectedItem { get;}
    }
}
