using System;
using System.Collections.Generic;
using System.Text;

namespace PDVDroidElgin.Contracts
{
    public interface IToastFacade
    {
        void LongAlert(string message);
        void ShortAlert(string message);
    }
}
