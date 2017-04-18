using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCMB;
using UniRx;
using UnityEngine;

namespace NcmbAsObservables.sample
{
    /// <summary>
    /// 参考： https://github.com/NIFTYCloud-mbaas/NCMB2dst_comp
    /// </summary>
    public class QuerySample : MonoBehaviour
    {
        void Start()
        {
            var query = new NCMBQuery<NCMBObject>("Score");
            query.OrderByDescending("score");
            query.Limit = 5;

            NcmbQueryHelper<NCMBObject>
                .FindAsync(query)
                .Subscribe(resultList =>
                {
                    foreach (var o in resultList)
                    {
                        Debug.Log(o);
                    }
                }, error => Debug.LogError(error));

        }

    }
}
