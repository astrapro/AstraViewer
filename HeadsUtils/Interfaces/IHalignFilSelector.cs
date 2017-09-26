using System;
using System.Collections.Generic;
using System.Text;
using HeadsUtils;

namespace HeadsUtils.Interfaces
{
    public interface IHalignFilSelector
    {
        System.Windows.Forms.Form DialogInstance { get;}
        CHIPInfo SelectedItem { get;}
    }
}
