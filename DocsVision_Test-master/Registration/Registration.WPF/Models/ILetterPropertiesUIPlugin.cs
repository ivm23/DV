﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Registration.Model;

namespace Registration
{
    public interface ILetterPropertiesUIPlugin: IPropertiesUIPlugin
    {
        void OnLoad(IServiceProvider serviceProvider);

        LetterView LetterView { get; set; }

    //    event EventHandler AddedReceiver;
        bool ReadOnly { set; get; }
        void InitializeLetterView();
       // LetterView LetterView { get; set; }
    }
}
