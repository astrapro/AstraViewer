using System;
using System.Collections.Generic;
using System.Text;

namespace HeadsUtils.Interfaces
{
    public interface IWorkingDirSelector
    {
        System.Windows.Forms.Form DialogInstance { get;}
        string WorkingFolderPath { get;set;}
    }
}
