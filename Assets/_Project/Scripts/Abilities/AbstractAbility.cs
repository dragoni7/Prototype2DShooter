using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace dragoni7
{
    public abstract class AbstractAbility : MonoBehaviour
    {
        public abstract IEnumerator Execute(BaseEntity subject);
    }
}
