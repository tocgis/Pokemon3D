﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace birdScript.Types
{
    class SVariable : SObject
    {
        private SObject _data;

        public string Identifier { get; private set; }
        public bool IsReadOnly { get; set; }

        public SVariable(string identifier, SObject data)
        {
            Identifier = identifier;
            _data = data;
        }

        public SVariable(string identifier, SObject data, bool isReadOnly)
        {
            Identifier = identifier;
            IsReadOnly = isReadOnly;
            _data = data;
        }

        public SObject Data
        {
            get
            {
                return _data;
            }
            set
            {
                if (!IsReadOnly)
                {
                    _data = value;
                }
            }
        }

        /// <summary>
        /// Sets data ignoring the Read-Only property. Only use when necessary.
        /// </summary>
        public void ForceSetData(SObject data)
        {
            _data = data;
        }
        
    }
}
